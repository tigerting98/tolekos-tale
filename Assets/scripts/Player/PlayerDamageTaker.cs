using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
