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
        [SerializeField]
        private Image hpBack;

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
            Player.instance.changeHp += ChangeHpBar;
        }
        void OnDestroy() {
            Player.instance.changeHp = null;
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
            if(hpAction != null) {
                targetHp = Player.instance.CurrentHp;
            } else {
                targetHp = Player.instance.CurrentHp;
                lastHp = Player.instance.LastHp;
                hpAction = StartCoroutine(ChangeHpBarAction(hp));
            }
            if(Player.instance.CurrentHp <= 0) {
                gameScene.GameOver();
            }
        }
        Coroutine hpAction;
        float targetHp;
        float lastHp;
        IEnumerator ChangeHpBarAction(Image img) {
            while(true) {
                lastHp = Mathf.Lerp(lastHp, targetHp, 5f * Time.deltaTime);
                img.fillAmount = lastHp / Player.instance.MaxHp;
                if(lastHp <= targetHp) {
                    hpAction = null;
                    img.fillAmount = targetHp / Player.instance.MaxHp;
                    break;
                }
                yield return null;
            }
        }

        public void DamageByMonster(float atk, float defendBreak)
        {
            //2021.11.12 작성, MonsterManager에서 사용중
            //확인 후 자유롭게 변경하셔도 됩니다.
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
            SoundManager.instance.PlayEFF("Hit_Player");
        }
    }
}
