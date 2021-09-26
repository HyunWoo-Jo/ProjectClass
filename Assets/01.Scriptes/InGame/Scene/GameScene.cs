///작성일 21.09.26
///작성자 조현우
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignStruct;
using Spawner;
using UI_Controller;
namespace Scene {
    public class GameScene : MonoBehaviour {
        [HideInInspector]
        public List<Block> blockListInField = new List<Block>();

        [SerializeField]
        private BlockSpawner blockSpawner;
        [SerializeField]
        private ButtonController buttonController;

        [SerializeField]
        private Transform blockTarget;

        public List<Color> leftColorList = new List<Color>();
        public List<Color> rightColorList = new List<Color>();

        private void Awake() {
            Init();

        }

        private void Start() {
            for (int i = 0; i < 3; i++) {
                Block block = SpawnBlock();
            }
            SetBlockPos();
        }

        /// <summary>
        /// 초기화
        /// </summary>
        private void Init() {
            buttonController.AddLeftAction(LeftButton);
            buttonController.AddRightAction(RightButton);
        }
        /// <summary>
        /// 게임 리셋
        /// </summary>
        private void ResetGame() {
            while (blockListInField.Count != 0) {
                ReturnBlock();
            }
        }
        /// <summary>
        /// 게임 시작
        /// </summary>
        private void GameStart() {

        }
        /// <summary>
        /// 블록 생성
        /// </summary>
        private Block SpawnBlock() {
            GameObject obj = blockSpawner.SpawnBlock();
            Block block = obj.GetComponent<Block>();
            blockListInField.Add(block);
            SetBlock(block);
            return block;
        }

        /// <summary>
        /// 블록의 방향을 결정
        /// </summary>
        /// <param name="block"></param>
        private void SetBlock(Block block) {
            int random = UnityEngine.Random.Range(0, 2);
            if(random == 0) {
                block.direction = BlockDirection.LEFT;
            } else {
                block.direction = BlockDirection.RIGHT;
            }
            SetBlockColor(block);
        }
        /// <summary>
        /// 블록 방향에 따라 컬러 설정
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
        /// <summary>
        /// 왼쪽 버튼 클릭 로직
        /// </summary>
        private void LeftButton() {
            if (blockListInField.Count == 0) return;
            if (blockListInField[0].direction.Equals(BlockDirection.LEFT)) {
                SuccesButton();
            } else {
                FailButton();
            }
        }

        /// <summary>
        /// 오른쪽 버튼 클릭 로직
        /// </summary>
        private void RightButton() {
            if (blockListInField.Count == 0) return;
            if (blockListInField[0].direction.Equals(BlockDirection.RIGHT)) {
                SuccesButton();
            } else {
                FailButton();
            }
        }
        /// <summary>
        /// 같은 색 버튼 클릭 성공
        /// </summary>
        private void SuccesButton() {

            ReturnBlock();
            SpawnBlock();
            BlockMove2Target();
        }

        /// <summary>
        /// 같은 색 버튼 클릭 실패
        /// </summary>
        private void FailButton() {

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
                Vector3 targetPos = blockTarget.transform.position + (pos.normalized * i);
                var tween = LeanTween.move(blockListInField[i].gameObject, targetPos, 0.1f);
                tween.setEase(LeanTweenType.easeInQuad);
               
            }
        }
    }
}