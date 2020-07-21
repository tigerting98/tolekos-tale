using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombUpgradeList : ShopItemList
{
    public override ShopItem ChooseItem()
    {
        if (PlayerStats.bombLevel < shopitemlist.Count)
        {
            return shopitemlist[PlayerStats.bombLevel];
        }
        else {
            return shopitemlist[shopitemlist.Count - 1];
        }
    }
}
