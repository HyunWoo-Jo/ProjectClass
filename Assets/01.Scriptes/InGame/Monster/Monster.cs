using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scene;

public class Monster : MonoBehaviour
{   
    private enum DamagedKind
    {
        Normal, Posion, Freezing, Lightning
    }

    public Image hpBar;    

    public Image timerBar;
    public Text timerText;
     
    public Image monSprite;

    public MonsterManager manager;

    struct monStatus
    {
        public float atk;
        public float maxHp;
        public float hp;
        public float defend;
        public float defendbreak;
        public float atkSpeed;
        public float atkTimer;

        public bool dark;
        public bool btnPlus;
        public bool doubleAtk;
    }
    monStatus status;

    private int skChecker = 0;
    private bool isPause = false;
    private Color defColor = Color.white;
    private bool isAlive = false;

    //Atk moving
    private Vector3 defPos;
    private Vector3 moveScale = new Vector3(0, -30f, 0);
    
    //poison
    private bool isPosion = false;
    private float posionDamage = 0f;
    private float posionTimer = 1f;

    //freezing
    private bool isFreezing = false;
    private float reFreezTimer = 0f;

    private void Awake()
    {
        defPos = monSprite.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPause) return;
        if (isAlive == false) return;

        if (isPosion)
        {
            posionTimer -= Time.deltaTime;
            if (posionTimer <= float.Epsilon)
            {
                GetPosionDamage();
                posionTimer = 1f;
            }
        }

        if (isFreezing) return;
        if (reFreezTimer >= float.Epsilon) reFreezTimer -= Time.deltaTime;

        status.atkTimer -= Time.deltaTime;
        if(status.atkTimer <= float.Epsilon)
        {
            StartCoroutine(AtkMoving());
            Attack();
            status.atkTimer = status.atkSpeed;
        }

