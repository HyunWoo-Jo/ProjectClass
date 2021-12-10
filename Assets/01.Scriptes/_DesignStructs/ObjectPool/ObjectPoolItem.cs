///�ۼ��� 21.09.26
///�ۼ��� ������

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
