using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] bool destroyOnImpact = true;
    [SerializeField] bool damageOverTime = false;
    public int GetDamage() {
        return this.damage;
    }

    public bool DestroyOnImpact() {
        return destroyOnImpact;
    }

    public bool DamageOverTime() {
        return damageOverTime;
    }
}
