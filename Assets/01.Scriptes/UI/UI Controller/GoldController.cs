using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UI_Controller {
    public class GoldController : MonoBehaviour {
        private Text textGold;
        private void Start() {
            textGold = GetComponent<Text>();
            RenewGold();
            GameManager.instance.gold.addAction += RenewGold;
            GameManager.instance.gold.consumeAction += RenewGold;
        }

        private void RenewGold() {
            textGold.text = GameManager.instance.gold.GetGold().ToString();
        }
    }
}
