using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AbilityManager : MonoBehaviour
{
    public List<Ability> abilityList = new List<Ability>();
    private Dictionary<string, Sprite> abilityImgDictionary = new Dictionary<string, Sprite>();

    public List<Ability> currentAblity = new List<Ability>();
    // Start is called before the first frame update

    [HideInInspector] 
    public int lightningCount = 0;
    [HideInInspector] 
    public int poisonCount = 0;
    [HideInInspector] 
    public int freezingCount = 0;


    void Awake() {
        LoadAbility();
    }

    protected void OnDestroy() {
        Player.instance.ResetStats();
    }

    public void AddAbility(Ability abill) {
        currentAblity.Add(abill);

        switch(abill.type) {
            case AbilityType.Damage1:
            case AbilityType.Damage2:
            case AbilityType.Damage3:
                Player.instance.Damage *= (abill.increasedAmount * 0.01f) + 1;
                break;
            case AbilityType.Armour1:
            case AbilityType.Armour2:
            case AbilityType.Armour3:
                Player.instance.Armour *= (abill.increasedAmount * 0.01f) + 1;
                break;
            case AbilityType.Poison:
                poisonCount++;
                break;
            case AbilityType.Lightning:
                lightningCount++;
                break;
            case AbilityType.Freezing:
                freezingCount++;
                break;
            case AbilityType.Hp1:
            case AbilityType.Hp2:
            case AbilityType.Hp3:
                Player.instance.MaxHp *= (abill.increasedAmount * 0.01f) + 1;
                break;
            case AbilityType.Recovery1:
            case AbilityType.Recovery2:
            case AbilityType.Recovery3:
                Player.instance.CurrentHp += Player.instance.MaxHp * ((abill.increasedAmount * 0.01f) +1);
                break;
        }

    }

    private void LoadAbility() {
        Sprite[] imgs = Resources.LoadAll<Sprite>("Ability");

        for(int i = 0; i < imgs.Length; i++) {
            abilityImgDictionary.Add(imgs[i].name, imgs[i]);
        }
        var info = CsvReader.instance.dic_list_skillsInfo;

        
        for(int i = 0; i < info["Skill"].Count; i++) {
            string name = info["Skill"][i];
            float damage = 0.0f;
            if(!float.TryParse(info["Damage"][i], out damage)) return;
            AbilityType type;
            if(!Enum.TryParse(info["Type"][i], out type)) return;
            abilityList.Add(new Ability(name, 0, damage, type));
         }
    }

    public Sprite GetSpirte(Ability ability ) {
        return abilityImgDictionary[ability.type.ToString()];
    }
}
