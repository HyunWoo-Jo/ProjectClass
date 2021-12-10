///작성일 21.09.26
///작성자 조현우

using UnityEngine;
using System;
using System.Collections;

namespace DesignStruct {
    public class ObjectPoolItem : MonoBehaviour {
        public ObjectPool owner;

        public void Init(ObjectPool owner) {
            this.owner = owner;
        }

        public void ReturnObject() {
            owner.ReturnObject(this.gameObject);
        }

        public void DelayReturn(float time) {
            StartCoroutine(Delay(time));
        }

        private IEnumerator Delay(float time) {
            yield return new WaitForSeconds(time);
            owner.ReturnObject(this.gameObject);
        }
    }
}
