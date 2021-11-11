using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scene;

public class MonsterManager : MonoBehaviour
{    

    [System.Serializable]
    public struct MonSpot
    {
        public List<Monster> spot;
    }
    public List<MonSpot> monsterSpot;


    [System.Serializable]
    public struct MonSprite
    {
        public string name;
        public Sprite sprite;
    }
    public List<MonSprite> monSprite;

    public GameScene scene;

    private string dungeonNum;
    private int stage = 1;
    private int maxStage;
    private string stageInfoKey = "";

    private List<Monster> curMonList = new List<Monster>();

    
    private void Start()
    {        
        StartDungeon(4);
    }

    public void StartDungeon(int dungeonNum)
    {
        this.dungeonNum = dungeonNum.ToString();
        maxStage = int.Parse(CsvReader.instance.dic_dungenInfo[dungeonNum][1]);        

        stage = 1;
        stageInfoKey = dungeonNum.ToString() + "-" + stage.ToString();
        SetupStageMonsters();
    }

    private void SetupStageMonsters()
    {
        Dictionary<string, List<Dictionary<MonsterInfo, string>>> dic = CsvReader.instance.dic_monsterInfo;

        //for (int i = 0; i < monsterSpot.Count; i++)
        //    for (int j = 0; i < monsterSpot[i].spot.Count; j++)
        //        monsterSpot[i].spot[j].gameObject.SetActive(false);

        int spotIndex = dic[stageInfoKey].Count - 1;
        
        for (int i = 0; i < monsterSpot[spotIndex].spot.Count; i++)
        {
            curMonList.Add(monsterSpot[spotIndex].spot[i]);
            curMonList[i].gameObject.SetActive(true);

            Sprite sprite = null;
            for (int j = 0; j < monSprite.Count; j++)
            {
                if (monSprite[j].name == dic[stageInfoKey][i][MonsterInfo.Monster])
                {
                    sprite = monSprite[j].sprite;
                    break;
                }
            }

            curMonList[i].SetReady(dic[stageInfoKey][i], sprite);
        }
    }

    public void GetDamage(float atk)
    {
        if (curMonList.Count == 0) return;

        bool isAlive = curMonList[0].GetDamage(atk);
        if (isAlive == false)
        {
            curMonList[0].gameObject.SetActive(false);
            curMonList.RemoveAt(0);
            if (curMonList.Count == 0)
            {
                stage++;
                if (stage <= maxStage)
                {
                    stageInfoKey = dungeonNum + "-" + stage.ToString();
                    SetupStageMonsters();
                }
                else
                {
                    //GameEnd ¾Ë¸²
                }
            }
        }
    }


}
