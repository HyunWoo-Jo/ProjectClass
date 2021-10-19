///작성일 21.10.19
///작성자 조현우
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI_Controller {
    public class HomeController : MonoBehaviour {
        [SerializeField]
        private GameObject dungeonsPanel;
        [SerializeField]
        private GameObject reinforcementsPanel;

        List<GameObject> allPanelList = new List<GameObject>();

        private void Awake() {
            allPanelList.Add(dungeonsPanel);
            allPanelList.Add(reinforcementsPanel);
        }

        public void OnOpenDungeon() {
            OffAllPanel();
            dungeonsPanel.SetActive(true);
        }
        public void OnOpenReinforce() {
            OffAllPanel();
            reinforcementsPanel.SetActive(true);
        }

        private void OffAllPanel() {
            foreach (var item in allPanelList) {
                item.SetActive(false);
            }
        }
    }
}
