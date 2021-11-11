using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scene;

public class Monster : MonoBehaviour
{   
    public Image hpBar;    

    public Image timerBar;
    public Text timerText;
     
    public Image monSprite;


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


    // Update is called once per frame
    void Update()
    {
        status.atkTimer -= Time.deltaTime;
        if(status.atkTimer <= float.Epsilon)
        {
            Attack();
            status.atkTimer = status.atkSpeed;
        }

        ChangeTimerUI();                
    }

    private void Attack()
    {
        //Player Damaged
        //monManager.Attack()
    }    

    public void SetReady(Dictionary<MonsterInfo, string> info, Sprite sprite)
    {
        status.atk = float.Parse(info[MonsterInfo.Attack]);
        status.maxHp = float.Parse(info[MonsterInfo.HP]);
        status.hp = status.maxHp;
        status.defend = float.Parse(info[MonsterInfo.Defend]);
        status.atkSpeed = float.Parse(info[MonsterInfo.AttackSpeed]);
        status.atkTimer = status.atkSpeed;
        status.defendbreak = float.Parse(info[MonsterInfo.DefendBreak]);
        monSprite.sprite = sprite;

        status.dark = (info[MonsterInfo.Dark] == "TRUE") ? true : false;
        status.btnPlus = (info[MonsterInfo.ButtonPlus] == "TRUE") ? true : false;
        status.doubleAtk = (info[MonsterInfo.DoubleAttack] == "TRUE") ? true : false;

        ChangeHpBar();
        ChangeTimerUI();
    }

    public bool GetDamage(float atk)
    {
        float curDamage = atk;
        curDamage -= status.defend;

        status.hp -= curDamage;
        ChangeHpBar();
        if (status.hp <= float.Epsilon)
            return false;
        else
            return true;        
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
    
}
