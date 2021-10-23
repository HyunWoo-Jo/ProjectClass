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
            GameManager.instance.gold.addHandler += RenewGold;
            GameManager.instance.gold.consumeHandler += RenewGold;
        }

        private void OnDestroy() {
            GameManager.instance.gold.addHandler -= RenewGold;
            GameManager.instance.gold.consumeHandler -= RenewGold;
        }

        private void RenewGold() {
            textGold.text = GameManager.instance.gold.GetGold().ToString();
        }
    }
}
