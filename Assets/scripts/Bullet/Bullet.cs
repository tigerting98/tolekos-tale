
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Movement movement;
    public BulletOrientation orientation;
    public DamageDealer damageDealer;
    public GameObject explosion;

    public void DestroyBullet() {
        Destroy(gameObject);
    }

    public Bullet SetDamage(float dmg) { 
        if(damageDealer)
        {
            damageDealer.damage = dmg;

        }return this;
    }



}
