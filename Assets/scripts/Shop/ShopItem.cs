using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Shop Item/Shop Item")]
public class ShopItem : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public string description;
    public int cost;

    public virtual bool Buyable() {
        return cost <= PlayerStats.gold;
    
    }

    public virtual void Buy() {
        Debug.Log("bought");
        PlayerStats.gold -= cost;       
    }
}
