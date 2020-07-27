using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (menuName = "Shop Item/Bomb")]
public class BombItem : ShopItem
{
    [SerializeField] int numberOfBombs;
    public override bool Buyable()
    {
        return base.Buyable() && PlayerStats.bombCount < PlayerStats.maxBomb;
    }

    public override void Buy()
    {
        base.Buy();
        PlayerStats.bombCount += numberOfBombs;
    }
}
