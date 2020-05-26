using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWave : EnemyWave
{
    // Start is called before the first frame update
    [SerializeField] Vector2 startPosition = default;
    [SerializeField] Vector2 endPosition = default;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float shotRate = 0.05f;
  
    [SerializeField] float bulletSpeed = 4f;
    [SerializeField] float shotRate2 = 0.5f;

    [SerializeField] float bulletSpeed2 = 3f;
    [SerializeField] int shotLines = 20;
    [SerializeField] float angularVel = 1f;
    [SerializeField] int circleLines=20;
    [SerializeField] float delay = 0.1f;
    [SerializeField] int number = 3;
    [SerializeField] AudioClip sound;
    [SerializeField] float volume = 0.05f;
    public override void SpawnWave() {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        Enemy enemy = Instantiate(enemies[0], startPosition, Quaternion.identity);
        float time = enemy.movement.MoveTo(endPosition, moveSpeed);
        enemy.shooting.StartShootingAfter(EnemyPatterns.BorderOfWaveAndParticle(bullets[0], enemy.transform, bulletSpeed, shotRate, shotLines, angularVel), 
                time);
        for (int i = 0; i < number; i++)
        { enemy.shooting.StartShootingAfter(EnemyPatterns.PulsingBulletsRandom(bulletPack.bullets, enemy.transform, bulletSpeed2, shotRate2, circleLines)
            , time + delay*i );
            enemy.shooting.PlayAudio(sound, shotRate2, volume, delay * i);
        }
        yield return new WaitForSeconds(1);
        
        Destroy(gameObject);
    }

    
}
