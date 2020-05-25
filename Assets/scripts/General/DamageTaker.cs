
using UnityEngine;

public class DamageTaker : MonoBehaviour
{
    [SerializeField] Health health;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        DamageDealer dmg = collision.GetComponent<DamageDealer>();
        if (dmg != null && !dmg.DamageOverTime())
        {
            health.TakeDamage(dmg.GetDamage());
            if (dmg.DestroyOnImpact())
            { Destroy(collision.gameObject); }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        DamageDealer dmg = collision.GetComponent<DamageDealer>();
        if (dmg != null && dmg.DamageOverTime())
        {
            health.TakeDamage((int)Mathf.Ceil(dmg.GetDamage() * Time.deltaTime));
        }
    }
}
