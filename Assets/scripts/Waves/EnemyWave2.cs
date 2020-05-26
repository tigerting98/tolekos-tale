using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyWave2 : EnemyWave
{
    // Start is called before the first frame update
    [SerializeField] Vector2 startPosition1 = default;
    [SerializeField] Vector2 stopPosition1 = default;
    [SerializeField] Vector2 endPosition1 = default;
    [SerializeField] Vector2 startPosition2 = default;
    [SerializeField] Vector2 stopPosition2 = default;
    [SerializeField] Vector2 endPosition2 = default;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float moveOutSpeed = 1f;
    [SerializeField] float shotRate = 1f;
    [SerializeField] int number = 3;
    [SerializeField] float spawnRate = 0.5f;
    [SerializeField] float bulletSpeed = 2f;
    [SerializeField] int lines = 20;
    [SerializeField] AudioClip bulletSpawnSound;
    [SerializeField] float volume = 0.2f;


    public override void SpawnWave() {
   
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < number; i++)
        {
            float time1 = SpawnOneEnemy(enemies[0], startPosition1, stopPosition1, endPosition1);
            yield return new WaitForSeconds(time1);

            SpawnOneEnemy(enemies[0], startPosition2, stopPosition2, endPosition2);

            yield return new WaitForSeconds(spawnRate - time1);
        }
        Destroy(gameObject, 20f);
    }

    float SpawnOneEnemy(Enemy enemy, Vector2 start, Vector2 stop, Vector2 end) {
        Enemy enemy1 = Instantiate(enemy, start, Quaternion.identity);
        float time1 = enemy1.movement.MoveTo(stop, moveSpeed);
        enemy1.shooting.StartShootingAfter(EnemyPatterns.PulsingBulletsRandom(bulletPack.bullets, enemy1.transform, bulletSpeed, shotRate, lines), time1);
        StartCoroutine(MoveAwayAfter(enemy1, end, moveOutSpeed, spawnRate));
        enemy1.shooting.PlayAudio(bulletSpawnSound, shotRate, volume, time1);
        return time1;
    }

    IEnumerator MoveAwayAfter(Enemy enemy, Vector2 end, float speed, float time) {
        yield return new WaitForSeconds(time);
        if (enemy)
        {
            float sec = enemy.movement.MoveTo(end, speed);
            yield return new WaitForSeconds(sec);
        }
        if (enemy) {
            Destroy(enemy.gameObject);
        }

        
    }
}
