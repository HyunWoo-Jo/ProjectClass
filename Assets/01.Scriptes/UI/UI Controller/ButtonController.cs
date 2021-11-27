///작성일 21.09.25
///작성자 조현우
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
namespace GameUI.Controller {
    public class ButtonController : MonoBehaviour {
        [SerializeField]
        private PanelController panelController;

        public Action leftButtonHandler;
        public Action rightButtonHandler;
        public Action centerButtonHandler;

        public Action pauseButtonHandler;
        public Action resetartButtonHandler;

        public GameObject leftButton;
        public GameObject rightButton;
        public GameObject centerButton;

        private bool isAction = false;
        public void AddCenterButton() {
            centerButton.SetActive(true);
            centerButton.transform.localScale = Vector3.zero;
            TweenManager.Add(
                LeanTween.scale(centerButton, Vector3.one, 0.3f).
                setEase(LeanTweenType.easeInOutBack)
                );
            TweenManager.Add(
                LeanTween.scale(leftButton, Vector3.one, 0.3f).
                setEase(LeanTweenType.easeOutBack)
                );
            TweenManager.Add(
                LeanTween.scale(rightButton, Vector3.one, 0.3f).
                setEase(LeanTweenType.easeOutBack)
                );
            TweenManager.Add(
                LeanTween.moveLocalX(leftButton, -220f, 0.3f)
                );
            TweenManager.Add(
                LeanTween.moveLocalX(rightButton, 220f, 0.3f)
                );

        }
        public void DeleteCenterButton() {
            TweenManager.Add(
                LeanTween.scale(centerButton, Vector3.zero, 0.2f).
                setEase(LeanTweenType.easeInOutBack).
                setOnComplete(() => { centerButton.SetActive(false); })
                );
            TweenManager.Add(
                LeanTween.scale(leftButton, new Vector3(1.4f,1f,1f), 0.3f).
                setEase(LeanTweenType.easeOutBack)
                );
            TweenManager.Add(
                LeanTween.scale(rightButton, new Vector3(1.4f, 1f, 1f), 0.3f).
                setEase(LeanTweenType.easeOutBack)
                );
            TweenManager.Add(
                LeanTween.moveLocalX(leftButton, -200f, 0.3f)
                );
            TweenManager.Add(
                LeanTween.moveLocalX(rightButton, 200f, 0.3f)
                );
        }

        public void OnLeft() {
            leftButtonHandler.Invoke();
        }
        public void OnRight() {
            rightButtonHandler.Invoke();
        }

        public void OnCenter() {
            centerButtonHandler.Invoke();
        }

        public void OnPauseGame() {
            SoundManager.Play_EFF("Button");
            panelController.FadeIn(panelController.fadePanel,0.3f ,0.7f);
            panelController.pausePanel.SetActive(true);
            panelController.pausePanel.Add_UI_Animation().Play(UI_Animation_Type.ScrollUpFromUnder, 0.3f);
            pauseButtonHandler.Invoke();
        }
        public void OnRestartGame() {
            if(isAction) return;
            isAction = true;
            SoundManager.Play_EFF("Button");
            resetartButtonHandler.Invoke();
            panelController.FadeOut(panelController.fadePanel, 0.1f, 0f);
            panelController.pausePanel.Add_UI_Animation().Play(UI_Animation_Type.ScrollUpFromCenter, 0.3f, () => { 
                panelController.pausePanel.SetActive(false);
                isAction = false;
            });
           
            //panelController.FadeOut(panelController.pausePanel, 0.1f, 0f);
        }

        public void OnQuit() {
            if(isAction) return;
            SceneManager.LoadScene("HomeScene");
        }
        public void OnResetGame() {
            SceneManager.LoadScene("GameScene");
        }

        public void ResetAction() {
            leftButtonHandler = null;
            rightButtonHandler = null;
        }
    }
}
