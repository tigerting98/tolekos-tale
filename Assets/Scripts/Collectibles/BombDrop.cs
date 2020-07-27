using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//A collectible that provides the player with 1 bomb
public class BombDrop : Collectible
{
    public int bombAmount = 1;
    // Start is called before the first frame update
    protected override void Collect()
    {
        PlayerStats.GainBomb(bombAmount);
    }
}
