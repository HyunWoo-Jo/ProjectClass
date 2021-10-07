///�ۼ��� 21.10,07
///�ۼ��� ������
using System;

public class Gold
{
    private int gold;
    public Action consumeAction = null;
    public Action addAction = null;
    public Action failAction = null;

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
            if (!(failAction is null)) failAction.Invoke();
            return false;
        } else {
            gold -= useGold;
            if (!(consumeAction is null)) consumeAction.Invoke();
            return true;
        }
    }
    
    /// <summary>
    /// ��� �߰�
    /// </summary>
    /// <param name="addGold">�߰� ��</param>
    public void AddGold(int addGold) {
        gold += addGold;
        if (!(addAction is null)) addAction.Invoke();
    }

}
