using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scene;

public class MonsterManager : MonoBehaviour
{
    public MonsterSk skManager;

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
    public GameUI.Controller.PlayerController pCont;

    public Transform blockTarget;
    public FX_Manager fxManager;

    //private string dungeonNum;
    private int stage = 0;
    //private int maxStage;
    //private string stageInfoKey = "";

    private List<Monster> curMonList = new List<Monster>();

    
    //private void Start()
    //{     
    //    //StartDungeon(); //임시, 추후 씬로드시 불려야함
    //}

    //public void StartDungeon()
    //{
    //    //this.dungeonNum = dungeonNum.ToString();
    //    //maxStage = int.Parse(CsvReader.instance.dic_dungenInfo[dungeonNum][1]);        

    //    //stage = 1;
    //    //stageInfoKey = dungeonNum.ToString() + "-" + stage.ToString();
    //    SetupStageMonsters();
    //}

    private void SetupStageMonsters()
    {
        //Dictionary<string, List<Dictionary<MonsterInfo, string>>> dic = CsvReader.instance.dic_monsterInfo;
        //int spotIndex = dic[stageInfoKey].Count - 1;

        //for (int i = 0; i < monsterSpot[spotIndex].spot.Count; i++)
        //{
        //    curMonList.Add(monsterSpot[spotIndex].spot[i]);
        //    curMonList[i].gameObject.SetActive(true);

        //    Sprite sprite = null;
        //    for (int j = 0; j < monSprite.Count; j++)
        //    {
        //        if (monSprite[j].name == dic[stageInfoKey][i][MonsterInfo.Monster])
        //        {
        //            sprite = monSprite[j].sprite;
        //            break;
        //        }
        //    }

        //    curMonList[i].SetReady(dic[stageInfoKey][i], sprite);
        //}

        int curSpotIndex = Random.Range(0, monsterSpot.Count);
        bool isBoss = false;
        if (stage % 10 == 0)
        {
            isBoss = true;
            curSpotIndex = 0;
        }
        
        for (int i = 0; i < monsterSpot[curSpotIndex].spot.Count; i++)
        {
            curMonList.Add(monsterSpot[curSpotIndex].spot[i]);
            curMonList[i].gameObject.SetActive(true);

            int monIndex = -1;
            if (isBoss)
            {
                if (stage % 20 == 0) monIndex = 3;
                else monIndex = 2;
            }
            else monIndex = Random.Range(0, 2);
            Dictionary<MonsterInfo, string> curDic = CsvReader.instance.monsterList[monIndex];

            Sprite sprite = null;
            for (int j = 0; j < monSprite.Count; j++)
            {
                if (monSprite[j].name == curDic[MonsterInfo.Monster])
                {
                    sprite = monSprite[j].sprite;
                    break;
                }
            }

            curMonList[i].SetReady(curDic, sprite, stage);
        }

    }

    public void GetDamage(float atk, int poisonCount, int lightningCount, int freezingCount)
    {
        if (curMonList.Count == 0) return;

        bool isAlive = curMonList[0].GetDamage(atk);
        if (poisonCount > 0) curMonList[0].GetPoisonStatus(atk, poisonCount);
        if (freezingCount > 0) curMonList[0].GetFreezStatus(freezingCount);
        if (lightningCount > 0 && curMonList.Count > 1)
        {
            for (int i = 1; i < curMonList.Count; i++)
            {
                curMonList[i].GetLightningDamage(atk, lightningCount);
                fxManager.Lightning(blockTarget, curMonList[i].transform);
            }
        }

        if (isAlive == false)
        {
            curMonList[0].gameObject.SetActive(false);
            curMonList.RemoveAt(0);
            SoundManager.Play_EFF("foley_orc_death3"); // 몬스터 사망 시 
            /// MonsterManager -> SpawnNextStageMonster() 로 변경
            //if (curMonList.Count == 0)
            //{
            //    stage++;
            //    if (stage <= maxStage)
            //    {
            //        stageInfoKey = dungeonNum + "-" + stage.ToString();
            //        SetupStageMonsters();
            //    }
            //    else
            //    {
            //        //GameEnd, dungeon clear 알림 필요
            //    }
            //}
        }
    }
    /// <summary>
    /// Game Scene에서 몬스터 생성을 제어하기 위해서 변경
    /// </summary>
    public void SpawnNextStageMonster() {
        if(curMonList.Count == 0) {
            stage++;
            SetupStageMonsters();

            //if(stage <= maxStage) {
            //    stageInfoKey = dungeonNum + "-" + stage.ToString();
            //    SetupStageMonsters();
            //} else {
            //    //GameEnd, dungeon clear 알림 필요
            //}
        }
    }

    public void DieByElse(Monster mon)
    {
        if (curMonList.Count == 0) return;
        for(int i = 0; i < curMonList.Count; i++)
        {
            if(curMonList[i] == mon)
            {
                curMonList[i].gameObject.SetActive(false);
                curMonList.RemoveAt(i);
                SoundManager.Play_EFF("foley_orc_death3"); // 몬스터 사망 시 
                break;
            }           
        }
    }

    public void AttakToPlayer(float atk, float defendBreak)
    {
        pCont.DamageByMonster(atk, defendBreak);
    }

    public void RequestSk(MonsterInfo info)
    {
        if (info == MonsterInfo.ButtonPlus)
            scene.StartCoroutine(scene.TestAddButton());
        else
            skManager.PlaySkill(info);
    }

    public void SetPause(bool b)
    {
        for (int i = 0; i < curMonList.Count; i++)
            curMonList[i].SetPause(b);
    }

    /// <summary>
    /// 작성자 조현우 22.11.22
    /// </summary>
    /// 게임 씬에서 사용
    /// <returns></returns>
    public List<Monster> GetMonsterList() {
        return curMonList;
    }
}
