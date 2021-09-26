///작성일 21.09.25
///작성자 조현우
using UnityEngine;
using System;

public class ButtonController : MonoBehaviour
{
    private Action leftHandller;
    private Action rightHandller;

    public void OnLeft() {
        leftHandller.Invoke();
    }
    public void OnRight() {
        rightHandller.Invoke();
    }

    public void AddLeftAction(Action action) {
        leftHandller += action;
    }
    
    public void AddRightAction(Action action) {
        rightHandller += action;
    }

    public void ResetAction() {
        leftHandller = null;
        rightHandller = null;
    }
}
