using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AbilityManager : MonoBehaviour
{
    public List<Ability> abilityList = new List<Ability>();
    private Dictionary<string, Sprite> abilityImgDictionary = new Dictionary<string, Sprite>();
    // Start is called before the first frame update

    void Awake() {
        LoadAbility();
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
