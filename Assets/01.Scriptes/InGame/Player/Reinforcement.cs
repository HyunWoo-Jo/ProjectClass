///작성일 21.10,07
///작성자 조현우
using System.Collections.Generic;
using UnityEngine;

public class Reinforcement : MonoBehaviour
{
    public Dictionary<Stats, int> level = new Dictionary<Stats, int>();

    private void Awake() {
        for(int i = 0; i < 5; i++) {
            level.Add((Stats)i, 1);
        }
    }
    private void Start() {
        SetReinfoce();
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
