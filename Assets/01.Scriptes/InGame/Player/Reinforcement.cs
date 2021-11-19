///작성일 21.10,07
///작성자 조현우
using System.Collections.Generic;
using UnityEngine;
using System;

public class Reinforcement : MonoBehaviour
{
    public Dictionary<Stats, int> level = new Dictionary<Stats, int>();

    private void Awake() {
        LoadReinforce();
    }
    private void Start() {
        SetReinfoce();
    }


    private void LoadReinforce() {
        foreach(Stats stat in Enum.GetValues(typeof(Stats))) {
            level.Add(stat, 1);
        }
        foreach(Stats stat in Enum.GetValues(typeof(Stats))) {
            if(PlayerPrefs.HasKey(stat.ToString())) {
                level[stat] = PlayerPrefs.GetInt(stat.ToString());
            }
        }
    }

    public void SaveReinforce() {
        foreach(Stats stat in Enum.GetValues(typeof(Stats))) {
            PlayerPrefs.SetInt(stat.ToString(), level[stat]);
        }
    }

    public float GetReinfoceStat(Stats stat) {
        switch (stat) {
            case Stats.DAMAGE:
            return 100.0f * level[stat]; 
            case Stats.ARMOUR:
            return 20.0f * level[stat];
            case Stats.MAXHP:
            return 1500.0f * level[stat];
            case Stats.STAGE_RECOVERY_HP:
            return 20.0f * level[stat];
            case Stats.ABSORPTION_HP:
            return 0.5f * level[stat];
        }
        return 0f;
    }
    public int GetReinfoceGold(Stats stat) {
        return level[stat] * 100;
    }

    public void SetReinfoce() {
        foreach (var item in level) {
            float stat = GetReinfoceStat(item.Key);
            switch (item.Key) {
                case Stats.DAMAGE:
                Player.instance.Damage = stat;
                break;
                case Stats.ARMOUR:
                Player.instance.Armour = stat;
                break;
                case Stats.MAXHP:
                Player.instance.MaxHp = stat;
                break;
                case Stats.STAGE_RECOVERY_HP:
                Player.instance.StageRecoveryHp = stat;
                break;
                case Stats.ABSORPTION_HP:
                Player.instance.AbsorptionHp = stat;
                break;
            }
        }
    }
}
