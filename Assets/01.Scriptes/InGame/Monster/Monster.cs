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

    public Button imgBtn;
    private GameScene scene;

    private Image monSprite;

    struct monStatus
    {
        public float maxHp;
        public float hp;
        public float amor;
        public float atkSpeed;
        public float atkTimer;
        public float amorbreak;
    }
    monStatus status;

    
    void Awake()
    {
        scene = GameObject.Find("GameScene").GetComponent<GameScene>();
        imgBtn.onClick.AddListener(() => scene.SetCurMonster(this));
        scene.SetCurMonster(this);

        monSprite = imgBtn.gameObject.GetComponent<Image>();        

        SetReady(100f, 5f, 5f, 1f); //임시 
    }

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
    }    

    public void SetReady(float maxHp, float amor, float atkSpeed, float amorbreak, Sprite sprite)
    {
        status.maxHp = maxHp;
        status.hp = maxHp;
        status.amor = amor;
        status.atkSpeed = atkSpeed;
        status.atkTimer = atkSpeed;
        status.amorbreak = amorbreak;
        monSprite.sprite = sprite;

        ChangeHpBar();
        ChangeTimerUI();
    }

    public void SetReady(float maxHp, float amor, float atkSpeed, float amorbreak)
    {//임시 함수
        status.maxHp = maxHp;
        status.hp = maxHp;
        status.amor = amor;
        status.atkSpeed = atkSpeed;
        status.atkTimer = atkSpeed;
        status.amorbreak = amorbreak;

        ChangeHpBar();
        ChangeTimerUI();
    }

    public void GetDamage(float atk)
    {
        float curDamage = atk;
        curDamage -= status.amor;

        status.hp -= curDamage;
        if(status.hp <= float.Epsilon)
        {//임시 복구
            status.hp = status.maxHp;
            //scene.SetCurMonster(null);
        }

        ChangeHpBar();
    }

    private void ChangeHpBar()
    {
        float percent = status.hp / status.maxHp;
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
