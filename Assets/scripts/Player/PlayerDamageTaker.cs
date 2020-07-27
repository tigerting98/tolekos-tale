using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Overrides the damage taker to remove bullet on a radius under collision and play sound effects on hit
public class PlayerDamageTaker : DamageTaker
{
    [SerializeField] SFX hitSFX;
    [SerializeField] SFX stayHitSFX;
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        DamageDealer dmg = collision.GetComponent<DamageDealer>();
        if (dmg && !dmg.DamageOverTime())
        { AudioManager.current.PlaySFX(hitSFX);
            GameManager.DestroyNonDPSEnemyBulletsInRadius(PlayerStats.hitBarrierRadius);
        }
    }
    public override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        DamageDealer dmg = collision.GetComponent<DamageDealer>();
        if (dmg && dmg.DamageOverTime())
        {
            AudioManager.current.PlaySFX(stayHitSFX);
        }
    }
    
}
