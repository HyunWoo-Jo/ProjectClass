///작성일 21.10.21
///작성자 조현우
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI_Controller {
    public class StageController : MonoBehaviour {
        [SerializeField]
        private Text stageText;
        private void Awake() {
            stageText.color = Color.clear;
        }

        public void SetStageText(int currentStage, int maxStage) {
            string str = string.Format("Stage {0} / {1}", currentStage, maxStage);
            stageText.text = str;
        }

        public void FlashText(float delay = 1.0f) {
            ShowText();
            DelayText(delay);
        }

        private void ShowText() {
            var tween = LeanTween.colorText(stageText.rectTransform, Color.black, 0.5f);
            TweenManager.Add(tween);
        }

        private void DelayText(float delay) {
            var tween = LeanTween.delayedCall(this.gameObject, delay, () => { HideText();});
            tween.setCallback();
            TweenManager.Add(tween);
        }

        private void HideText() {
            var tween = LeanTween.colorText(stageText.rectTransform, Color.clear, 0.5f);
            TweenManager.Add(tween);
        }

    }
}
