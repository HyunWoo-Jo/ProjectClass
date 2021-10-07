using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignStruct;

public class GameManager : Singleton<GameManager>
{
    public Gold gold = new Gold();

    protected override void Awake() {
        base.Awake();
        gold.AddGold(1000); // �ӽ�
    }
}