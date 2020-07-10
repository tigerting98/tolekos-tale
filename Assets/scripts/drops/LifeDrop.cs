using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDrop : Collectible
{
    public bool percentageBased =  false;
    public float percentageAmount = 50;
    public float fixedAmount = 1000;

    protected override void Collect()
    {
        float gainAmount;
        if (percentageBased)
        {
            gainAmount = PlayerStats.playerMaxHP * percentageAmount;
        }
        else {
            gainAmount = fixedAmount;
        }
        PlayerStats.player.health.Heal(gainAmount);
    }
}
