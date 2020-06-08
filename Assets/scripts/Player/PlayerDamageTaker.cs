using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageTaker : DamageTaker
{
    [SerializeField] SFX hitSFX;
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        hitSFX.PlayClip();
    }
}
