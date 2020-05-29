using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int baseDmg = 10;
    public static int baseMaxHP = 1000;
    public static Player player;
    public static float playerMaxHP = 1000;
    public static int playerLevel = 1;
    public static float damage = 10;
    public static int exp = 0;
    public static int expToLevelUp = 100;
    public static Func<int, int> expFormula = level => 100 + 25 * (level - 1);
    public static float dmgGain = 1;
    public static float maxHPGain = 100;
    public static float laserDamageRatio = 15f;
    public static void GainEXP(int gainedEXP)
    {

        exp += gainedEXP;
        while (exp >= expToLevelUp)
        {

            exp -= expToLevelUp;
            LevelUp();

        }

    }

    public static void Reset()
    {
        playerLevel = 1;
        exp = 0;
        expToLevelUp = expFormula(1);
        damage =baseDmg;
        playerMaxHP = baseMaxHP;
    }
    public static void LevelUp()
    {

        playerLevel++;
        expToLevelUp = expFormula(playerLevel);
        damage += dmgGain;
        playerMaxHP += maxHPGain;
        player.Level();
    }

}
