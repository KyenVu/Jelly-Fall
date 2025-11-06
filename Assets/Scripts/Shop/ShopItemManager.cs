using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.Advertisements;

public class ShopItemManager : MonoBehaviour
{
    public Image itemIconImage;

    public TMP_Text itemNameText;
    public TMP_Text coinCostText;
    public TMP_Text moneyCostText;

    public Button coinPurchaseButton;
    public Button moneyPurchaseButton;
    public Button equipButton;

    public GameObject notEnoughCoinPanel;

    public ShopItemData itemData;

    public delegate bool CoinPurchaseHandler(int cost);
    public static event CoinPurchaseHandler OnCoinPurchase;

    private void Start()
    {
        Setup();
    }

    public void Setup()
    {   
        //Take the data from scriptable object
        itemNameText.text = itemData.itemName;
        itemIconImage.sprite = itemData.itemIcon;
        coinCostText.text = "<sprite name=coin> " + itemData.coinCost.ToString();
        moneyCostText.text = itemData.moneyCost.ToString();

        coinPurchaseButton.onClick.AddListener(AttemptCoinPurchase);
        moneyPurchaseButton.onClick.AddListener(MoneyPurchase);
        equipButton.onClick.AddListener(Equip);

        RefreshUI();
    }

    private void RefreshUI()
    {
        bool isPurchased = PlayerPrefs.GetInt("ShopItem_" + itemData.itemName, 0) == 1;
        string equippedItem = PlayerPrefs.GetString("EquippedItem", "");

        if (isPurchased)
        {
            coinPurchaseButton.gameObject.SetActive(false);
            moneyPurchaseButton.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(true);

            if (itemData.itemName == equippedItem)
            {
                equipButton.GetComponentInChildren<TMP_Text>().text = "Equipped";
                equipButton.interactable = false;
            }
            else
            {
                equipButton.GetComponentInChildren<TMP_Text>().text = "Equip";
                equipButton.interactable = true;
            }
        }
        else
        {
            equipButton.gameObject.SetActive(false);
        }
    }

    void AttemptCoinPurchase()
    {
        if (OnCoinPurchase != null)
        {
            bool success = OnCoinPurchase.Invoke(itemData.coinCost);

            if (success)
            {
                PlayerPrefs.SetInt("ShopItem_" + itemData.itemName, 1);
                PlayerPrefs.Save();
                RefreshUI();
            }
            else
            {
                notEnoughCoinPanel.SetActive(true);
                StartCoroutine(PopDownNotEnoughPanel());
            }
        }
    }
    void Equip()
    {
        PlayerPrefs.SetString("EquippedItem", itemData.itemName);
        PlayerPrefs.Save();

        // Notify PlayerCosmetic to update character sprite
        PlayerCosmetic.OnCosmeticChanged?.Invoke();

        // Refresh all shop items
        foreach (ShopItemManager sim in FindObjectsOfType<ShopItemManager>())
        {
            sim.RefreshUI();
        }

        Debug.Log(itemData.itemName + " equipped.");
    }

    IEnumerator PopDownNotEnoughPanel()
    {
        yield return new WaitForSeconds(5);
        notEnoughCoinPanel.SetActive(false);
    }

    void MoneyPurchase()
    {

        AdsManager.instance.OnShowRewardVideo(
            onShowCompleted: () =>
            {
                PlayerPrefs.SetInt("ShopItem_" + itemData.itemName, 1);
                PlayerPrefs.Save();
                RefreshUI();

                Debug.Log("Item unlocked via ad: " + itemData.itemName);
            },
            onShowFailed: () =>
            {
                Debug.Log("Ad was not completed, item not unlocked.");
            });
    }


}
