///작성일 21.10,07
///작성자 조현우
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace GameUI.Controller {
    public class ReinforcementController : MonoBehaviour {
        [SerializeField]
        private Reinforcement reinforcement;
        private ReinforceSlot[] slotList;

        private void Start() {
            slotList = GetComponentsInChildren<ReinforceSlot>();
            SetAllTexts();
        }


        private void SetAllTexts() {
            foreach(var item in slotList) {
                SetText(item);
            }
        }
        private void SetText(ReinforceSlot slot) {
            slot.SetGold(reinforcement.GetReinfoceGold(slot.stat));
            slot.SetText(reinforcement.GetReinfoceStat(slot.stat).ToString());
        }

        public void OnReinfoce(ReinforceSlot slot) {
            int gold = reinforcement.GetReinfoceGold(slot.stat);
            if (GameManager.instance.gold.ConsumeGold(gold)) {
                SuccessReinfoce(slot);
            } else {
                FailReinfoce();
            }
        }
        private void SuccessReinfoce(ReinforceSlot slot) {
            reinforcement.level[slot.stat]++;
            reinforcement.SetReinfoce();
            SoundManager.Play_EFF("Reinforce");
            SetText(slot);
            reinforcement.SaveReinforce();
            slot.gameObject.Add_UI_Animation().Click();
        }
        private void FailReinfoce() {

        }
    }
}
