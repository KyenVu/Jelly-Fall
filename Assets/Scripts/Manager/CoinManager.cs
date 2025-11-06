using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance { get; private set; }
    public static event System.Action OnCoinChanged;


    private int totalCoins;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
    }

    private void OnEnable()
    {
        Coin.collectCoin += AddCoin;
        ShopItemManager.OnCoinPurchase += HandleCoinPurchase;
    }

    private void OnDisable()
    {
        Coin.collectCoin -= AddCoin;
        ShopItemManager.OnCoinPurchase -= HandleCoinPurchase;
    }
    public int GetTotalCoins()
    {
        return totalCoins;
    }

    public void AddCoin(int amount)
    {
        totalCoins += amount;
        SaveCoins();
        OnCoinChanged?.Invoke();
    }
    public void Add100Coin()
    {
        totalCoins += 100;
    }
    public void MinusCoin(int amount)
    {
        totalCoins -= amount;
        if (totalCoins < 0) totalCoins = 0;
        SaveCoins();
        OnCoinChanged?.Invoke();
    }


    private void SaveCoins()
    {
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save();
    }

    private bool HandleCoinPurchase(int cost)
    {
        if (totalCoins >= cost)
        {
            MinusCoin(cost);
            return true;
        }
        else
        {
            return false;
        }
    }
}
