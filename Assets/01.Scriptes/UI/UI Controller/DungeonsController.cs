///작성일 21.10.19
///작성자 조현우
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GameUI.Controller {
    public class DungeonsController : MonoBehaviour {
        [SerializeField]
        private GameObject buttonParent;
        private List<Button> dungeonList = new List<Button>();
        [SerializeField]
        private GameObject dungeonEnterPanel;
        [SerializeField]
        private Text doungeonNameContext;

        protected void Awake() {
            Button[] buttons = buttonParent.GetComponentsInChildren<Button>();
            foreach (var button in buttons) {
                dungeonList.Add(button);
                button.onClick.AddListener(OpenEnterPanel);
            }

        }

        public void OpenEnterPanel() {
            //텍스트 내용 변경
            dungeonEnterPanel.SetActive(true);
            SoundManager.Play_EFF("Button");
        }

        public void OnEnter() {
            SceneManager.LoadScene("GameScene");
        }

        public void OnBack() {
            dungeonEnterPanel.SetActive(false);
            SoundManager.Play_EFF("Button");
        }
    }
}