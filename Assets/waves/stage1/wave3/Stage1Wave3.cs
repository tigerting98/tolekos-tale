using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.XR.WSA;

public class Stage1Wave3 : EnemyWave
{
    // Start is called before the first frame update
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float bottomY = 0f;
    [SerializeField] int number = 10;
    [Header("SpawnRate")]
    [SerializeField] float spawnRate = 0.2f;
    [SerializeField] float subWaveDelay = 1f;
    [SerializeField] float finalDelay = 3f;
    [Header("Bullet Behavior")]
    [SerializeField] int bulletCount = 10;
    [SerializeField] float initialSpeed = 10f;
    [SerializeField] float finalSpeed = 1f;
    [SerializeField] float shotRateMin = 1f;
    [SerializeField] float shotRateMax = 2f;
    [SerializeField] float minTime = 0.1f;
    [SerializeField] float maxTime = 1f;
    [SerializeField] float chanceofShooting = 0.3f;


    public override void SpawnWave() {
        StartCoroutine(TheWave());


    }

    IEnumerator TheWave() {
        SpawnSubWave(true, false);
        yield return new WaitForSeconds(spawnRate * number + subWaveDelay);
        SpawnSubWave(false, false);
        yield return new WaitForSeconds(spawnRate * number + subWaveDelay);
        SpawnSubWave(true, true);
        yield return new WaitForSeconds(spawnRate * number + subWaveDelay);
        SpawnSubWave(false, true);
        yield return new WaitForSeconds(spawnRate * number + finalDelay);
        SpawnSubWave(true, false);
        SpawnSubWave(false, false);
        yield return new WaitForSeconds(spawnRate / 2);
        SpawnSubWave(true, true);
        SpawnSubWave(false, true);


    }

     void SpawnSubWave(bool left, bool color) {
        float time = 0;
        Vector2 start = left ? new Vector2(-4.2f, 4.2f) : new Vector2(4.2f, 4.2f);
        Vector2 end = left ? new Vector2(4.2f, bottomY) : new Vector2(-4.2f, bottomY);
        for (int i = 0; i < number; i++)
        {
            int j = color ? i % 3 + 1 : 0;
            StartCoroutine(SpawnEnemy(enemies[j], bulletPack.GetBullet(j), time, start, end));
            time += spawnRate;
        }

    }

    IEnumerator SpawnEnemy(Enemy enemy, Bullet bullet, float delay, Vector2 start, Vector2 end)
    {
        yield return new WaitForSeconds(delay);
        Enemy newEnemy = Instantiate(enemy, start, Quaternion.identity);
        float time = newEnemy.movement.MoveTo(end, moveSpeed);
        float shotRate = Random.Range(shotRateMin, shotRateMax);
        if (chanceofShooting > Random.Range(0f, 1f))
        {
            newEnemy.shooting.StartShootingAfter
              (EnemyPatterns.ExplodingLineAtPlayer(bullet, newEnemy, initialSpeed, finalSpeed, bulletCount, minTime, maxTime, shotRate), shotRate / 2);
            newEnemy.enemyAudio.PlayAudio(bulletSpawnSound, shotRate, shotRate / 2);
        }
        newEnemy.DestroyAfter(time);

        
        
        
    }

    
}
