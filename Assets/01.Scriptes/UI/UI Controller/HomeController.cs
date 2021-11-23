///�ۼ��� 21.10.19
///�ۼ��� ������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace GameUI.Controller {
    public class HomeController : MonoBehaviour {
        [SerializeField]
        private GameObject dungeonsPanel;
        [SerializeField]
        private GameObject reinforcementsPanel;

        List<GameObject> allPanelList = new List<GameObject>();

        [SerializeField]
        private GameObject dungeonEnterPanel;

        private void Awake() {
            allPanelList.Add(dungeonsPanel);
            allPanelList.Add(reinforcementsPanel);
        }

        public void OnOpenDungeon() {
            if(!dungeonsPanel.activeSelf) {
                SoundManager.Play_EFF("Button");
            }
            OffAllPanel();
            dungeonsPanel.SetActive(true);
            
        }
        public void OnOpenReinforce() {
            if(!reinforcementsPanel.activeSelf) {
                SoundManager.Play_EFF("Button");
            }
            OffAllPanel();      
            reinforcementsPanel.SetActive(true);
        }

        private void OffAllPanel() {
            foreach (var item in allPanelList) {
                item.SetActive(false);
            }
        }
        
        public void OnOpenEnterPanel() {
            //�ؽ�Ʈ ���� ����
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
