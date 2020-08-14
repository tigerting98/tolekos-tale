using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (menuName = "Shop Item/Bomb")]
//Creates a bomb purchasable in the shop
public class BombItem : ShopItem
{
    [SerializeField] int numberOfBombs;
    //Makes sure that the player do not have max bomb before buying a bomb
    public override bool Buyable()
    {
        return base.Buyable() && PlayerStats.bombCount < PlayerStats.maxBomb;
    }
    //Increases bomb Count
    public override void Buy()
    {
        base.Buy();
        PlayerStats.bombCount += numberOfBombs;
    }
}
