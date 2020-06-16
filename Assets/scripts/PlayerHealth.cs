using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    // Start is called before the first frame update
    public override void Start()
    {
        maxHP = PlayerStats.playerMaxHP;
        currentHP = PlayerStats.playerCurrentHP;
    }

    

}
