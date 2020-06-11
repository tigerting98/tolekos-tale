using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Shop Item/HealthPack")]
public class HealthPack : ShopItem
{
    public int HealthGain;

    public override bool Buyable()
    {
        return base.Buyable() && ((int)PlayerStats.playerMaxHP > (int)PlayerStats.playerCurrentHP);

    }

    public override void Buy()
    {
        base.Buy();
        PlayerStats.playerCurrentHP = Mathf.Clamp(PlayerStats.playerCurrentHP + HealthGain, 0, PlayerStats.playerMaxHP);
    }
}
