using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (menuName ="Shop Item/Bomb Upgrade")]
public class BombUpgrade : ShopItem
{
    // Start is called before the first frame update
    public float multiplier = 1.25f;
    public int level = 1;

    public override bool Buyable()
    {
        return base.Buyable() && PlayerStats.bombLevel +1  == level;
    }
    public override void Buy()
    {
        base.Buy();
        PlayerStats.bombLevel = level;
        PlayerStats.bombEffectiveness = multiplier;
    }
}
