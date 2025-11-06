using System.Collections;
using TMPro;
using UnityEngine;

public class ArcadeGameManager : MonoBehaviour
{
    public int baseCoinsToCollect = 5;
    public int baseRewardCoin = 10;

    public TMP_Text coinProgressText;

    private int coinToCollect;
    private int rewardedCoin;
    private int coinCollected;
    private bool isWin;

    public delegate void DoneLevelArcade();
    public static event DoneLevelArcade doneLevelArcade;

    private void OnEnable()
    {
        Coin.collectCoin += CoinCollected;
    }

    private void OnDisable()
    {
        Coin.collectCoin -= CoinCollected;
    }

    void Start()
    {
        isWin = false;
        coinCollected = 0;

        int currentLevel = PlayerPrefs.GetInt("ArcadeMode_SelectedLevel", 1);
        ApplyDifficulty(currentLevel);
        UpdateCoinProgressText();
    }

    void Update()
    {
        if (coinCollected >= coinToCollect && !isWin)
        {
            isWin = true;
            RewardPlayer();
        }
    }

    private void ApplyDifficulty(int level)
    {
        coinToCollect = baseCoinsToCollect + (level-1) * 2;
        rewardedCoin = baseRewardCoin + level * 5;
    }

    private void CoinCollected(int amount)
    {
        coinCollected += amount;
        UpdateCoinProgressText();
    }

    private void RewardPlayer()
    {
        doneLevelArcade?.Invoke();
        CoinManager.instance.AddCoin(rewardedCoin);

        string modeKey = "ArcadeMode";
        int currentLevel = PlayerPrefs.GetInt(modeKey + "_SelectedLevel", 1);
        int unlockedLevel = PlayerPrefs.GetInt(modeKey + "_UnlockedLevel", 1);

        if (currentLevel >= unlockedLevel)
        {
            PlayerPrefs.SetInt(modeKey + "_UnlockedLevel", currentLevel + 1);
            PlayerPrefs.Save();
        }
    }
    private void UpdateCoinProgressText()
    {
        if (coinProgressText != null)
        {
            coinProgressText.text = $"{coinCollected}/{coinToCollect}";
        }
    }

}
