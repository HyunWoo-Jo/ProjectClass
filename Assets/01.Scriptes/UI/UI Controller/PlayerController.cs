///�ۼ��� 21.10.14
///�ۼ��� ������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scene;
using UnityEngine.UI;

namespace UI_Controller {
    public class PlayerController : MonoBehaviour {
        [SerializeField]
        private GameScene gameScene;
        [SerializeField]
        private Text comboCountText;
        [SerializeField]
        private Image hp;

        private void Start() {

            gameScene.sucessHandler += ChangeComboCountText;
            gameScene.failHandler += ChangeComboCountText;
            gameScene.failHandler += ChangeHpBar; // �ӽ� �׽�Ʈ��
        }
        
        private void ChangeComboCountText() {
            comboCountText.text = gameScene.playerCombo.GetCombo().ToString();
        }

        private void ChangeHpBar() {
            float percent = Player.instance.CurrentHp / Player.instance.MaxHp;
            hp.fillAmount = percent;
        }
    }
}
