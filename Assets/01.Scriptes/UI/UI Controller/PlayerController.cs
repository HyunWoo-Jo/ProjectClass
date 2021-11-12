///�ۼ��� 21.10.14
///�ۼ��� ������
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
            gameScene.failHandler += ChangeHpBar; // �ӽ� �׽�Ʈ��
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

        public void DamageByMonster(float atk, float defendBreak)
        {
            //2021.11.12 �ۼ�, MonsterManager���� �����
            //Ȯ�� �� �����Ӱ� �����ϼŵ� �˴ϴ�.
            float ratio = 0.5f;
            float armour = Player.instance.Armour;
            if (armour < defendBreak)
            {
                float percent = (armour / defendBreak) * 100f;
                if (percent < 70) ratio = 0.6f;
                if (percent < 60) ratio = 0.7f;
                if (percent < 50) ratio = 0.8f;
                if (percent < 40) ratio = 0.9f;
                if (percent < 20) ratio = 1f;
            }
            int curDamage = (int)(atk * ratio);
            Player.instance.CurrentHp -= curDamage;
            ChangeHpBar();
        }
    }
}
