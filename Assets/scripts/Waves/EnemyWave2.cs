using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave2 : EnemyWave
{
    // Start is called before the first frame update
    [SerializeField] Vector2 startPosition = default;
    [SerializeField] Vector2 endPosition = default;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float shotRate1 = 1f;
    [SerializeField] float shotRate2 = 0.5f;
    [SerializeField] int number = 5;
    [SerializeField] float spawnRate = 0.5f;
    [SerializeField] float bulletSpeed1 = 2f;
    [SerializeField] float bulletSpeed2 = 5f;
    [SerializeField] int lines = 30;


    public override void SpawnWave() {
   
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < number; i++)
        {
            Enemy enemy = Instantiate(enemies[0], startPosition, Quaternion.identity);
            float time = enemy.movement.MoveTo(endPosition, moveSpeed);
            enemy.DestroyAfter(time);
            enemy.shooting.StartShooting(EnemyPatterns.PulsingBullets(bullets[0], enemy.transform, bulletSpeed1, shotRate1, lines));
            enemy.shooting.StartShooting(EnemyPatterns.ShootAtPlayer(bullets[1], enemy.transform, bulletSpeed2, shotRate2));
            yield return new WaitForSeconds(spawnRate);
        }
        Destroy(gameObject);
    }
}
