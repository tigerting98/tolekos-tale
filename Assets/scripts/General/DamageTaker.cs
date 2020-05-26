
using UnityEngine;

public class DamageTaker : MonoBehaviour
{
    [SerializeField] Health health;

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {

        DamageDealer dmg = collision.GetComponent<DamageDealer>();
        if (dmg != null && !dmg.DamageOverTime())
        {
            health.TakeDamage(dmg.GetDamage());
            if (dmg.DestroyOnImpact())
            { Destroy(collision.gameObject); }
        }
    }

    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        DamageDealer dmg = collision.GetComponent<DamageDealer>();
        if (dmg != null && dmg.DamageOverTime())
        {
            health.TakeDamage(dmg.GetDamage() * Time.deltaTime);
        }
    }
}
