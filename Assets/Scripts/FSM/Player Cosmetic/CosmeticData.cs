using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CosmeticData", menuName = "Shop/Cosmetic Data")]
public class CosmeticData : ScriptableObject
{
    public List<CosmeticItem> cosmeticList;
}

[System.Serializable]
public class CosmeticItem
{
    public string itemName;
    public Sprite characterSprite;
}