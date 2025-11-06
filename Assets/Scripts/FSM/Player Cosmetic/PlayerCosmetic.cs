using UnityEngine;
using System;

public class PlayerCosmetic : MonoBehaviour
{
    public SpriteRenderer characterRenderer;
    public CosmeticData cosmeticDatabase;

    public static Action OnCosmeticChanged;

    private void Start()
    {
        ApplyEquippedCosmetic();
        OnCosmeticChanged += ApplyEquippedCosmetic;
    }

    private void OnDestroy()
    {
        OnCosmeticChanged -= ApplyEquippedCosmetic;
    }

    public void ApplyEquippedCosmetic()
    {
        string equippedItem = PlayerPrefs.GetString("EquippedItem", "");

        foreach (var cosmetic in cosmeticDatabase.cosmeticList)
        {
            if (cosmetic.itemName == equippedItem)
            {
                characterRenderer.sprite = cosmetic.characterSprite;
                break;
            }
        }
    }
}
