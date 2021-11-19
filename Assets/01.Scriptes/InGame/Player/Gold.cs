///작성일 21.10,07
///수정일 21.10.14
///작성자 조현우
using System;
using UnityEngine;

public class Gold
{
    private int gold;
    public Action consumeHandler = null;
    public Action addHandler = null;
    public Action failHandler = null;

    private void Save() {
        PlayerPrefs.SetInt("gold", gold);
    }
    public void Load() {
        if(PlayerPrefs.HasKey("gold")) {
            gold = PlayerPrefs.GetInt("gold");
        }
    }

    public int GetGold() {
        return gold;
    }
    /// <summary>
    /// gold 사용
    /// </summary>
    /// <param name="useGold"> 사용할 골드량 </param>
    /// <returns> true 소모 성공, false 소모 실패 </returns>
    public bool ConsumeGold(int useGold) {
        if(gold < useGold) {
            if (!(failHandler is null)) failHandler.Invoke();
            return false;
        } else {
            gold -= useGold;
            if (!(consumeHandler is null)) consumeHandler.Invoke();
            Save();
            return true;
        }
    }
    
    /// <summary>
    /// 골드 추가
    /// </summary>
    /// <param name="addGold">추가 량</param>
    public void AddGold(int addGold) {
        gold += addGold;
        if (!(addHandler is null)) addHandler.Invoke();
        Save();
    }

}
