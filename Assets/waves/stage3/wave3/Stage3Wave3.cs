using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct Stage3Wave3Subwave 
{
    public float spawnDelay;
    public int numberOfEnemies;
    public float startY;
    public float midY;
    public float endY;
    public int lines;
    public DamageType element;
    public EnemyPack enemyPack;
    public Bullet enemyBullet;
    public EnemyStats enemyStats;
}
public class Stage3Wave3 : EnemyWave
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float delay;
    [SerializeField] int number = 5;
    [SerializeField] float spawnRate = 0.5f;
    [SerializeField] EnemyStats stats;
    [SerializeField] float shootingDelay = 0.3f;
    [SerializeField] float bulletSpeed = 0.5f;
    [SerializeField] float shotRate = 0.2f;
    [SerializeField] float shotAngle = 20f;
    [SerializeField] int lines = 1;
    [SerializeField] int bulletsPerLine = 10;
    [SerializeField] float damage = 10f;
    [SerializeField] List<Stage3Wave3Subwave> subwaveList;
    Enemy enemy;

    public override void SpawnWave()
    {
        foreach (Stage3Wave3Subwave subwave in subwaveList) 
        {
            StartCoroutine(SpawnSubwave(subwave.spawnDelay, subwave.numberOfEnemies, subwave.startY, subwave.midY, subwave.endY, subwave.lines, subwave.enemyPack, subwave.element, subwave.enemyBullet, subwave.enemyStats));
        }
    }

    IEnumerator SpawnSubwave(float spawnDelay, int numberOfEnemies, float startY, float midY, float endY, int lines, EnemyPack enemyPack, DamageType element, Bullet enemyBullet, EnemyStats stats) 
    {
        Enemy enemy = enemyPack.GetItem(element);
        yield return new WaitForSeconds(spawnDelay);
        Functions.StartMultipleCustomCoroutines(this, i => SpawnEnemy(moveSpeed, 0, new Vector2(-4.1f, startY), new Vector2(0f, midY), new Vector2(-4.1f, endY), lines, enemy, stats), numberOfEnemies, spawnRate);
        Functions.StartMultipleCustomCoroutines(this, i => SpawnEnemy(moveSpeed, 0, new Vector2(4.1f, startY), new Vector2(0f, midY), new Vector2(4.1f, endY), lines, enemy, stats), numberOfEnemies, spawnRate);
    }

    IEnumerator SpawnEnemy(float speed, float firingDelay, Vector2 start, Vector2 mid, Vector2 end, int lines, Enemy enemy, EnemyStats stats)
    {

        Enemy en = Instantiate(enemy, start, Quaternion.identity);
        en.SetEnemy(stats, false);
        
        float timeToMid = en.movement.MoveTo(mid, speed);

        yield return new WaitForSeconds(shootingDelay);

        if(en) {
            float bulletSpeed = this.bulletSpeed;
            float angle = Patterns.AimAt(en.transform.position, GameManager.playerPosition);
            for (int i = 0; i < bulletsPerLine; i++) {
                Patterns.ShootMultipleStraightBullet(GameManager.gameData.leafBullet1, damage, en.transform.position, bulletSpeed, angle, shotAngle, lines);
                bulletSpeed += 0.5f; 
            }
        } 

        yield return new WaitForSeconds(timeToMid - shootingDelay);

        if (en) 
        {
            float timeToEnd = en.movement.MoveTo(end, speed);
    
            yield return new WaitForSeconds(timeToEnd);

            if(en)
            {
            Destroy(en.gameObject);
            }
        }
    }
}
