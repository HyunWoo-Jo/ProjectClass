///�ۼ��� 21.10,07
///�ۼ��� ������
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace UI_Controller {
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
            SetText(slot);
        }
        private void FailReinfoce() {

        }
    }
}