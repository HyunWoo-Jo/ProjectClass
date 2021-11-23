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


    private string dungeonNum;
    private int stage = 1;
    private int maxStage;
    private string stageInfoKey = "";

    private List<Monster> curMonList = new List<Monster>();

    
    private void Start()
    {     
        StartDungeon(4); //�ӽ�, ���� ���ε�� �ҷ�����
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

    public void GetDamage(float atk, float combo = 1f)
    {
        if (curMonList.Count == 0) return;

        bool isAlive = curMonList[0].GetDamage(atk);
        if (isAlive == false)
        {
            curMonList[0].gameObject.SetActive(false);
            curMonList.RemoveAt(0);
            /// MonsterManager -> SpawnNextStageMonster() �� ����
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
            //        //GameEnd, dungeon clear �˸� �ʿ�
            //    }
            //}
        }
    }
    /// <summary>
    /// Game Scene���� ���� ������ �����ϱ� ���ؼ� ����
    /// </summary>
    public void SpawnNextStageMonster() {
        if(curMonList.Count == 0) {
            stage++;
            if(stage <= maxStage) {
                stageInfoKey = dungeonNum + "-" + stage.ToString();
                SetupStageMonsters();
            } else {
                //GameEnd, dungeon clear �˸� �ʿ�
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

    /// <summary>
    /// �ۼ��� ������ 22.11.22
    /// </summary>
    /// ���� ������ ���
    /// <returns></returns>
    public List<Monster> GetMonsterList() {
        return curMonList;
    }
}
