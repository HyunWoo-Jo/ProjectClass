using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public List<Ability> abilityList = new List<Ability>();
    private Dictionary<string, Sprite> abilityImgDictionary = new Dictionary<string, Sprite>();
    // Start is called before the first frame update
    void Awake()
    {
        LoadAbility();

        abilityList.Add(new Ability("���ݷ� ����", 1, 10, AbilityType.DAMAGE));
        abilityList.Add(new Ability("���� ����", 1, 10, AbilityType.ARMOUR));
        abilityList.Add(new Ability("HP ����", 1, 10, AbilityType.MAXHP));
        abilityList.Add(new Ability("����� ��� ����", 1, 0.1f, AbilityType.ABSORPTION_HP));
        abilityList.Add(new Ability("�������� ȸ���� ����", 1, 10f, AbilityType.STAGE_RECOVERY_HP));
    }
    private void LoadAbility() {
        Sprite[] imgs = Resources.LoadAll<Sprite>("Ability");

        for(int i = 0; i < imgs.Length; i++) {
            abilityImgDictionary.Add(imgs[i].name, imgs[i]);
        }
    }

    public Sprite GetSpirte(Ability ability ) {
        return abilityImgDictionary[ability.type.ToString()];
    }
}
