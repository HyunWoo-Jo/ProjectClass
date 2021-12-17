using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignStruct;
using System;

public class GameManager : Singleton<GameManager>
{
    public Gold gold = new Gold();
    [HideInInspector] public int maxClearStage;

    protected override void Awake() {
        base.Awake();
        gold.Load();
        LoadClearStage();
    }

    private void LoadClearStage()
    {
        if (PlayerPrefs.HasKey("maxClearStage"))
            maxClearStage = PlayerPrefs.GetInt("maxClearStage");
        else
            maxClearStage = 0;
    }

    public void SaveClearStage(int reachStage)
    {
        if (reachStage > maxClearStage)
        {
            maxClearStage = reachStage;
            PlayerPrefs.SetInt("maxClearStage", maxClearStage);
        }
    }

}
