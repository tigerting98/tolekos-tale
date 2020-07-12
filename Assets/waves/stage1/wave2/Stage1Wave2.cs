using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stage1Wave2 : EnemyWave
{
    // Start is called before the first frame update
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float acceleration = 10f;
    [SerializeField] int number = 10;
    [SerializeField] float randomFactor = 0.1f;
    [SerializeField] float stopY = 3f;
    [SerializeField] float stopTime = 0.5f;
    [SerializeField] EnemyStats stats;
    [Header("SpawnRate")]
    [SerializeField] float min = 0.3f;
    [SerializeField] float max = 0.7f;
    [Header("Bullet Behavior")]
    [SerializeField] int bulletCount = 10;
    [SerializeField] float initialSpeed = 10f;
    [SerializeField] float burstRadius = 1f;
    [SerializeField] float finalSpeed = 1f;
    [SerializeField] float dmg = 100;

    public override void SpawnWave() {
        List<float> positions = new List<float>();
        float spacing = 7f / (number - 1);
        for (int i = 0; i < number; i++) {
            positions.Add(-3.5f + i * spacing);
        
        }
        float time = 0;
        for (int i = number - 1; i >= 0; i--) {
            int j = Random.Range(0, i + 1);
            StartCoroutine(SpawnEnemy(time, positions[j]));
            positions.RemoveAt(j);
            time += Random.Range(min, max);
        }
        DestroyAfter(max * number + 10);
        
    }

    IEnumerator SpawnEnemy(float delay, float x)
    {
        yield return new WaitForSeconds(delay);
        Enemy enemy = Instantiate(GameManager.gameData.linemonster.GetItem(DamageType.Pure), new Vector2(x, 4.5f), Quaternion.identity);
        enemy.SetEnemy(stats, false);
        float time1 = enemy.movement.MoveTo(new Vector2(x + Random.Range(-randomFactor, randomFactor), stopY), moveSpeed);
        yield return new WaitForSeconds(time1);
        if (enemy) {
            float angle = Functions.AimAtPlayer(enemy.transform);
            Patterns.ExplodingRingOfBullets(GameManager.gameData.smallRoundBullet.GetItem(Random.Range(0, 3)), dmg
                , enemy.transform.position, bulletCount, angle, initialSpeed, finalSpeed, burstRadius / initialSpeed, bulletSpawnSound) ;
            yield return new WaitForSeconds(stopTime);
            if (enemy) {
                enemy.movement.StartMoving();
                float newX = enemy.transform.position.x > 0 ? 4.5f: -4.5f;

                enemy.movement.AccelerateTowards(acceleration, new Vector2(newX, 4.5f), moveSpeed);
                enemy.movement.destroyBoundary = 4.2f;
            }


        }
        
        
    }

    
}
