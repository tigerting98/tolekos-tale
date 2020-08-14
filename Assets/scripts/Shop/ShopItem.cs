using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Shop Item/Shop Item")]
//A base class for shop items that can be purchased
public class ShopItem : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    [TextArea(3,5)]public string description;
    public int cost;

    public virtual bool Buyable() {
        return cost <= PlayerStats.gold;
    
    }

    public virtual void Buy() {
        Debug.Log("bought");
        PlayerStats.gold -= cost;       
    }
}
