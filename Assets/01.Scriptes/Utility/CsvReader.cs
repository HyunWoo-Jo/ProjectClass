using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignStruct;

public class CsvReader : Singleton<CsvReader>
{
    //Dic: 던전-스테이지, List: 몬스터수, Dic: 개별몬스터
    [HideInInspector] public Dictionary<string, List<Dictionary<MonsterInfo, string>>> dic_monsterInfo = new Dictionary<string, List<Dictionary<MonsterInfo, string>>>();

    //key: 던전num, List: 0-이름,1-max stage
    [HideInInspector] public Dictionary<int, List<string>> dic_dungenInfo = new Dictionary<int, List<string>>();
    

    protected override void Awake()
    {
        base.Awake();
        SetupMonsterInfo();
        SetupDungeonInfo();
    }

    private void SetupMonsterInfo()
    {
        TextAsset txtFile = Resources.Load<TextAsset>("CSV/Dungeon_Info");
        string[] lines = txtFile.text.Split('\n');

        for(int i = 1; i < lines.Length; i++)
        {
            string[] strs = lines[i].Split(',');

            string key = strs[0] + "-" + strs[1];
            if (dic_monsterInfo.ContainsKey(key) == false)
                dic_monsterInfo.Add(key, new List<Dictionary<MonsterInfo, string>>());
                                                
            Dictionary<MonsterInfo, string> curMon = new Dictionary<MonsterInfo, string>();
            curMon.Add(MonsterInfo.Monster, strs[2]);
            curMon.Add(MonsterInfo.Attack, strs[3]);
            curMon.Add(MonsterInfo.Defend, strs[4]);
            curMon.Add(MonsterInfo.HP, strs[5]);
            curMon.Add(MonsterInfo.AttackSpeed, strs[6]);
            curMon.Add(MonsterInfo.DefendBreak, strs[7]);
            curMon.Add(MonsterInfo.Dark, strs[8]);
            curMon.Add(MonsterInfo.ButtonPlus, strs[9]);
            curMon.Add(MonsterInfo.DoubleAttack, strs[10]);

            dic_monsterInfo[key].Add(curMon);
        }

    }

    private void SetupDungeonInfo()
    {
        TextAsset txtFile = Resources.Load<TextAsset>("CSV/Dungeon_Name_StageMax");
        string[] lines = txtFile.text.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            string[] strs = lines[i].Split(',');
            int key = int.Parse(strs[0]);

            dic_dungenInfo.Add(key, new List<string>());
            dic_dungenInfo[key].Add(strs[1]);
            dic_dungenInfo[key].Add(strs[2]);
        }
    }
}
