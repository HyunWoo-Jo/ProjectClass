///�ۼ��� 21.10,07
///������ 21.10.14
///�ۼ��� ������
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
    /// gold ���
    /// </summary>
    /// <param name="useGold"> ����� ��差 </param>
    /// <returns> true �Ҹ� ����, false �Ҹ� ���� </returns>
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
    /// ��� �߰�
    /// </summary>
    /// <param name="addGold">�߰� ��</param>
    public void AddGold(int addGold) {
        gold += addGold;
        if (!(addHandler is null)) addHandler.Invoke();
        Save();
    }

}
