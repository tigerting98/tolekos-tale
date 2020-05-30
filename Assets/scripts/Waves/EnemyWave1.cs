using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave1 : EnemyWave
{
    // Start is called before the first frame update
    [SerializeField] Vector2 startPosition = default;
    [SerializeField] Vector2 endPosition = default;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float shotRate = 0.5f;
    [SerializeField] int number = 5;
    [SerializeField] float spawnRate = 0.5f;
    [SerializeField] float bulletSpeed = 5f;


    public override void SpawnWave() {

        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        int j = 0;
        for (int i = 0; i < number; i++)
        {   
            Enemy enemy = Instantiate(enemies[j], startPosition, Quaternion.identity);
            float time = enemy.movement.MoveTo(endPosition, moveSpeed);
            enemy.DestroyAfter(time);
            enemy.shooting.StartShooting(EnemyPatterns.ShootAtPlayer(bulletPack.bullets[j], enemy.transform, bulletSpeed, shotRate));
            j = (j + 1) % 3;
            yield return new WaitForSeconds(spawnRate);
        }
        Destroy(gameObject);
    }
}
