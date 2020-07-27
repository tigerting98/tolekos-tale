using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Shop Item/Shot Upgrade")]

public class ShotDamageUpgrade : ShopItem
{
    public float multiplier = 1.25f;
    public int level = 1;

    public override bool Buyable()
    {
        return base.Buyable() && PlayerStats.shotDamageUpgradeLevel + 1 == level;
    }
    public override void Buy()
    {
        base.Buy();
        PlayerStats.shotDamageUpgradeLevel = level;
        PlayerStats.shotDamageMultiplier = multiplier;
    }
}
