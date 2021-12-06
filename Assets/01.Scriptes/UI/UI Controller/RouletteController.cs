using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DesignStruct;
using UnityEngine.EventSystems;
using Scene;
using System;
namespace GameUI.Controller {
    public class RouletteController : MonoBehaviour {
        [SerializeField]
        private GameScene scene; 
        [SerializeField]
        private CanvasGroup canvasGroup;
        [SerializeField]
        private PanelController panelCtrl;
        [SerializeField]
        private AbilityManager abilityManager;
        [SerializeField]
        private ObjectPool imgPool;
        [SerializeField]
        private RectTransform[] imgParents;
        [SerializeField]
        private Text[] texts; 

        private List<List<Ability>> lineList = new List<List<Ability>>();
        private List<List<GameObject>> lineObjList = new List<List<GameObject>>();
        private List<ObjectPoolItem> itemList = new List<ObjectPoolItem>();
        private bool isSelected = false;

        public Action selectCallback;
        private void Awake() {
            Init();
        }
        private void Init() {
            for(int i = 0; i < 3; i++) {
                lineList.Add(new List<Ability>());
                lineObjList.Add(new List<GameObject>());
            }
        }
        public void Roulette() {
            scene.PauseGame();
            isSelected = false;
            panelCtrl.FadeIn(0.5f, 0.7f);
            panelCtrl.FadeIn(canvasGroup.gameObject, 0.5f, 1f);

            SoundManager.Play_EFF("R_S");

            for(int i =0;i< 3;i++) {
                for(int y = 0; y < (30 + i); y++) {
                    Ability abili = abilityManager.abilityList[UnityEngine.Random.Range(0, abilityManager.abilityList.Count)];
                    RectTransform obj = CreateRouletImg(abili);
                    AddAbili(i, abili, obj.gameObject);
                    SetPosition(obj, i, y);
                    itemList.Add(obj.GetComponent<ObjectPoolItem>());
                }
            }
            StartCoroutine(MoveAllLine());
        }
        private IEnumerator MoveAllLine() {
            while(true) {
                for(int i =0; i < 3; i++) {
                    if(lineObjList[i][lineObjList[i].Count - 1].transform.localPosition.y > 0) {
                        for(int y = 0; y < lineObjList[i].Count; y++) {
                            if(lineObjList[i][y].transform.localPosition.y < 150 && lineObjList[i][y].transform.localPosition.y > -150) {
                                SetText(i, lineList[i][y].name );
                            }
                            lineObjList[i][y].transform.Translate(Vector3.down * Time.deltaTime * 17f, Space.World);
                        }
                    } else {
                        lineObjList[i][lineObjList[i].Count - 1].transform.localPosition = Vector3.zero;
                        if(i == 2) {
                            AddAbilityButton();
                            break;
                        }
                    }
                }
                yield return null;
            }
        }

        private void AddAbilityButton() {
            for(int i = 0; i < 3; i++) {
                EventTrigger trigger = imgParents[i].GetComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerDown;
                int index = i;
                entry.callback.AddListener((eventData) => {
                    AbilityClick(index, trigger.gameObject); });
                trigger.triggers.Add(entry);
            }
        }

        private void AbilityClick(int index, GameObject obj) {
            if(!isSelected) {
                SoundManager.Play_EFF("S_S");
                scene.RestartGame();
                obj.Add_UI_Animation().Click();
                abilityManager.AddAbility(lineList[index][lineList[index].Count - 1]);
                panelCtrl.FadeOut(0.2f, 0f);
                panelCtrl.FadeOut(canvasGroup.gameObject, 0.2f, 0f);
                for(int i = 0; i < itemList.Count; i++) {
                    itemList[i].ReturnObject();
                }
                itemList.Clear();
                if(selectCallback != null) selectCallback.Invoke();
                isSelected = true;
            }
        }

        private void SetText(int line, string name) {
            texts[line].text = name;
        }


        private void AddAbili(int line, Ability abili, GameObject obj) {
            lineList[line].Add(abili);
            lineObjList[line].Add(obj);
        }

        private RectTransform CreateRouletImg(Ability ability) {
            Image img = imgPool.TakeObject<Image>();
            img.sprite = abilityManager.GetSpirte(ability);
            return img.rectTransform;
        }

        private void SetPosition(RectTransform obj, int lineNumber,int y) {
            obj.SetParent(imgParents[lineNumber]);
            obj.localScale = Vector3.one;
            obj.localPosition = new Vector3(0, 180 * y, 0);
        }


    }
}