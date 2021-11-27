using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUI {
    public class Popup<T> : MonoBehaviour {
        public bool isAutoKill = false;
        public float autoKillTime = 5f;
        public static T InstancePopup(GameObject mainCanvas) {
            string[] classNames = typeof(T).ToString().Split('.');
            GameObject newPopup = Resources.Load(classNames[classNames.Length - 1]) as GameObject;

            GameObject obj = Instantiate(newPopup);
            obj.transform.SetParent(mainCanvas.transform);
            obj.transform.localScale = Vector3.one;
            obj.transform.position = Vector3.zero;
            return obj.GetComponent<T>();

        }

        public T AutoKill(float time) {
            T popup = this.GetComponent<T>();
            isAutoKill = true;
            autoKillTime = time;
            StartCoroutine(AutoKill());
            return popup;
        }

        private IEnumerator AutoKill() {
            yield return new WaitForSeconds(autoKillTime);
            Destroy(this.gameObject);
        }
    }

}