///작성일 21.09.25
///작성자 조현우
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
namespace UI_Controller {
    public class ButtonController : MonoBehaviour {
        [SerializeField]
        private PanelController panelController;
        public Action leftButtonHandler;
        public Action rightButtonHandler;

        public Action pauseButtonHandler;
        public Action resetartButtonHandler;

        public void OnLeft() {
            leftButtonHandler.Invoke();
        }
        public void OnRight() {
            rightButtonHandler.Invoke();
        }

        public void OnPauseGame() {
            panelController.FadeIn(panelController.fadePanel,0.3f ,0.7f);
            panelController.FadeIn(panelController.pausePanel, 0.3f, 1f);
            pauseButtonHandler.Invoke();
        }

        public void OnRestartGame() {
            resetartButtonHandler.Invoke();
            panelController.FadeOut(panelController.fadePanel, 0.1f, 0f);
            panelController.FadeOut(panelController.pausePanel, 0.1f, 0f); 
        }

        public void OnQuit() {
            SceneManager.LoadScene("HomeScene");
        }

        public void ResetAction() {
            leftButtonHandler = null;
            rightButtonHandler = null;
        }
    }
}
