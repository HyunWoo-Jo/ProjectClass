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

        private Animator anim;

        private List<List<Ability>> lineList = new List<List<Ability>>();
        private List<List<GameObject>> lineObjList = new List<List<GameObject>>();
        private List<ObjectPoolItem> itemList = new List<ObjectPoolItem>();
        private bool isSelected = false;

        public Action selectCallback;

        //장용진 : 2021.12.10 추가
        private AudioSource temAudio = null;

        private void Awake() {
            Init();
           
        }
        private void Init() {
            for(int i = 0; i < 3; i++) {
                lineList.Add(new List<Ability>());
                lineObjList.Add(new List<GameObject>());
            }
            anim = canvasGroup.GetComponent<Animator>();
        }
        public void Roulette() {
            scene.PauseGame();
            isSelected = false;
            temAudio = SoundManager.Play_EFF("R_S");
            canvasGroup.gameObject.SetActive(true);
            SizeReset();
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
        
        private void SizeReset() {
            for(int i =0;i < 3; i++) {
                imgParents[i].localScale = Vector3.one;
            }
        }
        
        private IEnumerator MoveAllLine() {
            bool isEnd = false;
            while(!isEnd) {
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
                            isEnd = true;
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
                if(temAudio != null)
                {//장용진 : 2021.12.10 추가
                    temAudio.Stop();
                    temAudio = null;
                }
                scene.RestartGame();
                obj.Add_UI_Animation().Click();
                abilityManager.AddAbility(lineList[index][lineList[index].Count - 1]);
                anim.SetTrigger("AlphaDown");
                imgParents[index].GetComponent<Animation>().Play();
                StartCoroutine(DelayOffObject());
                for(int i = 0; i < itemList.Count; i++) {
                    itemList[i].ReturnObject();
                }
                itemList.Clear();
                Clear();
                if(selectCallback != null) selectCallback.Invoke();
                isSelected = true;
            }
        }
        private IEnumerator DelayOffObject() {
            yield return new WaitForSeconds(0.2f);
            canvasGroup.gameObject.SetActive(false);
        }

        private void SetText(int line, string name) {
            texts[line].text = name;
        }

        private void Clear() {
            for(int i =0;i < 3; i++) {
                EventTrigger trigger = imgParents[i].GetComponent<EventTrigger>();
                trigger.triggers.Clear();
                lineList[i].Clear();
                lineObjList[i].Clear();
            }
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