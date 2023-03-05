using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject player;

    public GameObject menuPanel;
    public GameObject gamePanel;

    public TextMeshProUGUI guideText;
    public Slider gaugeSlider;

    public void SetGuideText(string msg)
    {
        guideText.text = msg;
    }

    public void SetGauge(float val)
    {
        gaugeSlider.value = val;
    }

    public void ShowGuideText()
    {
        guideText.gameObject.SetActive(true);
    }

    public void HideGuideText()
    {
        guideText.gameObject.SetActive(false);
    }

    public void ShowGaugeInfo()
    {
        if (gaugeSlider == null) return;
        gaugeSlider.gameObject.SetActive(true);
    }

    public void HideGaugeInfo()
    {
        if (gaugeSlider == null) return;
        gaugeSlider.gameObject.SetActive(false);
    }
}
