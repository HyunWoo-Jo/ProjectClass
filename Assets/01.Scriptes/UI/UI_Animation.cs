//작성자 조현우
//21.11.27
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace GameUI {
    public enum UI_Animation_Type {
        ScaleUp,
        ScaleDown,
        ScrollUpFromUnder,
        ScrollUpFromCenter,
        ScrollDownFromCenter,
        ScrollDownFromUp,
        ScrollLeftFromRight,
        ScrollLeftFromCenter,
        ScrollRightFromLeft,
        ScrollRightFromCenter,
        RotateY,
        RotateZero
    }
    public class UI_Animation : MonoBehaviour {

        public Vector3 originPosition; 

        private void Awake() {
            this.originPosition = transform.position;
        }

        public void Play(UI_Animation_Type type, float time) {
            this.Play(type, time, null);
        }
        public void Play(UI_Animation_Type type, float time , Action action) {
            LTDescr tween = null;
            Vector3 target;
            switch(type) {
                case UI_Animation_Type.ScaleUp:
                    this.gameObject.transform.localScale = Vector3.zero;
                    tween = LeanTween.scale(this.gameObject, Vector3.one, time).setEase(LeanTweenType.easeInOutBack);
                    break;
                case UI_Animation_Type.ScaleDown:
                    tween = LeanTween.scale(this.gameObject, Vector3.zero, time).setEase(LeanTweenType.easeInOutBack);
                    break;

                case UI_Animation_Type.ScrollUpFromUnder:
                    this.transform.position = originPosition - new Vector3(0, Camera.main.orthographicSize * 2, 0);
                    tween = LeanTween.move(this.gameObject, originPosition, time).setEase(LeanTweenType.easeInOutBack);
                    break;
                case UI_Animation_Type.ScrollUpFromCenter:
                    target = originPosition + new Vector3(0, Camera.main.orthographicSize * 2, 0);
                    tween = LeanTween.move(this.gameObject, target, time).setEase(LeanTweenType.easeInOutBack);
                    break;

                case UI_Animation_Type.ScrollDownFromCenter:
                    target = originPosition - new Vector3(0, Camera.main.orthographicSize * 2, 0);
                    tween = LeanTween.move(this.gameObject, target, time).setEase(LeanTweenType.easeInOutBack);
                    break;
                case UI_Animation_Type.ScrollDownFromUp:
                    this.transform.position = originPosition + new Vector3(0, Camera.main.orthographicSize * 2, 0);
                    tween = LeanTween.move(this.gameObject, originPosition, time).setEase(LeanTweenType.easeInOutBack);
                    break;

                case UI_Animation_Type.ScrollLeftFromRight:
                    this.transform.position = originPosition + new Vector3(Camera.main.orthographicSize * 2 * Camera.main.aspect, 0, 0);
                    tween = LeanTween.move(this.gameObject, originPosition, time).setEase(LeanTweenType.easeInOutBack);
                    break;
                case UI_Animation_Type.ScrollLeftFromCenter:
                    target = originPosition - new Vector3(Camera.main.orthographicSize * 2 * Camera.main.aspect, 0, 0);
                    tween = LeanTween.move(this.gameObject, target, time).setEase(LeanTweenType.easeInOutBack);
                    break;

                case UI_Animation_Type.ScrollRightFromLeft:
                    this.transform.position = originPosition - new Vector3(Camera.main.orthographicSize * 2 * Camera.main.aspect, 0, 0);
                    tween = LeanTween.move(this.gameObject, originPosition, time).setEase(LeanTweenType.easeInOutBack);
                    break;
                case UI_Animation_Type.ScrollRightFromCenter:
                    target = originPosition + new Vector3(Camera.main.orthographicSize * 2 * Camera.main.aspect, 0, 0);
                    tween = LeanTween.move(this.gameObject, target, time).setEase(LeanTweenType.easeInOutBack);
                    break;

                case UI_Animation_Type.RotateY:
                    tween = LeanTween.rotateY(this.gameObject, 90, time);
                    break;
                case UI_Animation_Type.RotateZero:
                    tween = LeanTween.rotate(this.gameObject, Vector3.zero, time);
                    break;
            }
            if(tween == null) return;
            if(action != null) {
                tween.setOnComplete(action);
            }
        }

        public void Click() {
            LeanTween.scale(this.gameObject, new Vector3(0.7f, 0.7f, 0.7f), 0.05f).
                setOnComplete(() => {
                    LeanTween.scale(this.gameObject, Vector3.one, 0.1f).
                    setEase(LeanTweenType.easeOutBack);
                });
        }
    }

    public static class Anim {
        public static UI_Animation Add_UI_Animation(this GameObject obj) {
            UI_Animation anim = obj.GetComponent<UI_Animation>();
            if(anim != null) return anim;
            else {
                anim = obj.AddComponent<UI_Animation>();
                anim.originPosition = obj.transform.position;
                return anim;
            }
            
        }
    }
}
