using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop/Shop Item")]
public class ShopItemData : ScriptableObject
{
    public string itemName;         
    public Sprite itemIcon;         
    public int coinCost;         
    public int moneyCost;        

   
}
