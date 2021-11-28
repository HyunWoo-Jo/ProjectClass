///작성일 21.10.19
///작성자 조현우
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameUI;
namespace GameUI.Controller {
    public class HomeController : MonoBehaviour {
        private enum UI_Type {
            Home,
            Reinforce,
        }
        private UI_Type currentState = UI_Type.Home;
        private GameObject currentPopup;
        private bool isAction = false;
        [SerializeField]
        private GameObject dungeonsPanel;

        [SerializeField]
        private GameObject reinforcementsPanel;


        [SerializeField]
        private GameObject dungeonEnterPanel;

        [SerializeField]
        private GameObject allPanelsParent;

        [SerializeField]
        private GameObject fadePanel;
        private void Awake() {
            currentPopup = dungeonsPanel;
        }

        public void OnOpenDungeon(GameObject obj) {
            if(currentState != UI_Type.Home && !isAction) {
                isAction = true;
                SoundManager.Play_EFF("Button");
                currentPopup.Add_UI_Animation().Play(UI_Animation_Type.ScrollRightFromCenter, 0.5f);

                dungeonsPanel.Add_UI_Animation().Play(UI_Animation_Type.ScrollRightFromLeft, 0.5f, () => { isAction = false; });
                currentPopup = dungeonsPanel;
                currentState = UI_Type.Home;

                obj.Add_UI_Animation().Click();
            }
        }
        public void OnOpenReinforce(GameObject obj) {
            if(currentState != UI_Type.Reinforce && !isAction) {
                reinforcementsPanel.SetActive(true);
                isAction = true;
                SoundManager.Play_EFF("Button");
                currentPopup.Add_UI_Animation().Play(UI_Animation_Type.ScrollLeftFromCenter, 0.5f);

                reinforcementsPanel.Add_UI_Animation().Play(UI_Animation_Type.ScrollLeftFromRight, 0.5f, () => { isAction = false; });
                currentPopup = reinforcementsPanel;
                currentState = UI_Type.Reinforce;

                obj.Add_UI_Animation().Click();
            }
        }

        public void OnOpenEnterPanel(GameObject obj) {
            //텍스트 내용 변경
            if(!dungeonEnterPanel.activeSelf) {
                dungeonEnterPanel.SetActive(true);
                dungeonEnterPanel.Add_UI_Animation().Play(UI_Animation_Type.ScrollUpFromUnder, 0.5f);
                obj.Add_UI_Animation().Click();
                

                SoundManager.Play_EFF("Button");
            }
        }

        public void OnEnter() {
            fadePanel.AddFadeUI().FadeIn(1f, 1);
            Invoke("LoadGameScene", 1.5f);
        }
        
        private void LoadGameScene() {
            SceneManager.LoadScene("GameScene");
        }

        public void OnBack(GameObject obj) {
            obj.Add_UI_Animation().Click();
            dungeonEnterPanel.Add_UI_Animation().Play(UI_Animation_Type.ScaleDown, 0.2f, 
                () => { 
                    dungeonEnterPanel.SetActive(false);
                    dungeonEnterPanel.transform.localScale = Vector3.one;
                    
                }
                
                );
            SoundManager.Play_EFF("Button");
        }
    }
}
