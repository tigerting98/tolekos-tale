using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Shop Item/Harden Skin")]
//This upgrade improve the damage resistance of a player
public class HardenSkin : ShopItem
{
    // Start is called before the first frame update
    [SerializeField] int hardenSkinLevel = 1;
    [SerializeField] float damageMultiplier = 0.9f;
    public override bool Buyable()
    {
        return base.Buyable() && PlayerStats.hardenSkinLevel == hardenSkinLevel - 1;
    }
    public override void Buy()
    {
        base.Buy();
        PlayerStats.hardenSkinLevel = hardenSkinLevel;
        PlayerStats.damageMultiplier = damageMultiplier;
    }
}
