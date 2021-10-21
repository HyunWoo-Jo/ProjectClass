using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TweenManager
{
    private static List<LTDescr> tweenList = new List<LTDescr>();

    public static void Clear() {
        tweenList.Clear();
    }
    public static void Pause() {
        foreach(var item in tweenList) {
            item.pause();
        }
    }
    public static void Resume() {
        foreach (var item in tweenList) {
            item.resume();
        }
    }
    public static void Add(LTDescr tween) {
        tweenList.Add(tween);
        tween.setOnComplete(() => {
            tweenList.Remove(tween);
        });
    }
}
