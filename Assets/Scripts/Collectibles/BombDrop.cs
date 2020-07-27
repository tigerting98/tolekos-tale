using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDrop : Collectible
{
    public int bombAmount = 1;
    // Start is called before the first frame update
    protected override void Collect()
    {
        PlayerStats.GainBomb(bombAmount);
    }
}
