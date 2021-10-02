using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{    
    public Image hpBar;        
    float maxHp;
    float hp;

    public Image timerBar;
    public Text timerText;
    float maxTimer;
    float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        SetReady(100f, 5f); //임시 
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= float.Epsilon)
        {
            Attack();           
            timer = maxTimer;
        }

        ChangeTimerUI();
    }

    private void Attack()
    {
        //Player Damaged
    }    

    public void SetReady(float maxHp, float maxTimer)
    {
        this.maxHp = maxHp;
        hp = maxHp;

        this.maxTimer = maxTimer;
        timer = maxTimer;

        hpBar.fillAmount = 1f;
        timerBar.fillAmount = 1f;

        int num = (int)maxTimer + 1;
        timerText.text = num.ToString();
    }

    public void GetDamage(float atk)
    {
        hp -= atk;
        if(hp <= float.Epsilon)
        {//임시 복구
            hp = maxHp;
        }

        ChangeHpBar();
    }

    private void ChangeHpBar()
    {
        float percent = hp / maxHp;
        hpBar.fillAmount = percent;
    }

    private void ChangeTimerUI()
    {
        int num = (int)timer + 1;
        timerText.text = num.ToString();

        float percent = timer / maxTimer;
        timerBar.fillAmount = percent;
    }
}
