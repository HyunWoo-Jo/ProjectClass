///작성일 21.09.26
///작성자 조현우
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignStruct;
using System;
public class Player : Singleton<Player>
{
    #region State
    
    [SerializeField] private float damage = 100.0f;
    [SerializeField] private float armour = 20.0f;
    [SerializeField] private float maxHp = 1500.0f;
    [SerializeField] private float currentHp = 1500.0f;
    [SerializeField] private float stageRecoveryHp = 20.0f;
    [SerializeField] private float absorptionHp = 0.5f;
    private float lastHp = 1500.0f;
    public Action changeHp;

    public float Damage { get => damage; set => damage = value; }
    public float Armour { get => armour; set => armour = value; }
    public float MaxHp { get => maxHp; 
        set { 
            maxHp = value;
            if(changeHp != null) changeHp.Invoke();
        } 
    }
    public float CurrentHp {
        get => currentHp;
        set {
            float buffer = value;
            if (this.currentHp > maxHp) {
                buffer = maxHp;
            }
            lastHp = currentHp;
            currentHp = buffer;
            if(changeHp != null) changeHp.Invoke();
        }
    }
    public float StageRecoveryHp { get => stageRecoveryHp; set => stageRecoveryHp = value; }
    public float AbsorptionHp { get => absorptionHp; set => absorptionHp = value; }
    public float LastHp { get => lastHp;}
    #endregion
    /// <summary>
    /// 스테이지 이동시 Hp 회복
    /// </summary>
    public void RecoverHp() {
        CurrentHp += MaxHp * (StageRecoveryHp * 0.01f);
        if(changeHp != null) changeHp.Invoke();
    }
    /// <summary>
    /// 생멱력 흡수
    /// </summary>
    /// <param name="damage"> 적에게 준 데미지</param>
    public void Absorb(float damage) {
        CurrentHp += damage * (AbsorptionHp * 0.01f);
        if(changeHp != null) changeHp.Invoke();
    }

    public void ResetStats() {
        damage = 100.0f;
        armour = 20.0f;
        maxHp = 1500.0f;
        currentHp = 1500.0f;
        stageRecoveryHp = 20.0f;
        absorptionHp = 0.5f;
    }
}
