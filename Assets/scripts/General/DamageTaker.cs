
using System;
using UnityEngine;

//this class is responsible for taking damage upon collision
public class DamageTaker : MonoBehaviour
{
    [SerializeField] Health health = default;
    public Action<DamageDealer> OnDamageTaken;
    public bool vulnerable = true;
    public float FireMultiplier = 1;
    public float WaterMultiplier = 1;
    public float EarthMultiplier = 1;
    public float PureMultiplier = 1;
    private void Awake()
    {
        health = health ? health : GetComponent<Health>();
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (vulnerable)
        {
            DamageDealer dmg = collision.GetComponent<DamageDealer>();
            if (dmg != null && !dmg.DamageOverTime())
            {
                OnDamageTaken?.Invoke(dmg);
                health.TakeDamage(GetDamage(dmg));
                if (dmg.DestroyOnImpact())
                {
                    if (dmg.gameObject.tag == "Player Bullet") {
                        dmg.gameObject.GetComponent<Bullet>().SpawnHitParticles();
                    }
                    IPooledObject obj = dmg.gameObject.GetComponent<IPooledObject>();
                    if (obj != null)
                    {
                        obj.Deactivate();
            
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                    }
                }
            }
        }
    }

    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (vulnerable)
        {
            DamageDealer dmg = collision.GetComponent<DamageDealer>();
            if (dmg != null && dmg.DamageOverTime())
            {
                OnDamageTaken?.Invoke(dmg);
                health.TakeDamage(GetDamage(dmg) * Time.deltaTime);
            }
        }
    }

    public float GetDamage(DamageDealer dmg) {
        float baseDamage = dmg.damage;
        if (dmg.damageType == DamageType.Water) {
            return baseDamage * WaterMultiplier;
        }
        else if (dmg.damageType == DamageType.Earth)
        {
            return baseDamage * EarthMultiplier;
        }
        else if (dmg.damageType == DamageType.Fire)
        {
            return baseDamage * FireMultiplier;
        }
         else
        {
            return baseDamage * PureMultiplier;
        }
    }
}
