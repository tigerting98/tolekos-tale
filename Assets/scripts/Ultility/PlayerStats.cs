using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static Player player;
    public static float playerMaxHP = 5000;
    public static int playerLevel = 1;
    public static float damage = 100;
    public static int exp = 0;
    public static int expToLevelUp = 100;
    public static Func<int, int> expFormula = level => level * 100 * (1 + level / 5);
    public static Func<int, float> dmgGainFormula = level => 100 * (Mathf.Pow(1.15f, level - 1) - 1);
    public static Func<int, float> hpGainFormula = level => 5000 * (Mathf.Pow(1.15f, level - 1) - 1);
    public static void GainEXP(int gainedEXP) {
       
        exp += gainedEXP;
        while (exp >= expToLevelUp) {
            
            exp -= expToLevelUp;
            LevelUp();
          
        }

    }

    public static void Reset() {
        playerLevel = 1;
        exp = 0;
        expToLevelUp = 100;
        damage = 100;
        playerMaxHP = 5000;
    }
    public static void LevelUp() {
        
        playerLevel++;
        expToLevelUp = expFormula(playerLevel);
        damage += dmgGainFormula(playerLevel);
        playerMaxHP += hpGainFormula(playerLevel);
        player.Level();
    }
    
}
