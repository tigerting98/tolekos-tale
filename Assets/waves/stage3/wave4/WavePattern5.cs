using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct WavePattern5Subwave
{
    public float spawnDelay;
    public float startX;
    public float endY;
    public bool summonBoss;

}
public class WavePattern5 : EnemyWave
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] EnemyStats stats;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float shotRate = 0.5f;
    [SerializeField] float shotAngle = 20f;
    [SerializeField] int numberOfRings = 3;
    [SerializeField] int bulletsPerRing = 10;
    [SerializeField] float bulletDamage = 10f;
    [SerializeField] float stationaryTime = 1f;
    [SerializeField] List<WavePattern5Subwave> subwaveList;
    protected Enemy enemy;
    protected Bullet bullet;

    public virtual void SetUp()
    {

    }

    public override void SpawnWave()
    {
        SetUp();
        foreach (WavePattern5Subwave subwave in subwaveList) 
        {
            StartCoroutine(SpawnSubwave(subwave.spawnDelay, subwave.startX, subwave.endY, subwave.summonBoss));
        }
    }

    IEnumerator SpawnSubwave(float spawnDelay, float startX, float endY, bool summonBoss) 
    {
        Enemy enemy = this.enemy;
        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(SpawnEnemy(moveSpeed, new Vector2(startX, 4.2f), new Vector2(startX, endY)));
        StartCoroutine(SpawnEnemy(moveSpeed, new Vector2(-startX, 4.2f), new Vector2(-startX, endY)));
        if (summonBoss)
        {
            yield return new WaitForSeconds(10f);
            GameManager.DestoryAllEnemyBullets();
            GameManager.DestroyAllNonBossEnemy(false);
            GameManager.SummonEndBoss();
        }
    }

    IEnumerator SpawnEnemy(float speed, Vector2 start, Vector2 end)
    {

        Enemy en = Instantiate(enemy, start, Quaternion.identity);
        en.SetEnemy(stats, false);
        
        float timeToEnd = en.movement.MoveTo(end, speed);
        
        yield return new WaitForSeconds(timeToEnd);

        if (en)
        {
            en.shooting.StartCoroutine(Functions.RepeatCustomActionXTimes(i => Patterns.RingOfBullets(bullet, bulletDamage, en.transform.position, bulletsPerRing, 0, bulletSpeed), shotRate, numberOfRings));
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
