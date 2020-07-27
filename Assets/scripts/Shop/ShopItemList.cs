using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//A generic shop item list to store the tree of a shop item
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
