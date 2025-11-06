using System.Collections;
using UnityEngine;
using TMPro; // Make sure TMP is imported

public class PreGameState : MonoBehaviour
{
    public static PreGameState instance {  get; private set; }
    public GameObject preGamePanel;
    public TMP_Text countdownText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        preGamePanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game

        // Start countdown coroutine using unscaled time
        StartCoroutine(CountdownBeforeStart());
    }

    IEnumerator CountdownBeforeStart()
    {
        int countdown = 3;
        while (countdown > 0)
        {
            countdownText.text = countdown.ToString();
            yield return new WaitForSecondsRealtime(1f); // Use unscaled time
            countdown--;
        }

        countdownText.text = "Go!";
        yield return new WaitForSecondsRealtime(1f);

        FindObjectOfType<ScoreManager>()?.StartGame(); 

        preGamePanel.SetActive(false);
        Time.timeScale = 1f;

    }
}
