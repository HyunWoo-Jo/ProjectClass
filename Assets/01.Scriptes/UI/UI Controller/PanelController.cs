///작성일 21.10,21
///작성자 조현우
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Controller;
using UnityEngine.UI;
public class PanelController : MonoBehaviour
{
    public GameObject fadePanel;
    public GameObject midPanel;
    public GameObject frontPanel;
    public GameObject pausePanel;

    public void FadeIn(GameObject panel,float time, float alhpa) {
        panel.SetActive(true);
        var tween = LeanTween.alphaCanvas(panel.GetComponent<CanvasGroup>(), alhpa, time);
    }

    public void FadeOut(GameObject panel, float time, float alhpa) {
        var tween = LeanTween.alphaCanvas(panel.GetComponent<CanvasGroup>(), alhpa, time);
        tween.setOnComplete(() => { panel.SetActive(false); });
    }


}
