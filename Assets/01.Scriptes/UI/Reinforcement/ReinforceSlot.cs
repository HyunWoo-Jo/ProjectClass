///작성일 21.10,07
///작성자 조현우
using UnityEngine;
using UnityEngine.UI;
public class ReinforceSlot : MonoBehaviour
{
    public Stats stat;
    public Text[] texts;
    private string textStr;
    private void Awake() {
        texts = GetComponentsInChildren<Text>();
        textStr = texts[0].text;
    }
    public void SetText(string text) {
        texts[0].text = string.Format("{0} : {1}", textStr, text);
    }
    public void SetGold(int gold) {
        texts[1].text = gold.ToString();
    }
    
}
