using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ScoreUIManager : MonoBehaviour
{
    
    public TMP_Text scoreText;
    public TMP_Text finalScoreText;
    public TMP_Text highScoreText;
    public TMP_Text coinCollectedText;  

    private void OnEnable()
    {
        PostGameState.playerWin += EndScreenEnable;
        PostGameState.playerLose += EndScreenEnable;
        ScoreManager.OnScoreChanged += UpdateScoreText;
    }

    private void OnDisable()
    {
        PostGameState.playerWin -= EndScreenEnable;
        PostGameState.playerLose -= EndScreenEnable;
        ScoreManager.OnScoreChanged -= UpdateScoreText;
    }

    private void Start()
    {
        scoreText.text = "00000";
    }
    // Update score and text format
    private void UpdateScoreText(float newScore)
    {
        scoreText.text = newScore.ToString("00000");
    }

    // End Screen Text
    private void EndScreenEnable()
    {
        finalScoreText.text = scoreText.text;

        int coinCollected = CoinUIManager.instance?.GetCoinCollected() ?? 0;
        coinCollectedText.text = coinCollected.ToString();


        float highScore = PlayerPrefs.GetFloat("HighScore", 0f);
        highScoreText.text = highScore.ToString();
        SaveToLeaderboard(highScore);
    }
    private void SaveToLeaderboard(float finalScore)
    {
        List<float> scores = new List<float>();

        // Load current scores
        for (int i = 0; i < 5; i++)
        {
            scores.Add(PlayerPrefs.GetFloat("HighScore" + i, 0f));
        }

        // Add new score
        scores.Add(finalScore);

        // Sort descending and keep top 5
        scores.Sort((a, b) => b.CompareTo(a));
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetFloat("HighScore" + i, scores[i]);
        }

        PlayerPrefs.Save();
    }


}
