using System.Collections;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private float score = 0;
    private float timer = 0;
    private bool canGainPoints = true;
    private bool isGameOver = false;
    private bool gameStarted = false;


    public delegate void ScoreChanged(float newScore);
    public static event ScoreChanged OnScoreChanged;

    private void OnEnable()
    {
        PostGameState.playerWin += EndGame;
        PostGameState.playerLose += EndGame;
        PlayerHurtState.playerHurt += Hurt;
    }

    private void OnDisable()
    {
        PostGameState.playerWin -= EndGame;
        PostGameState.playerLose -= EndGame;
        PlayerHurtState.playerHurt -= Hurt;
    }

    private void Update()
    {
        if (!gameStarted || isGameOver || !canGainPoints) return;

        score += 1; 
        OnScoreChanged?.Invoke(score);
    }

    public void StartGame()
    {
        gameStarted = true;
    }
    private void Hurt()
    {
        StartCoroutine(PausePointsForSeconds(1.2f));
    }

    private IEnumerator PausePointsForSeconds(float duration)
    {
        canGainPoints = false;
        yield return new WaitForSeconds(duration);
        canGainPoints = true;
    }

    private void EndGame()
    {
        isGameOver = true;

        float highScore = PlayerPrefs.GetFloat("HighScore", 0f);
        if (score > highScore)
        {
            PlayerPrefs.SetFloat("HighScore", score);
            PlayerPrefs.Save();
            Debug.Log("New High Score: " + score);
        }

        Debug.Log("Game Over! Final Score: " + score);
    }

}
