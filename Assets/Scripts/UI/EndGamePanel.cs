using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGamePanel : MonoBehaviour
{
    public Button replayButton, nextLevelButton, homeButton;
    public TMP_Text text;

    private void OnEnable()
    {
        PlayerDeathState.playerDeath += Lose;
        ArcadeGameManager.doneLevelArcade += Win;
        TimeAttackGameModeManager.doneLevelTimeAttack += Win;
    }

    private void OnDisable()
    {
        PlayerDeathState.playerDeath -= Lose;
        ArcadeGameManager.doneLevelArcade -= Win;
        TimeAttackGameModeManager.doneLevelTimeAttack -= Win;
    }

    void Start()
    {
        homeButton.onClick.AddListener(BackToHomeScene);
        replayButton.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);
        homeButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Lose()
    {
        text.text = "YOU LOSE";
        nextLevelButton.gameObject.SetActive(false);
        replayButton.gameObject.SetActive(true);
        homeButton.gameObject.SetActive(true);

    }

    private void Win()
    {
        text.text = "YOU WIN";
        replayButton.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(true);
        homeButton.gameObject.SetActive(true);
    }

    public void ReplaySMMode()
    {
        SceneManager.LoadScene("SurvivalMode");
    }
    public void ReplayACMode()
    {
        SceneManager.LoadScene("ArcadeMode");
    }
    public void ReplayTAMode()
    {
        SceneManager.LoadScene("TimeAttackMode");
    }

    public void NextLevelArcadeMode()
    {
        string modeKey = "ArcadeMode";
        int currentLevel = PlayerPrefs.GetInt(modeKey + "_SelectedLevel", 1);

        PlayerPrefs.SetInt(modeKey + "_SelectedLevel", currentLevel + 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene("ArcadeMode");
    }

    public void NextLevelTimeAttackMode()
    {
        string modeKey = "TimeAttackMode";
        int currentLevel = PlayerPrefs.GetInt(modeKey + "_SelectedLevel", 1);

        PlayerPrefs.SetInt(modeKey + "_SelectedLevel", currentLevel + 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene("TimeAttackMode");
    }

    private void BackToHomeScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
