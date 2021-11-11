///작성일 21.10.14
///작성자 조현우
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scene;
using UnityEngine.UI;

namespace GameUI.Controller {
    public class PlayerController : MonoBehaviour {
        [SerializeField]
        private GameScene gameScene;
        [SerializeField]
        private Text comboCountText;
        [SerializeField]
        private Image hp;

        [Header("Combo Settings")]
        [SerializeField]
        private List<int> comboFontSizeList;
        [SerializeField]
        private List<Color> comboFontColorList;

        private float currentComboMangnification = -1;

        private const string comboStr = "Combo";

        private Outline comboOutline;

        private void Start() {

            gameScene.sucessHandler += ChangeComboCountText;
            gameScene.failHandler += ChangeComboCountText;
            gameScene.failHandler += ChangeHpBar; // 임시 테스트용
            comboOutline = comboCountText.GetComponent<Outline>();
        }
        
        private void ChangeComboCountText() {

            float magnification = gameScene.playerCombo.GetComboMagnification();
            if(currentComboMangnification != magnification) {
                currentComboMangnification = magnification;
                SetFont(magnification);
            }
            comboCountText.text = string.Format("{0} \n {1}", gameScene.playerCombo.GetCombo().ToString(), comboStr);
        }

        private void SetFont(float magnification) {
            int index = (int)(magnification * 10) - 10;
            comboCountText.fontSize = comboFontSizeList[index];
            comboOutline.effectColor = comboFontColorList[index];
        }

        private void ChangeHpBar() {
            float percent = Player.instance.CurrentHp / Player.instance.MaxHp;
            hp.fillAmount = percent;
        }
    }
}
