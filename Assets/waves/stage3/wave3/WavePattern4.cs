using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct WavePattern4Subwave 
{
    public float spawnDelay;
    public int numberOfEnemies;
    public float startY;
    public float midY;
    public float endY;
    public int lines;
    
}
public class WavePattern4 : EnemyWave
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float delay;
    [SerializeField] int number = 5;
    [SerializeField] float spawnRate = 0.5f;
    [SerializeField] EnemyStats stats;
    [SerializeField] float shootingDelay = 0.3f;
    [SerializeField] float bulletSpeed = 3f;
    [SerializeField] float shotAngle = 20f;
    [SerializeField] int bulletsPerLine = 10;
    [SerializeField] float bulletDamage = 10f;
    [SerializeField] List<WavePattern4Subwave> subwaveList;
    protected Enemy enemy;
    protected Bullet bullet;

    public virtual void SetUp()
    {

    }
    public override void SpawnWave()
    {
        SetUp();
        foreach (WavePattern4Subwave subwave in subwaveList) 
        {
            StartCoroutine(SpawnSubwave(subwave.spawnDelay, subwave.numberOfEnemies, subwave.startY, subwave.midY, subwave.endY, subwave.lines));
        }
    }

    IEnumerator SpawnSubwave(float spawnDelay, int numberOfEnemies, float startY, float midY, float endY, int lines) 
    {
        Enemy enemy = this.enemy;
        yield return new WaitForSeconds(spawnDelay);
        Functions.StartMultipleCustomCoroutines(this, i => SpawnEnemy(moveSpeed, 0, new Vector2(-4.1f, startY), new Vector2(0f, midY), new Vector2(-4.1f, endY), lines, enemy), numberOfEnemies, spawnRate);
        Functions.StartMultipleCustomCoroutines(this, i => SpawnEnemy(moveSpeed, 0, new Vector2(4.1f, startY), new Vector2(0f, midY), new Vector2(4.1f, endY), lines, enemy), numberOfEnemies, spawnRate);
    }

    IEnumerator SpawnEnemy(float speed, float firingDelay, Vector2 start, Vector2 mid, Vector2 end, int lines, Enemy enemy)
    {

        Enemy en = Instantiate(enemy, start, Quaternion.identity);
        en.SetEnemy(stats, false);
        
        float timeToMid = en.movement.MoveTo(mid, speed);

        yield return new WaitForSeconds(shootingDelay);

        if(en) {
            float bulletSpeed = this.bulletSpeed;
            float angle = Functions.AimAtPlayer(en.transform);
            for (int i = 0; i < bulletsPerLine; i++) {
                Patterns.ShootMultipleStraightBullet(GameManager.gameData.leafBullet1, bulletDamage, en.transform.position, bulletSpeed, angle, shotAngle, lines,GameManager.gameData.longarrowSFX);
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
