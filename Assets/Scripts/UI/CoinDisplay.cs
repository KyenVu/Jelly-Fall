using TMPro;
using UnityEngine;

public class CoinDisplay : MonoBehaviour
{
    public TMP_Text coinText;

    private void Start()
    {
        UpdateCoinText();

        CoinManager.OnCoinChanged += UpdateCoinText;
    }

    private void OnDestroy()
    {
        CoinManager.OnCoinChanged -= UpdateCoinText;
    }

    public void UpdateCoinText()
    {
        coinText.text = "<sprite name=coin> " + CoinManager.instance.GetTotalCoins().ToString();

    }
}
