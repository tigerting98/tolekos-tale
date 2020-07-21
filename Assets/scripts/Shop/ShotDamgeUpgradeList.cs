using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotDamgeUpgradeList : ShopItemList
{
    public override ShopItem ChooseItem()
    {
        if (PlayerStats.shotDamageUpgradeLevel < shopitemlist.Count)
        {
            return shopitemlist[PlayerStats.shotDamageUpgradeLevel];
        }
        else
        {
            return shopitemlist[shopitemlist.Count - 1];
        }
    }
}
