using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemList : MonoBehaviour
{
    public List<ShopItem> shopitemlist;

    public virtual ShopItem ChooseItem() {
        if (shopitemlist!=null)
        {
            return shopitemlist[0];
        }
        else {
            return null;
        }
    }
}
