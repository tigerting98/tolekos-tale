using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct Stage3Wave4Subwave 
{
    public float spawnDelay;
    public float startX;
    public float endY;
    public DamageType element;
    public EnemyPack enemyPack;
    public Bullet enemyBullet;
    public EnemyStats enemyStats;
}
public class Stage3Wave4 : EnemyWave
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] EnemyStats stats;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float shotRate = 0.5f;
    [SerializeField] float shotAngle = 20f;
    [SerializeField] int numberOfRings = 3;
    [SerializeField] int bulletsPerRing = 10;
    [SerializeField] float damage = 10f;
    [SerializeField] float stationaryTime = 1f;
    [SerializeField] List<Stage3Wave4Subwave> subwaveList;
    Enemy enemy;

    public override void SpawnWave()
    {
        foreach (Stage3Wave4Subwave subwave in subwaveList) 
        {
            StartCoroutine(SpawnSubwave(subwave.spawnDelay, subwave.startX, subwave.endY, subwave.enemyPack, subwave.element, subwave.enemyBullet, subwave.enemyStats));
        }
    }

    IEnumerator SpawnSubwave(float spawnDelay, float startX, float endY, EnemyPack enemyPack, DamageType element, Bullet enemyBullet, EnemyStats stats) 
    {
        Enemy enemy = enemyPack.GetItem(element);
        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(SpawnEnemy(moveSpeed, new Vector2(startX, 4.2f), new Vector2(startX, endY), enemyPack.GetItem(element), stats, enemyBullet));
        StartCoroutine(SpawnEnemy(moveSpeed, new Vector2(-startX, 4.2f), new Vector2(-startX, endY), enemyPack.GetItem(element), stats, enemyBullet));
    }

    IEnumerator SpawnEnemy(float speed, Vector2 start, Vector2 end, Enemy enemy, EnemyStats stats, Bullet bullet)
    {

        Enemy en = Instantiate(enemy, start, Quaternion.identity);
        en.SetEnemy(stats, false);
        
        float timeToEnd = en.movement.MoveTo(end, speed);
        
        yield return new WaitForSeconds(timeToEnd);

        if (en)
        {
            Patterns.RingOfBullets(bullet, damage, en.transform.position, bulletsPerRing, 360 / bulletsPerRing, bulletSpeed);
        }

        yield return new WaitForSeconds(stationaryTime);

        if(en) 
        {   
            float timeToReturn = en.movement.MoveTo(start, speed);
            yield return new WaitForSeconds(timeToReturn);
        }

        if (en)
        {
            Destroy(en.gameObject);
        }
    }
}
