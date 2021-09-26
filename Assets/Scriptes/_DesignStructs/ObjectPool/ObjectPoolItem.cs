///작성일 21.09.26
///작성자 조현우

using UnityEngine;

namespace DesignStruct {
    public class ObjectPoolItem : MonoBehaviour {
        public ObjectPool owner;

        public void ReturnObject() {
            owner.ReturnObject(this.gameObject);
        }
    }
}
