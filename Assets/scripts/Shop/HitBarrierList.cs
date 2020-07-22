using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBarrierList : ShopItemList
{
    public override ShopItem ChooseItem()
    {
        if (PlayerStats.hitBarrierLevel < shopitemlist.Count)
        {
            return shopitemlist[PlayerStats.hitBarrierLevel];

        }
        else
        {
            return shopitemlist[shopitemlist.Count - 1];
        }
    }
}
