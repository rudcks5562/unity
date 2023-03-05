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

    public void setGuideText(string msg)
    {
        guideText.text = msg;
    }

    public void showGuideText()
    {
        guideText.gameObject.SetActive(true);
    }

    public void HideGuideText()
    {
        guideText.gameObject.SetActive(false);
    }
}
