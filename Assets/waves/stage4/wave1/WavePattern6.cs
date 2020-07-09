using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct WavePattern6Subwave
{
    public float spawnDelay;
    public float highY;
    public float lowY;
    public int numberOfEnemies;

}
public class WavePattern6 : EnemyWave
{
/* 
Creates a swarm of small enemies with randomized positions that constantly shoot bullets aimed at the player, resulting in a constant stream of bullets.
The number of enemies spawned and the region in which they can spawn can be configured.
*/
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] EnemyStats stats;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float shotRate = 0.5f;
    [SerializeField] float bulletDamage = 10f;
    [SerializeField] float stationaryTime = 1f;
    [SerializeField] List<WavePattern6Subwave> subwaveList;
    [SerializeField] float spawnRate;
    protected Enemy enemy;
    protected Bullet bullet;

    public virtual void SetUp()
    {

    }

    public override void SpawnWave()
    {
        SetUp();
        foreach (WavePattern6Subwave subwave in subwaveList) 
        {
            StartCoroutine(SpawnSubwave(subwave.spawnDelay, subwave.lowY, subwave.highY, subwave.numberOfEnemies));
        }
    }

    IEnumerator SpawnSubwave(float spawnDelay, float lowY, float highY, int numberOfEnemies) 
    {
        yield return new WaitForSeconds(spawnDelay);
        this.StartCoroutine(Functions.RepeatCustomActionXTimes(i => this.StartCoroutine(SpawnEnemy(moveSpeed, highY, lowY)), spawnRate, numberOfEnemies));
    }

    IEnumerator SpawnEnemy(float speed, float upperYBound, float lowerYBound)
    {
        float yPos = UnityEngine.Random.Range(lowerYBound, upperYBound);
        float xPos = UnityEngine.Random.Range(-3.7f, 3.7f);
        float leftRightChance = Random.Range(0f, 1f);
        float initialX = leftRightChance >= 0.50f ? 4.2f : -4.2f;
        Enemy enemy = Instantiate(this.enemy, new Vector2(initialX, yPos), Quaternion.identity);
        enemy.SetEnemy(stats, false);
        float timeToPoint = enemy.movement.MoveTo(new Vector2(xPos, yPos), moveSpeed);
        yield return new WaitForSeconds(timeToPoint);
        if (enemy)
        {
        enemy.shooting.StartShootingFor(EnemyPatterns.ShootAtPlayer(bullet, bulletDamage, enemy.transform, bulletSpeed, shotRate,null), 0.5f, stationaryTime + 0.5f);
        }
        yield return new WaitForSeconds(stationaryTime + 1f);
        if (enemy)
        {
            float finalX = initialX < 0f ? 4.2f : -4.2f;
            float timeToEnd = enemy.movement.MoveTo(new Vector2(finalX, yPos), moveSpeed);
            Destroy(enemy.gameObject, timeToEnd);
        }

    }
}
