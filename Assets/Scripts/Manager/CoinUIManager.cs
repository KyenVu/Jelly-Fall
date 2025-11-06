using UnityEngine;
using TMPro;

public class CoinUIManager : MonoBehaviour
{
    public static CoinUIManager instance { get; private set; }

    public TMP_Text coinText;
    private int coinCollected;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
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
        coinText.text = ("0");
    }
    private void CoinCollected(int amount)
    {
        coinCollected += amount;
        UpdateCoinText(coinCollected);
    }

    public void UpdateCoinText(int coins)
    {
        if (coinText != null)
        {
            coinText.text = coins.ToString();
        }
    }

    public int GetCoinCollected()
    {
        return coinCollected;
    }
}
