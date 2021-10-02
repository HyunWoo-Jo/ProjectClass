///�ۼ��� 21.09.26
///�ۼ��� ������

using UnityEngine;

namespace DesignStruct {
    public class ObjectPoolItem : MonoBehaviour {
        public ObjectPool owner;

        public void Init(ObjectPool owner) {
            this.owner = owner;
        }

        public void ReturnObject() {
            owner.ReturnObject(this.gameObject);
        }
    }
}