using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSk : MonoBehaviour
{
    public GameObject darkPanel;

    public void PlaySkill(MonsterInfo info)
    {
        switch (info)
        {
            case MonsterInfo.Dark: StartCoroutine("Dark"); break;
            default: break;
        }
    }

    private IEnumerator Dark()
    {
        if (darkPanel) darkPanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        if (darkPanel) darkPanel.SetActive(false);
    }
}
