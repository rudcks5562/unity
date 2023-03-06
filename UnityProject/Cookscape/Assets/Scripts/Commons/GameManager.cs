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

    void Start()
    {
        gaugeSlider = GameObject.Find("Gauge_Info").GetComponent<Slider>();
    }

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
        gaugeSlider.gameObject.SetActive(true);
    }

    public void HideGaugeInfo()
    {
        gaugeSlider.gameObject.SetActive(false);
    }
}
