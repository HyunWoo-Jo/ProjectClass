///작성일 21.09.25
///작성자 조현우
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignStruct;

namespace Spawner {
    public class BlockSpawner : MonoBehaviour {
        [SerializeField]
        private Transform spawnPosition;
        [SerializeField]
        private ObjectPool pool;

        public GameObject SpawnBlock() {
            GameObject obj = pool.TakeObject();
            obj.transform.position = spawnPosition.position;
            return obj;
        }

        void SetSpawnerPosition(Vector3 position) {
            spawnPosition.position = position;
        }

    }
}
