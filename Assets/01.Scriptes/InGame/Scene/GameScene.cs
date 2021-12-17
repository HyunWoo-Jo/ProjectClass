///작성일 21.09.26
///작성자 조현우
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignStruct;
using Spawner;
using GameUI.Controller;
using GameUI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Scene {
    public class GameScene : MonoBehaviour {
        #region Property

        public Canvas mainCavas;
        [Header("Controller/ Manager")]
        [SerializeField]
        private ButtonController buttonController;
        [SerializeField]
        private PanelController panelController;
        [SerializeField]
        private RouletteController rouletteController;
        [SerializeField]
        private AbilityManager abilityManager;

        [SerializeField]
        private FX_Manager fx_Manager;

        public MonsterManager monManager = null; //2021.11.07 변경

        [Header("UI_Controllers")]
        [SerializeField]
        private StageController UI_stageController;
        [SerializeField]
        private GameObject allPanelsParent;

        [Header("Block")]
        [SerializeField]
        private RectTransform blockParent;

        public List<Block> blockListInField = new List<Block>();

        [SerializeField]
        private BlockSpawner blockSpawner;

        [SerializeField]
        private Transform blockTarget;

        [HideInInspector]
        public bool isPause = false;

        private bool isLose = false;
        private int currentStage = 1; // 임시
        private float blockDistance = 1.0f;
        private int blockDirectionCount = 2; // 2 = LEFT, RIGHT / 3 = ... + Center

        [SerializeField] private Vector3 blockScale = Vector3.one;
        [SerializeField] private int blockSpawnSize = 3;
        public List<Color> leftColorList = new List<Color>();
        public List<Color> centerColorList = new List<Color>();
        public List<Color> rightColorList = new List<Color>();

        
        public Combo playerCombo = new Combo();

        

        public Action sucessHandler;
        public Action failHandler;
        #endregion

        #region Unity Method
        private void Awake() {
            Init();
            Player.instance.ResetMaxHp();
        }

        IEnumerator Start() {
            panelController.fadePanel.AddFadeUI().SetAlpha(1f);
            panelController.FadeOut(1f, 0);
            isPause = true;
            yield return new WaitForSeconds(1f);
            rouletteController.Roulette();
        }

#if UNITY_EDITOR
        /// <summary>
        /// Text Cord
        /// </summary>
        private void Update() {
            if(Input.GetKeyDown(KeyCode.LeftArrow)) {
                LeftButton();
            }
            if(Input.GetKeyDown(KeyCode.RightArrow)) {
                RightButton();
            }
            if(Input.GetKeyDown(KeyCode.DownArrow)) {
                CenterButton();
            }
        }
#endif
        #endregion

        public IEnumerator TestAddButton() {
            //MonsterManager에서 이용중입니다.
            yield return new WaitForSeconds(3f);
            AddButtonCeneter();
            yield return new WaitForSeconds(10);
            DeleteButtonCenter();
        }

        private void AddButtonCeneter() {
            blockDirectionCount = 3;
            Popup<SpeechPopup>.InstancePopup(mainCavas.gameObject).AutoKill(3f).SetText("버튼 증가");
            buttonController.AddCenterButton();
        }

        private void DeleteButtonCenter() {
            blockDirectionCount = 2;
            for(int i = blockListInField.Count - 1; i >= 0; i--) {
                Block block = blockListInField[i];
                if(block.direction.Equals(BlockDirection.Center)) {
                    ReturnBlock(i);
                    SpawnBlock();
                }
            } 
            buttonController.DeleteCenterButton();
        }
        #region GameLogic
        /// <summary>
        /// 초기화
        /// </summary>
        private void Init() {
            buttonController.leftButtonHandler += LeftButton;
            buttonController.rightButtonHandler += RightButton;
            buttonController.centerButtonHandler += CenterButton;
            
            buttonController.pauseButtonHandler += PauseGame;
            buttonController.resetartButtonHandler += RestartGame;

            rouletteController.selectCallback += BlockCreate;

            sucessHandler += Succes;
            failHandler += Fail;
        }
        /// <summary>
        /// 게임 시작
        /// </summary>

        private void BlockCreate() {
            monManager.SpawnNextStageMonster();
            for(int i = 0; i < blockSpawnSize; i++) {
                SpawnBlock();
            }
        }

        private void NextStage() {
            GameManager.instance.SaveClearStage(currentStage); //작성자 장용진 : 2021.12.17 추가
            currentStage++;            
            ShowAndSetStageUI();
            ReturnBlock();
            SoundManager.Play_EFF("Footsteps_MetalV1_Walk_03");
            TweenManager.Add(
                LeanTween.delayedCall(2f, () => {
                    ShowAndSetStageUI();
                    rouletteController.Roulette();
                }
                ));
        }
        /// <summary>
        /// 몬스터가 모두 제거 되었나 확인
        /// </summary>
        /// <returns>true = clear, false = not claer </returns>
        public bool ChkNextStage() {
            //작성자 장용진 : 2021.12.16 변경
            //독뎀 사망시 추가 호출
            if(monManager.GetMonsterList().Count <= 0) {
                NextStage();
                return true;
            }
            return false;
        }
                

        public void PauseGame() {
            isPause = true;
            TweenManager.Pause();
            monManager.SetPause(isPause);
        }
        
        public void RestartGame() {
            isPause = false;
            TweenManager.Resume();
            monManager.SetPause(isPause);
        }

        public void GameOver() {
            isPause = true;
            monManager.SetPause(isPause);
            if (!isLose) {
                isLose = true;
                TweenManager.Pause();
                SoundManager.Play_EFF("stinger_lose");
                panelController.FadeIn(panelController.gameOverPanel, 0.2f, 1f);
                panelController.FadeIn(0.2f, 1f);
            }
        }

       
       
        #endregion

        #region Block Logic
        /// <summary>
        /// 블록 생성
        /// </summary>
        private Block SpawnBlock() {
            GameObject obj = blockSpawner.SpawnBlock();
            Block block = obj.GetComponent<Block>();
            blockListInField.Add(block);
            SetBlock(block);
            BlockMove2Target();
            return block;
        }

        /// <summary>
        /// 블록의 방향을 결정
        /// </summary>
        /// <param name="block"></param>
        private void SetBlock(Block block) {
            block.transform.SetParent(blockParent.transform);
            block.transform.localScale = Vector3.one;
            int random = UnityEngine.Random.Range(0, blockDirectionCount);
            if (random == 0) {
                block.direction = BlockDirection.LEFT;
            } else if(random == 1) {
                block.direction = BlockDirection.RIGHT;
            } else {
                block.direction = BlockDirection.Center;
            }
            SetBlockColor(block);
        }
        /// <summary>
        /// 블록 방향에 따라 컬러 설정
        /// </summary>
        /// <param name="block"></param>
        private void SetBlockColor(Block block) {
            Color color = Color.black;
            switch(block.direction) {
                case BlockDirection.LEFT:
                    color = leftColorList[UnityEngine.Random.Range(0, leftColorList.Count)];
                    break;
                case BlockDirection.Center:
                    color = centerColorList[UnityEngine.Random.Range(0, centerColorList.Count)];
                    break;
                case BlockDirection.RIGHT:
                    color = rightColorList[UnityEngine.Random.Range(0, rightColorList.Count)];
                    break;
            }
            block.SetColor(color);
        }
        /// <summary>
        /// index 블록 반환
        /// </summary>
        /// <param name="index"></param>
        private void ReturnBlock(int index) {

            Block block = blockListInField[index];
            ObjectPoolItem poolItem = block.GetComponent<ObjectPoolItem>();
            blockListInField.RemoveAt(index);
            block.Anim(Block_Animation_Trigger.ScaleDown);
            poolItem.DelayReturn(0.1f);
        }
        /// <summary>
        /// 모든 블록 반환
        /// </summary>
        private void ReturnBlock() {
            
            while(blockListInField.Count != 0) {
                ReturnBlock(0);
            }
        }

        private void SetBlockPos() {
            for (int i = 0; i < blockListInField.Count; i++) {
                Vector3 pos = blockListInField[i].transform.position - blockTarget.transform.position;
                Vector3 targetPos = blockTarget.transform.position + (pos.normalized * i);
                blockListInField[i].gameObject.transform.position = targetPos;
            }
        }
        private void BlockMove2Target() {
            for (int i = 0; i < blockListInField.Count; i++) {
                Vector3 pos = blockListInField[i].transform.position - blockTarget.transform.position;
                Vector3 targetPos = blockTarget.transform.position + (pos.normalized * i * blockDistance);
                LeanTween.move(blockListInField[i].gameObject, targetPos, 0.1f)
                    .setEase(LeanTweenType.easeInQuad); 
            }
        }
        
        #endregion
        #region Button Logic
        /// <summary>
        /// 왼쪽 버튼 클릭 로직
        /// </summary>
        private void LeftButton() {
            if (isPause) return;
            if (blockListInField.Count == 0) return;
            SoundManager.Play_EFF("BlockButton");
            if (blockListInField[0].direction.Equals(BlockDirection.LEFT)) {
                sucessHandler.Invoke();
                SoundManager.Play_EFF("SWORD_01");
            } else {
                failHandler.Invoke();
            }
        }

        /// <summary>
        /// 오른쪽 버튼 클릭 로직
        /// </summary>
        private void RightButton() {
            if (isPause) return;
            if (blockListInField.Count == 0) return;
            SoundManager.Play_EFF("BlockButton");
            if (blockListInField[0].direction.Equals(BlockDirection.RIGHT)) {
                sucessHandler.Invoke();
                SoundManager.Play_EFF("SWORD_02");
            } else {
                failHandler.Invoke();
            }
        }

        private void CenterButton() {
            if(isPause) return;
            if(blockListInField.Count == 0) return;
            SoundManager.Play_EFF("BlockButton");
            if(blockListInField[0].direction.Equals(BlockDirection.Center)) {
                sucessHandler.Invoke();
                SoundManager.instance.PlayEFF("SWORD_05");
            } else {
                failHandler.Invoke();
            }
        }
        /// <summary>
        /// 같은 색 버튼 클릭 성공
        /// </summary>
        private void Succes() {
            
            playerCombo.AddCombo(1);

            //playerCombo.GetComboDamage(); Player Damage * Combo Damage 배율 / 리턴값 float
            if(monManager != null) {
                monManager.GetDamage(playerCombo.GetComboDamage(),
                    abilityManager.poisonCount, abilityManager.lightningCount, abilityManager.freezingCount);
                //Lightning();
                //if(ChkNextStage()) {
                //    NextStage();
                //    return;
                //}
                if (ChkNextStage()) return;
            }

            ReturnBlock(0);
            SpawnBlock();        
        }

        

        /// <summary>
        /// 같은 색 버튼 클릭 실패
        /// </summary>
        private void Fail() {
            playerCombo.ResetCombo();
            Player.instance.CurrentHp -= 50.0f; // 임시 테스트용 코드
            SoundManager.instance.PlayEFF("Hit_Player");
        }
        #endregion
        #region EFX
        private void Lightning() {
            if(abilityManager.lightningCount <= 0) return;
            if(monManager != null) {
                var monList = monManager.GetMonsterList();
                if(monList.Count >= 0) {
                    for(int i = 1; i < monList.Count; i++) {
                        fx_Manager.Lightning(blockTarget, monList[i].transform);
                    }
                }
            }
        }
        #endregion

        #region UI

        private void ShowAndSetStageUI() {
            UI_stageController.SetStageText(currentStage);
            UI_stageController.FlashText();
        } 

        #endregion
    }
}