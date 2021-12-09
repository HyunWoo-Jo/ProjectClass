using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignStruct;

public class CsvReader : Singleton<CsvReader>
{
    //Dic: 던전-스테이지, List: 몬스터수, Dic: 개별몬스터
    //[HideInInspector] public Dictionary<string, List<Dictionary<MonsterInfo, string>>> dic_monsterInfo = new Dictionary<string, List<Dictionary<MonsterInfo, string>>>();
    [HideInInspector] public List<Dictionary<MonsterInfo, string>> monsterList = new List<Dictionary<MonsterInfo, string>>();


    //key: 던전num, List: 0-이름,1-max stage
    [HideInInspector] public Dictionary<int, List<string>> dic_dungenInfo = new Dictionary<int, List<string>>();

    [HideInInspector] public Dictionary<string, List<string>> dic_list_skillsInfo = new Dictionary<string, List<string>>();

    protected override void Awake()
    {
        base.Awake();
        //SetupMonsterInfo();
        SetupInfiniteMonster();
        SetupDungeonInfo();
        SetupSkillsInfo();
    }

    //private void SetupMonsterInfo()
    //{
    //    TextAsset txtFile = Resources.Load<TextAsset>("CSV/Dungeon_Info");
    //    string[] lines = txtFile.text.Split('\n');

    //    for(int i = 1; i < lines.Length; i++)
    //    {
    //        string[] strs = lines[i].Split(',');

    //        string key = strs[0] + "-" + strs[1];
    //        if (dic_monsterInfo.ContainsKey(key) == false)
    //            dic_monsterInfo.Add(key, new List<Dictionary<MonsterInfo, string>>());

    //        Dictionary<MonsterInfo, string> curMon = new Dictionary<MonsterInfo, string>();
    //        curMon.Add(MonsterInfo.Monster, strs[2]);
    //        curMon.Add(MonsterInfo.Attack, strs[3]);
    //        curMon.Add(MonsterInfo.Defend, strs[4]);
    //        curMon.Add(MonsterInfo.HP, strs[5]);
    //        curMon.Add(MonsterInfo.AttackSpeed, strs[6]);
    //        curMon.Add(MonsterInfo.DefendBreak, strs[7]);
    //        curMon.Add(MonsterInfo.Dark, strs[8]);
    //        curMon.Add(MonsterInfo.ButtonPlus, strs[9]);
    //        curMon.Add(MonsterInfo.DoubleAttack, strs[10]);

    //        dic_monsterInfo[key].Add(curMon);
    //    }
    //}

    private void SetupInfiniteMonster()
    {
        TextAsset txtFile = Resources.Load<TextAsset>("CSV/Infinite_Dungeon");
        string[] lines = txtFile.text.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            string[] strs = lines[i].Split(',');
            Dictionary<MonsterInfo, string> curDic = new Dictionary<MonsterInfo, string>();
            curDic.Add(MonsterInfo.Monster, strs[0]);
            curDic.Add(MonsterInfo.Attack, strs[1]);
            curDic.Add(MonsterInfo.Defend, strs[2]);
            curDic.Add(MonsterInfo.HP, strs[3]);
            curDic.Add(MonsterInfo.AttackSpeed, strs[4]);
            curDic.Add(MonsterInfo.DefendBreak, strs[5]);
            curDic.Add(MonsterInfo.Dark, strs[6]);
            curDic.Add(MonsterInfo.ButtonPlus, strs[7]);
            curDic.Add(MonsterInfo.DoubleAttack, strs[8]);
            curDic.Add(MonsterInfo.StageBonus, strs[9]);

            monsterList.Add(curDic);
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

    private void SetupSkillsInfo() {
        TextAsset txtFill = Resources.Load<TextAsset>("CSV/Skill_Table");
        string[] lines = txtFill.text.Split('\n');
        if(lines.Length <= 0) return;

        string[] keys = lines[0].Split(',');

        for(int i = 0; i < keys.Length; i++) {
            dic_list_skillsInfo.Add(keys[i], new List<string>());
        }

        for(int i =1;i < lines.Length; i++) {
            string[] values = lines[i].Split(',');
            for(int y = 0; y < keys.Length; y++) {
                dic_list_skillsInfo[keys[y]].Add(values[y]);
            }
        }
    }

}
