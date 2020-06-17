using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] ParticleSystem spawnParticles = default;
    [SerializeField] float size = 0.7f;
    [SerializeField] bool spawnAnimation = true;
    // Start is called before the first frame update
    private void Awake()
    {

        GameManager.enemyBullets.Add(gameObject.GetInstanceID(), gameObject);
        if (spawnAnimation && spawnParticles) {
            ParticleSystem particle = Instantiate(spawnParticles, transform.position, Quaternion.identity);
            SetParticle(particle);
            Destroy(particle.gameObject, 0.4f);
        }
    }


    void SetParticle(ParticleSystem system) {
        system.startSize = size;
    }

    private void OnDestroy()
    {

        GameManager.enemyBullets.Remove(gameObject.GetInstanceID());
    }

}
