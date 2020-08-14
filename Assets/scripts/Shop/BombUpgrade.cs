using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (menuName ="Shop Item/Bomb Upgrade")]
//This provides shop items to upgrarde their bobm damage
public class BombUpgrade : ShopItem
{
    // Start is called before the first frame update
    public float multiplier = 1.25f;
    public int level = 1;
    //Check if the player fulfills teh prerequisite
    public override bool Buyable()
    {
        return base.Buyable() && PlayerStats.bombLevel +1  == level;
    }

    //Increase the player's bommb damage and level
    public override void Buy()
    {
        base.Buy();
        PlayerStats.bombLevel = level;
        PlayerStats.bombEffectiveness = multiplier;
    }
}
