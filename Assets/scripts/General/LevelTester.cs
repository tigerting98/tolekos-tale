using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTester : Level
{
    [SerializeField] int playerLevel = 1;
    bool uped = false;
    public override void Update()
    {
        if (!uped)
        {
            for (int i = 1; i < playerLevel; i++)
            {

                PlayerStats.LevelUp();
                Debug.Log(PlayerStats.playerCurrentHP);
            }
            PlayerStats.GainEXP(1);
        }
        uped = true;
    }

}
