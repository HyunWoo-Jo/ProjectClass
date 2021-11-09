using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public List<List<Monster>> monsterSpot;

    private int dungeonNum;
    private int stage = 1;
    private int maxStage;
    private int remainMonster = 0;
    private string stageInfoKey = "";

    public void SettingDungeon(int dungeonNum)
    {
        this.dungeonNum = dungeonNum;
        maxStage = int.Parse(CsvReader.instance.dic_dungenInfo[dungeonNum][1]);

        stage = 1;
        stageInfoKey = dungeonNum.ToString() + "-" + stage.ToString();         
    }

    private void SetupStageMonsters()
    {
        Dictionary<string, List<Dictionary<MonsterInfo, string>>> dic = CsvReader.instance.dic_monsterInfo;

        remainMonster = dic[stageInfoKey].Count;

        for (int i = 0; i < monsterSpot.Count; i++)
            for (int j = 0; i < monsterSpot[i].Count; j++)
                monsterSpot[i][j].gameObject.SetActive(false);

        int spotIndex = remainMonster - 1;
        for (int i = 0; i < monsterSpot[spotIndex].Count; i++)
        {
            monsterSpot[spotIndex][i].gameObject.SetActive(true);
            monsterSpot[spotIndex][i].SetReady(
                float.Parse(dic[stageInfoKey][i][MonsterInfo.HP]),
                float.Parse(dic[stageInfoKey][i][MonsterInfo.Defend]),
                float.Parse(dic[stageInfoKey][i][MonsterInfo.AttackSpeed]),
                float.Parse(dic[stageInfoKey][i][MonsterInfo.DefendBreak])
            );
        }
    }

}
