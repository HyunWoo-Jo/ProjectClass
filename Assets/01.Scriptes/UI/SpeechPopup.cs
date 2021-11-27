using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace GameUI {
    public class SpeechPopup : Popup<SpeechPopup> {

        [SerializeField]
        private Text text;

        public SpeechPopup SetText(string str) {
            text.text = str;
            return this;
        }

    }
}
