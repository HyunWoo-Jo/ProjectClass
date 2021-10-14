///작성일 21.09.25
///작성자 조현우
using UnityEngine;
using System;

namespace UI_Controller {
    public class ButtonController : MonoBehaviour {
        private Action leftHandler;
        private Action rightHandler;

        public void OnLeft() {
            leftHandler.Invoke();
        }
        public void OnRight() {
            rightHandler.Invoke();
        }

        public void AddLeftCallback(Action action) {
            leftHandler += action;
        }

        public void AddRightCallback(Action action) {
            rightHandler += action;
        }

        public void ResetAction() {
            leftHandler = null;
            rightHandler = null;
        }
    }
}
