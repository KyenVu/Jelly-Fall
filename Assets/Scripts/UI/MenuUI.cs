using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [Header("Buttons")]
    public Button StartButton;
    public Button HighScoreButton;
    public Button ShopButton;

    [Header("Panels")]
    public GameObject StartPanel;
    public GameObject HighScorePanel;
    public GameObject ShopPanel;

    void Start()
    {
        StartButton.onClick.AddListener(OpenStartPanel);
        HighScoreButton.onClick.AddListener(OpenHighScorePanel);
        ShopButton.onClick.AddListener(OpenShopPanel);
    }

    void OpenStartPanel()
    {
        CloseAllPanels();
        StartPanel.SetActive(true);
    }

    void OpenHighScorePanel()
    {
        CloseAllPanels();
        HighScorePanel.SetActive(true);
    }

    void OpenShopPanel()
    {
        CloseAllPanels();
        ShopPanel.SetActive(true);
    }

    void CloseAllPanels()
    {
        StartPanel.SetActive(false);
        HighScorePanel.SetActive(false);
        ShopPanel.SetActive(false);
    }
}
