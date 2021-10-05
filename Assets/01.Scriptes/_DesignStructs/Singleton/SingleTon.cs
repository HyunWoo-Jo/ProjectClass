///�ۼ��� 21.10.05
///�ۼ��� ������
using UnityEngine;

namespace DesignStruct {
    public class Singleton<T> : MonoBehaviour {
        public static T instance;
        protected virtual void Awake() {
            if(instance != null) {
                Destroy(this.gameObject);
            } else {
                DontDestroyOnLoad(this.gameObject);
                instance = this.GetComponent<T>();
            }
        }
    }
}
