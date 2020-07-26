using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Enemy Stats")]
//Holds the information of the enemy
public class EnemyStats : ScriptableObject
{ 
    public float hp = 100, dmg = 1000, goldchance = 0.3f;
    public int maxGold = 20, minGold = 10, exp= 10;

    public bool customResist = false;

    public DamageType type = DamageType.Pure;

    public float waterMultiplier, earthMultiplier, fireMultiplier, pureMultiplier;

    public float GetWaterMultiplier() {
        return customResist ? waterMultiplier : type == DamageType.Fire ? GameManager.StrongMultiplier : type == DamageType.Earth ? GameManager.WeakMultiplier : 1;
    }
    public float GetEarthMultiplier()
    {
        return customResist ? earthMultiplier : type == DamageType.Water ? GameManager.StrongMultiplier : type == DamageType.Fire ? GameManager.WeakMultiplier : 1;
    }
    public float GetFireMultiplier()
    {
        return customResist ? fireMultiplier : type == DamageType.Earth ? GameManager.StrongMultiplier : type == DamageType.Water ? GameManager.WeakMultiplier : 1;
    }

    public float GetPureMultiplier() {
        return customResist ? pureMultiplier : 1;
    }
}
