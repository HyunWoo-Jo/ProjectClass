using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUI {
    public class FadeUtills : MonoBehaviour {
        [SerializeField]
        CanvasGroup canvasGroup;

        private void Awake() {
            Init();
        }

        public void Init() {
            if(canvasGroup != null) return;
            canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
            if(canvasGroup == null) {
                canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
            }
            canvasGroup.alpha = 0f;
        }

        public void SetAlpha(float value) {
            canvasGroup.alpha = value;
        }
        public void FadeOut(float time, float alhpa) {
            canvasGroup.gameObject.SetActive(true);
            var tween = LeanTween.alphaCanvas(canvasGroup, alhpa, time);
            tween.setOnComplete(() => { canvasGroup.gameObject.SetActive(false); });
        }

        public void FadeIn(float time, float alhpa) {
            canvasGroup.gameObject.SetActive(true);
            var tween = LeanTween.alphaCanvas(canvasGroup, alhpa, time);
        }
    }
    public static class Utils {
        public static FadeUtills AddFadeUI(this GameObject obj) {
            FadeUtills fade = obj.GetComponent<FadeUtills>();
            if(fade == null) {
                fade = obj.AddComponent<FadeUtills>();
            }
            fade.Init();
            return fade;
        }
    }

}