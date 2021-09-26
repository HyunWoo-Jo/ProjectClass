///작성일 21.09.26
///작성자 조현우

using System.Collections.Generic;
using UnityEngine;

namespace DesignStruct {
    public class ObjectPool : MonoBehaviour {
        public GameObject poolObject;

        private List<GameObject> poolObjectList;
        private bool isAutoInstance;
        private int instanceCount;


        private void Start() {
            Init();
        }
        private void Init() {
            poolObjectList = new List<GameObject>();
        }

        private void AddObject(int count) {
            for(int i = 0; i < count; i++) {
                GameObject obj = Instantiate(poolObject);
                obj.transform.SetParent(this.transform);

                poolObjectList.Add(obj);
                obj.SetActive(false);
            }
        }

        public GameObject TakeObject() {
            if (poolObjectList.Count <= 0) {
                AddObject(1);
            }
            GameObject obj = poolObjectList[0];
            poolObjectList.RemoveAt(0);

            return obj;
        }
        public T TakeObject<T>() {
            if (poolObjectList.Count <= 0) {
                AddObject(1);
            }
            GameObject obj = poolObjectList[0];
            poolObjectList.RemoveAt(0);
            return obj.GetComponent<T>();
        }
         
        public void ReturnObject(GameObject poolObject) {
            poolObjectList.Add(poolObject);
            poolObject.transform.SetParent(this.transform);
            poolObject.SetActive(false);
        }

    }
}