        ChangeTimerUI();                
    }
    
    private IEnumerator AtkMoving()
    {
        monSprite.transform.localPosition = defPos + moveScale;
        yield return new WaitForSeconds(0.1f);
        monSprite.transform.localPosition = defPos;
    } 


    public void SetPause(bool b)
    {
        isPause = b;
    }

    private void Attack()
    {
        manager.AttakToPlayer(status.atk, status.defendbreak);

        skChecker++;
        if (skChecker == 2)
        {
            if (status.doubleAtk) manager.AttakToPlayer(status.atk, status.defendbreak);
        }
        else if (skChecker == 3)
        {
            if (status.dark) manager.RequestSk(MonsterInfo.Dark);
            if (status.btnPlus) manager.RequestSk(MonsterInfo.ButtonPlus);
        }
        else if (skChecker == 4)
        {
            skChecker = 0;
        }
    }    

    public void SetReady(Dictionary<MonsterInfo, string> info, Sprite sprite, int stage)
    {
        status.atk = float.Parse(info[MonsterInfo.Attack]);        
        status.maxHp = float.Parse(info[MonsterInfo.HP]);        
        status.defend = float.Parse(info[MonsterInfo.Defend]);
        status.atkSpeed = float.Parse(info[MonsterInfo.AttackSpeed]);        
        status.defendbreak = float.Parse(info[MonsterInfo.DefendBreak]);

        float bonus = float.Parse(info[MonsterInfo.StageBonus]);
        status.atk += stage * bonus * status.atk;
        status.maxHp += stage * bonus * status.maxHp;
        status.defend += stage * bonus * status.defend;
        status.defendbreak += stage * bonus * status.defendbreak;

        status.hp = status.maxHp;
        status.atkTimer = status.atkSpeed;
        monSprite.sprite = sprite;

        status.dark = (info[MonsterInfo.Dark] == "TRUE") ? true : false;
        status.btnPlus = (info[MonsterInfo.ButtonPlus] == "TRUE") ? true : false;
        status.doubleAtk = (info[MonsterInfo.DoubleAttack] == "TRUE") ? true : false;

        skChecker = 0;
        isPosion = false;
        monSprite.color = Color.white;
        monSprite.transform.localPosition = defPos;
        defColor = Color.white;
        isFreezing = false;
        reFreezTimer = 0f;
        isAlive = true;

        ChangeHpBar();
        ChangeTimerUI();
    }

    private bool CheckAlive()
    {
        if (status.hp <= float.Epsilon)
        {
            isAlive = false;
            return false;
        }            
        else
            return true;
    }

    public bool GetNormalDamage(float atk)
    {
        float curDamage = atk - status.defend;
        if (curDamage <= 0f) curDamage = 1f;

        status.hp -= curDamage;
        ChangeHpBar();
        StartCoroutine(DamagedEfx(DamagedKind.Normal, 0.2f));

        return CheckAlive();
    }

    public void GetPoisonStatus(float atk, int posionCount)
    {
        if (isPosion) return;
        isPosion = true;

        float curDamage = atk - status.defend;
        if (curDamage <= 0f) curDamage = 1f;

        posionDamage = curDamage * (0.05f * posionCount);
        if (posionDamage <= 0f) posionDamage = 1f;

        posionTimer = 1f;
    }

    private void GetPosionDamage()
    {
        status.hp -= posionDamage;
        ChangeHpBar();        
        StartCoroutine(DamagedEfx(DamagedKind.Posion, 0.2f));

        if (CheckAlive() == false)
            manager.DieByElse(this);
    }

    public void GetFreezStatus(int freezingCount)
    {
        if (isFreezing) return;
        if (reFreezTimer >= float.Epsilon) return;

        isFreezing = true;
        reFreezTimer = 3f;
        StartCoroutine(DamagedEfx(DamagedKind.Freezing, 1f + 0.2f * freezingCount));
    }

    public void GetLightningDamage(float atk, int lightningCount)
    {
        float curDamage = atk - status.defend;
        if (curDamage <= 0f) curDamage = 1f;
        curDamage = curDamage * 0.2f * lightningCount;        

        status.hp -= curDamage;
        ChangeHpBar();
        StartCoroutine(DamagedEfx(DamagedKind.Lightning, 0.2f));

        if (CheckAlive() == false)
            manager.DieByElse(this);
    }

    private IEnumerator DamagedEfx(DamagedKind kind, float time)
    {
        if (isAlive)
        {
            switch (kind)
            {
                case DamagedKind.Normal: monSprite.color = Color.red; break;
                case DamagedKind.Posion: monSprite.color = Color.green; break;
                case DamagedKind.Lightning: monSprite.color = Color.yellow; break;
                case DamagedKind.Freezing:
                    monSprite.color = Color.blue;
                    defColor = Color.blue;
                    break;
                default: break;
            }
        }        
        yield return new WaitForSeconds(time);
        if (isAlive)
        {
            switch (kind)
            {
                case DamagedKind.Freezing:
                    defColor = Color.white;
                    isFreezing = false;
                    break;
                default: break;
            }
            monSprite.color = defColor;
        }        
    }

    private void ChangeHpBar()
    {
        float curHp = status.hp;
        if (curHp < 0) curHp = 0f;
        float percent = curHp / status.maxHp;
        hpBar.fillAmount = percent;
    }

    private void ChangeTimerUI()
    {
        int num = (int)status.atkTimer + 1;
        timerText.text = num.ToString();

        float percent = status.atkTimer / status.atkSpeed;
        timerBar.fillAmount = percent;
    }

    public void DyingAction()
    {
        monSprite.color = Color.white;
        StartCoroutine(DyingAlpha());
    }

    private IEnumerator DyingAlpha()
    {
        while(monSprite.color.a > 0f)
        {
            Color c = monSprite.color;
            c.a -= 0.1f;
            monSprite.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        this.gameObject.SetActive(false);
    }

}
