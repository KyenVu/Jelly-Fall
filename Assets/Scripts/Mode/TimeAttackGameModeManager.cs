using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeAttackGameModeManager : MonoBehaviour
{
    public float baseStartTime = 30f; 

    public TMP_Text timerText;

    public int baseRewardCoin = 10;

    private float currentTime;
    private bool isPaused = false;
    private bool isGameOver = false;

    private int rewardCoin;


    public delegate void DoneLevelTimeAttack();
    public static event DoneLevelTimeAttack doneLevelTimeAttack;

    private void OnEnable()
    {
        PlayerHurtState.playerHurt += OnPlayerHurt;
    }

    private void OnDisable()
    {
        PlayerHurtState.playerHurt -= OnPlayerHurt;
    }

    private void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("TimeAttackMode_SelectedLevel", 1);
        ApplyDifficulty(currentLevel);
        UpdateTimerUI();
    }

    private void Update()
    {
        if (isGameOver || isPaused) return;

        currentTime -= Time.deltaTime;
        UpdateTimerUI();

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            isGameOver = true;
            WinLevel();
        }
    }

    private void ApplyDifficulty(int level)
    {
        currentTime = baseStartTime + (level - 1) * 5f;
        rewardCoin = baseRewardCoin + (level * 5);
    }

    private void UpdateTimerUI()
    {
        timerText.text = Mathf.CeilToInt(currentTime).ToString() + "s";
    }

    private void WinLevel()
    {
        doneLevelTimeAttack?.Invoke();
        Debug.Log("Player wins the time attack!");

        string modeKey = "TimeAttackMode";
        int currentLevel = PlayerPrefs.GetInt(modeKey + "_SelectedLevel", 1);
        int unlockedLevel = PlayerPrefs.GetInt(modeKey + "_UnlockedLevel", 1);

        if (currentLevel >= unlockedLevel)
        {
            PlayerPrefs.SetInt(modeKey + "_UnlockedLevel", currentLevel + 1);
            PlayerPrefs.Save();
        }

        CoinManager.instance.AddCoin(rewardCoin);
    }

    private void OnPlayerHurt()
    {
        StartCoroutine(AddTimeWithPause(5f, 1f)); 
    }

    private IEnumerator AddTimeWithPause(float timeToAdd, float pauseDuration)
    {
        isPaused = true;
        currentTime += timeToAdd;
        UpdateTimerUI();
        yield return new WaitForSeconds(pauseDuration);
        isPaused = false;
    }
}
