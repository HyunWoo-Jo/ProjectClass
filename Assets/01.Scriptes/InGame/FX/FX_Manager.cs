using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class FX_Manager : MonoBehaviour
{
    [SerializeField]
    private LightningBoltScript[] lightnings;
    private int currentLightingNumber = 0;

    //장용진 : 2021.12.10 추가
    [SerializeField]
    private GameObject coinfxPrefab;
    private List<ParticleSystem> coinList = new List<ParticleSystem>();

    private void Awake() {
        lightnings = GetComponentsInChildren<LightningBoltScript>();
        for(int i =0;i< lightnings.Length; i++) {
            lightnings[i].gameObject.SetActive(false);
        }

        //2021.12.10 추가
        if (coinfxPrefab != null)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject prefab = Instantiate(coinfxPrefab);
                prefab.transform.SetParent(this.transform);
                coinList.Add(prefab.GetComponent<ParticleSystem>());
            }
        }
    }

    public void Lightning(Transform start ,Transform end) {
        SoundManager.Play_EFF("L_S");
        lightnings[currentLightingNumber].StartObject.transform.position = start.position;
        StartCoroutine(LightningAction(end.position));
    }
    private IEnumerator LightningAction(Vector3 pos) {
        var lightning = lightnings[currentLightingNumber];
        lightning.EndObject.transform.position = pos;
        lightning.gameObject.SetActive(true);
        LightningCount();

        var lightning2 = lightnings[currentLightingNumber];
        lightning2.StartObject.transform.position = pos;
        lightning2.EndObject.transform.position = pos + new Vector3(0, -1f, 0);
        lightning.gameObject.SetActive(true);
        lightning2.gameObject.SetActive(true);
        LightningCount();
        yield return new WaitForSeconds(0.15f);
        lightning.gameObject.SetActive(false);
        lightning2.gameObject.SetActive(false);
    }

    private void LightningCount() {
        if(lightnings.Length - 1 > currentLightingNumber) {
            currentLightingNumber++;
        } else {
            currentLightingNumber = 0;
        }
    }

    //장용진 : 2021.12.10 추가
    public void CoinFx(Transform trans)
    {
        for (int i = 0; i < coinList.Count; i++)
        {
            if (coinList[i].isPlaying == false)
            {
                Vector3 pos = trans.position;
                pos.z = 0f;
                coinList[i].gameObject.transform.position = pos;
                coinList[i].Play();
                break;
            }
        }
    }

}
