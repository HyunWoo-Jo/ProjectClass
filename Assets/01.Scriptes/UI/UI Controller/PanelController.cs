///작성일 21.10,21
///작성자 조현우
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUI.Controller;
using UnityEngine.UI;

namespace GameUI.Controller {
    public class PanelController : MonoBehaviour {
        public GameObject fadePanel;
        public GameObject midPanel;
        public GameObject frontPanel;
        public GameObject pausePanel;

        public void FadeIn(float time, float alhpa) {
            FadeIn(fadePanel, time, alhpa);
        }

        public void FadeIn(GameObject panel, float time, float alhpa) {
            panel.SetActive(true);
            var tween = LeanTween.alphaCanvas(panel.GetComponent<CanvasGroup>(), alhpa, time);
        }
        public void FadeOut(float time, float alhpa) {
            FadeOut(fadePanel, time, alhpa);
        }

        public void FadeOut(GameObject panel, float time, float alhpa) {
            var tween = LeanTween.alphaCanvas(panel.GetComponent<CanvasGroup>(), alhpa, time);
            tween.setOnComplete(() => { panel.SetActive(false); });
        }


    }
}