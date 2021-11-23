using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignStruct;
using System;

public class GameManager : Singleton<GameManager>
{
    public Gold gold = new Gold();

    protected override void Awake() {
        base.Awake();
        gold.Load();
    }
}
