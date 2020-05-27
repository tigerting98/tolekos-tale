using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float damage;
    [SerializeField] bool destroyOnImpact = true;
    [SerializeField] bool damageOverTime = false;
  

    public bool DestroyOnImpact() {
        return destroyOnImpact;
    }

    public bool DamageOverTime() {
        return damageOverTime;
    }
}
