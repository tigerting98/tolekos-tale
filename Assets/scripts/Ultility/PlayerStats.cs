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
    public static int gold = 0;
    public static event Action OnGainExp;
    public static event Action OnGainGold;
    public static event Action OnUseBomb;

    public static int bombCount = 3;

    public static void UseBomb() {
        bombCount--;
        OnUseBomb?.Invoke();
    }
    public static void AddGold(int gold) {
        PlayerStats.gold += gold;
        OnGainGold?.Invoke();
    }
    public static void ReduceGold(int gold)
    {
        PlayerStats.gold -= gold;
    }
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
        gold = 0;
        bombCount = 3;
    }
    public static void LevelUp()
    {

        playerLevel++;
        expToLevelUp = expFormula(playerLevel);
        damage += dmgGain;
        playerMaxHP += maxHPGain;
        player.Level();
    }

    public static void SetPlayerInvulnerable()
    {
        if (player)
        {
            player.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    public static void SetPlayerVulnerable()
    {
        if (player)
        {
            player.gameObject.GetComponent<Collider2D>().enabled = true;
        }
    }


}
