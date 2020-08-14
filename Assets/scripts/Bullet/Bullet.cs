
using UnityEngine;
//This class stores all the references to the bullet and the behavior of the bullet
public class Bullet : MonoBehaviour, IPooledObject
{

    public Movement movement;
    public bool destroyOnDeath = true;
    public BulletOrientation orientation;
    public DamageDealer damageDealer;
    public GameObject explosion;
    public string bulletPoolID;
    [SerializeField] ParticleSystem spawnParticles = default;
    [SerializeField] ParticleSystem hitParticles = default;
    [SerializeField] float size = 0.7f;
    [SerializeField] bool spawnAnimation = true;
    // Upon deactivation, return it to the bullet pool
    public void Deactivate()
    {
        if (orientation) {
            orientation.enabled = true;
        }
        if (movement)
        {
            movement.enabled = true;
        }
        GameManager.bulletpools.DeactivateBullet(this);
    }

    public Bullet SetDamage(float dmg)
    {
        if (damageDealer)
        {
            damageDealer.damage = dmg;

        }
        return this;
    }
    //Create a hit effect
    public void SpawnHitParticles() {
        if (hitParticles) {
            ParticleSystem particle = Instantiate(hitParticles, transform.position, Quaternion.identity);
            Destroy(particle.gameObject, 0.4f);
        }
    }
    //Create a spawning particle effect upon instantiation
    public void OnSpawn()
    {
        if (spawnAnimation && spawnParticles)
        {
            ParticleSystem particle = Instantiate(spawnParticles, transform.position, Quaternion.identity);
            SetParticle(particle);
            Destroy(particle.gameObject, 0.4f);
        }
      
        
    }
    void SetParticle(ParticleSystem system)
    {
        system.startSize = size;
    }
}
