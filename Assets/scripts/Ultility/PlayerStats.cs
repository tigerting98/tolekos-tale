using System;
using System.Runtime.CompilerServices;
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
    public static float fireFocusDamageRatio = 25f;
    public static float fireUnfocusDamageRatio = 15f;
    public static float earthFocusDaamgeRatio = 10f;
    
    public static float earthFocusedShotRateRatio = 2f;
    public static float baseShotRate = 0.1f;

    public static event Action OnGainExp;

    public static void GainEXP(int gainedEXP)
    {

        exp += gainedEXP;
        while (exp >= expToLevelUp)
        {

            exp -= expToLevelUp;
            LevelUp();

        }
        OnGainExp?.Invoke();

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
