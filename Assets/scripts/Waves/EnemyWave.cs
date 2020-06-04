using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    public List<Enemy> enemies = default;
    public List<Bullet> bullets = default;
    public BulletPack bulletPack = default;
    [Header("Audio")]
    public AudioClip bulletSpawnSound = default;
    public float audioVolume;

    public virtual void SpawnWave() { }

    public void DestroyAfter(float sec) {
        Invoke("Destroy", sec);
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }

}
