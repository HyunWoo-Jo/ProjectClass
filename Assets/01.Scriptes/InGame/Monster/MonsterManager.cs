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

    private int stage = 0;
    private List<Monster> curMonList = new List<Monster>();


    private void SetupStageMonsters()
    {
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

    private void MonsterDeath(int index)
    {
        if (curMonList.Count < index) return;

        int num = Random.Range(0, 2);
        if (num == 0)
        {
            fxManager.CoinFx(curMonList[index].transform);
            GameManager.instance.gold.AddGold(500);
        }
        
        curMonList[index].gameObject.SetActive(false);
        curMonList.RemoveAt(index);
        SoundManager.Play_EFF("foley_orc_death3"); // 몬스터 사망 시 
    }


    public void GetDamage(float atk, int poisonCount, int lightningCount, int freezingCount)
    {
        if (curMonList.Count == 0) return;

        bool isAlive = curMonList[0].GetDamage(atk);
        fxManager.AtkFx(curMonList[0].transform);

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
            MonsterDeath(0);
        }
    }

    public void DieByElse(Monster mon)
    {
        if (curMonList.Count == 0) return;
        for (int i = 0; i < curMonList.Count; i++)
        {
            if (curMonList[i] == mon)
            {
                MonsterDeath(i);
                break;
            }
        }
    }

    /// <summary>
    /// Game Scene에서 몬스터 생성을 제어하기 위해서 변경
    /// </summary>
    public void SpawnNextStageMonster() {
        if(curMonList.Count == 0) {
            stage++;
            SetupStageMonsters();
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
