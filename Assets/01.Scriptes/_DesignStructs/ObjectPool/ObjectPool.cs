///작성일 21.09.26
///작성자 조현우

using System.Collections.Generic;
using UnityEngine;

namespace DesignStruct {
    public class ObjectPool : MonoBehaviour {
        public GameObject poolObject;

        private List<GameObject> poolObjectList;
        [SerializeField]
        private bool isAutoInstance = true;
        [SerializeField]
        private int instanceCount = 5;


        private void Awake() {
            Init();
        }
        private void Init() {
            poolObjectList = new List<GameObject>();
            if (isAutoInstance) {
                AddObject(instanceCount);
            }
        }

        private void AddObject(int count) {
            for(int i = 0; i < count; i++) {
                GameObject obj = Instantiate(poolObject);
                obj.transform.SetParent(this.transform);
                obj.gameObject.name += poolObjectList.Count.ToString();
                poolObjectList.Add(obj);
                obj.AddComponent<ObjectPoolItem>().Init(this);
                obj.SetActive(false);
            }
        }

        public GameObject TakeObject() {
            if (poolObjectList.Count <= 0) {
                AddObject(1);
            }
            GameObject obj = poolObjectList[0];
            poolObjectList.RemoveAt(0);
            obj.SetActive(true);
            return obj;
        }
        public T TakeObject<T>() {
            GameObject obj = TakeObject();
            return obj.GetComponent<T>();
        }
         
        public void ReturnObject(GameObject poolObject) {
            poolObjectList.Add(poolObject);
            poolObject.transform.SetParent(this.transform);
            poolObject.SetActive(false);
        }

    }
}
