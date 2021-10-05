///�ۼ��� 21.09.26
///�ۼ��� ������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignStruct;
public class Player : Singleton<Player>
{
    #region State
    private float damage = 100.0f;
    private float armour = 20.0f;
    private float maxHp = 1500.0f;
    private float currentHp = 1500.0f;
    private float stageRecoveryHp = 20.0f;
    private float absorptionHp = 0.5f;

    public float Damage { get => damage; set => damage = value; }
    public float Armour { get => armour; set => armour = value; }
    public float MaxHp { get => maxHp; set => maxHp = value; }
    public float CurrentHp {
        get => currentHp;
        set {
            float buffer = value;
            if (this.currentHp > maxHp) {
                buffer = maxHp;
            }
            currentHp = buffer;
        }
    }
    public float StageRecoveryHp { get => stageRecoveryHp; set => stageRecoveryHp = value; }
    public float AbsorptionHp { get => absorptionHp; set => absorptionHp = value; }
    #endregion
    /// <summary>
    /// �������� �̵��� Hp ȸ��
    /// </summary>
    public void RecoverHp() {
        CurrentHp += MaxHp * (StageRecoveryHp * 0.01f);
    }
    /// <summary>
    /// ����� ���
    /// </summary>
    /// <param name="damage"> ������ �� ������</param>
    public void Absorb(float damage) {
        CurrentHp += damage * (AbsorptionHp * 0.01f);
    }
}
