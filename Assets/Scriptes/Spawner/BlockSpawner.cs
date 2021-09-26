///작성일 21.09.25
///작성자 조현우
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPosition;
    [SerializeField]
    private GameObject createObject;

    void SpawnBlock() {
         
    }

    void SetColorBlock(Color color) {
       
    }

    void SetSpawnerPosition (Vector3 position) {
        spawnPosition.position = position;
    }
}
