using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this holds the details of the damage values of the attacker
public class DamageDealer : MonoBehaviour
{
    public float damage;
    [SerializeField] bool destroyOnImpact = true;
    [SerializeField] bool damageOverTime = false;
    public DamageType damageType = DamageType.Pure;

    public bool DestroyOnImpact() {
        return destroyOnImpact;
    }

    public bool DamageOverTime() {
        return damageOverTime;
    }
}

public enum DamageType { Water, Earth, Fire, Pure}
