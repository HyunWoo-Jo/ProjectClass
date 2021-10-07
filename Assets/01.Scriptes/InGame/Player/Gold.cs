///작성일 21.10,07
///작성자 조현우
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
    /// gold 사용
    /// </summary>
    /// <param name="useGold"> 사용할 골드량 </param>
    /// <returns> true 소모 성공, false 소모 실패 </returns>
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
    /// 골드 추가
    /// </summary>
    /// <param name="addGold">추가 량</param>
    public void AddGold(int addGold) {
        gold += addGold;
        if (!(addAction is null)) addAction.Invoke();
    }

}
