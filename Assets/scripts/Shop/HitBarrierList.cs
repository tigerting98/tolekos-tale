using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This encompass the hit barrier upgrade tree
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
