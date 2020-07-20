using System;

using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int deathCount = 1;
    public static int baseDmg = 10;
    public static int baseMaxHP = 1000;
    public static Player player;
    public static float playerMaxHP = 1000;
    public static float playerCurrentHP = 1000;
    public static int playerLevel = 1;
    public static float damage = 10;
    public static int exp = 0;
    public static int expToLevelUp = 100;
    public static Func<int, int> expFormula = level => 100 + 25 * (level - 1);
    public static float dmgGain = 1;
    public static float maxHPGain = 100;
    public static float fireFocusDamageRatio = 35f;
    public static float fireUnfocusDamageRatio = 25f;
    public static float earthFocusDaamgeRatio = 8f;
    public static float damageMultiplier = 1f;
    public static int hardenSkinLevel = 0;
    public static int bombLevel = 0;
    public static float bombEffectiveness = 1;
    public static float earthFocusedShotRateRatio = 2f;
    public static float baseShotRate = 0.1f;
    public static int gold = 0;
    public static event Action OnGainExp;
    public static event Action OnGainGold;
    public static event Action OnUpdateBomb;
    public static int maxBomb = 10;
    public static int bombCount = 3;

    public static float bombDamage = 1000;

    public static float bombDamagePerLevel = 100;
    public static void UseBomb() {
        bombCount--;
        OnUpdateBomb?.Invoke();
    }
    public static void GainBomb(int x) {
        bombCount += x;
        OnUpdateBomb?.Invoke();
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
        bombLevel = 0;
        bombEffectiveness = 1;
        hardenSkinLevel = 0;
        damageMultiplier = 1;
        deathCount = 0;
        bombDamage = 1000;
        playerLevel = 1;
        exp = 0;
        expToLevelUp = expFormula(1);
        damage =baseDmg;
        playerMaxHP = baseMaxHP;
        playerCurrentHP = baseMaxHP;
        gold = 0;
        bombCount = 3;
        OnGainExp = null;
        OnGainGold = null;
        OnUpdateBomb = null;
    }
    public static void LevelUp()
    {

        playerLevel++;
        bombDamage += bombDamagePerLevel;
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
