using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (menuName ="Shop Item/Bomb Upgrade")]
public class BombUpgrade : ShopItem
{
    // Start is called before the first frame update
    public int UpgradeAmount;

   
    public override void Buy()
    {
        base.Buy();
        PlayerStats.bombDamage += UpgradeAmount;
    }
}
