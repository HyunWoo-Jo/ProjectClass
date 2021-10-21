///�ۼ��� 21.09.26
///���� ������ 21.10.05
///�ۼ��� ������
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignStruct;
using Spawner;
using UI_Controller;
namespace Scene {
    public class GameScene : MonoBehaviour {
        #region Property

        [HideInInspector]
        public List<Block> blockListInField = new List<Block>();

        [SerializeField]
        private BlockSpawner blockSpawner;
        [SerializeField]
        private ButtonController buttonController;

        [SerializeField]
        private Transform blockTarget;

        [HideInInspector]
        public bool isPause = false;

        private int maxStage = 5; // �ӽ�
        private int currentStage = 0; // �ӽ�
        private float blockDistance = 1.0f;

        public List<Color> leftColorList = new List<Color>();
        public List<Color> rightColorList = new List<Color>();

        public Monster monster; //2021.10.03 �߰�
        public Combo playerCombo = new Combo();

        

        public Action sucessHandler;
        public Action failHandler;

        #region Controllers;
        [Header("UI_Controllers")]
        [SerializeField]
        private StageController UI_stageController;
        #endregion

        #endregion

        #region Unity Method
        private void Awake() {
            Init();
        }

        private void Start() {
            for (int i = 0; i < 3; i++) {
                Block block = SpawnBlock();
            }
            SetBlockPos();
            GameStart();
        }
        #endregion

        #region GameLogic
        /// <summary>
        /// �ʱ�ȭ
        /// </summary>
        private void Init() {
            buttonController.leftButtonHandler += LeftButton;
            buttonController.rightButtonHandler += RightButton;
            buttonController.pauseButtonHandler += PauseGame;
            buttonController.resetartButtonHandler += RestartGame;


            sucessHandler += Succes;
            failHandler += Fail;
        }
        /// <summary>
        /// ���� ����
        /// </summary>
        private void ResetGame() {
            TweenManager.Clear();
            while (blockListInField.Count != 0) {
                ReturnBlock();
                playerCombo.ResetCombo();
            }
            GameStart();
        }
        /// <summary>
        /// ���� ����
        /// </summary>
        private void GameStart() {
            ShowAndSetStageUI();
        }

        private void NextStage() {
            currentStage++;
            ShowAndSetStageUI();
        }

        public void PauseGame() {
            isPause = true;
            TweenManager.Pause();
        }
        
        public void RestartGame() {
            isPause = false;
            TweenManager.Resume();
        }
        #endregion

        #region Block Logic
        /// <summary>
        /// ��� ����
        /// </summary>
        private Block SpawnBlock() {
            GameObject obj = blockSpawner.SpawnBlock();
            Block block = obj.GetComponent<Block>();
            blockListInField.Add(block);
            SetBlock(block);
            return block;
        }

        /// <summary>
        /// ����� ������ ����
        /// </summary>
        /// <param name="block"></param>
        private void SetBlock(Block block) {
            int random = UnityEngine.Random.Range(0, 2);
            if (random == 0) {
                block.direction = BlockDirection.LEFT;
            } else {
                block.direction = BlockDirection.RIGHT;
            }
            SetBlockColor(block);
        }
        /// <summary>
        /// ��� ���⿡ ���� �÷� ����
        /// </summary>
        /// <param name="block"></param>
        private void SetBlockColor(Block block) {
            Color color;
            if (block.direction.Equals(BlockDirection.LEFT)) {
                color = leftColorList[UnityEngine.Random.Range(0, leftColorList.Count)];
            } else {
                color = rightColorList[UnityEngine.Random.Range(0, rightColorList.Count)];
            }
            block.SetColor(color);
        }

        private void ReturnBlock() {
            blockListInField[0].GetComponent<ObjectPoolItem>().ReturnObject();
            blockListInField.RemoveAt(0);
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
                var tween = LeanTween.move(blockListInField[i].gameObject, targetPos, 0.1f); 
                tween.setEase(LeanTweenType.easeInQuad);
            }
        }
        
        #endregion
        #region Button Logic
        /// <summary>
        /// ���� ��ư Ŭ�� ����
        /// </summary>
        private void LeftButton() {
            if (isPause) return;
            if (blockListInField.Count == 0) return;
            if (blockListInField[0].direction.Equals(BlockDirection.LEFT)) {
                sucessHandler.Invoke();
            } else {
                failHandler.Invoke();
            }
        }

        /// <summary>
        /// ������ ��ư Ŭ�� ����
        /// </summary>
        private void RightButton() {
            if (isPause) return;
            if (blockListInField.Count == 0) return;
            if (blockListInField[0].direction.Equals(BlockDirection.RIGHT)) {
                sucessHandler.Invoke();
            } else {
                failHandler.Invoke();
            }
        }
        /// <summary>
        /// ���� �� ��ư Ŭ�� ����
        /// </summary>
        private void Succes() {

            playerCombo.AddCombo(1);

            //playerCombo.GetComboDamage(); Player Damage * Combo Damage ���� / ���ϰ� float
            monster.GetDamage(10f); //�ӽ� 2021.10.03 �߰�

            ReturnBlock();
            SpawnBlock();
            BlockMove2Target();
        }

        /// <summary>
        /// ���� �� ��ư Ŭ�� ����
        /// </summary>
        private void Fail() {
            playerCombo.ResetCombo();
            Player.instance.CurrentHp -= 50.0f; // �ӽ� �׽�Ʈ�� �ڵ�
        }
        #endregion
        #region UI

        private void ShowAndSetStageUI() {
            UI_stageController.SetStageText(currentStage, maxStage);
            UI_stageController.FlashText();
        }

        #endregion
    }
}