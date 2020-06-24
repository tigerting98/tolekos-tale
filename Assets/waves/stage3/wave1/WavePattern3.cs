using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

[System.Serializable]
struct WavePattern3SubWave
{
    public float miny, maxy;
    public float timing;
    public int number;
    public bool left;
    [Range(0, 1)] public float shotChance;
}

public class WavePattern3 : EnemyWave
{

    // Start is called before the first frame update
    [Header("subwave")]
    [SerializeField] float movespeed, spawnRate;
    [SerializeField] EnemyStats stats;
    protected Enemy enemy;
    [SerializeField] List<WavePattern3SubWave> subwaves;
    [Header("Bullet Behavior")]
    [SerializeField] float shotRate = 0.2f, dmg = 150, shotSpeed = 3, delay = 0.5f;
    protected Bullet bulletnormal;
    [Header("Explode Behavior")]
    [SerializeField] float dmgexplode = 150, fastspeed = 5, slowspeed = 1, radius = 1;
    [SerializeField] int numberOfExplode = 5;
    protected Bullet bulletexplode;

    public virtual void SetUp() { 
        
    }
    public override void SpawnWave()
    {
        SetUp();
        foreach (WavePattern3SubWave subwave in subwaves)
        {
            StartCoroutine(SpawnSubWave(subwave.miny, subwave.maxy, subwave.timing, subwave.number, subwave.left, subwave.shotChance));
        }
    }

    IEnumerator SpawnSubWave(float miny, float maxy, float timing, int number, bool left, float shotChance)
    {
        yield return new WaitForSeconds(timing);
        StartCoroutine(Functions.RepeatCustomActionXTimes(i =>
        {
            float y = Random.Range(miny, maxy);
            SpawnEnemy(y, left, shotChance);
        }, spawnRate, number));

    }

    Enemy SpawnEnemy(float y, bool left, float shotChance)
    {
        Enemy en = Instantiate(enemy, new Vector2(left ? -4.1f : 4.1f, y), Quaternion.identity);
        en.SetEnemy(stats, false);
        en.deathEffects.OnDeath += () => Patterns.ExplodingRingOfBullets(bulletexplode, dmgexplode, en.transform.position, numberOfExplode, 0, fastspeed, slowspeed, radius / fastspeed);
        en.movement.SetSpeed(movespeed, left ? 0 : 180);
        en.movement.destroyBoundary = 4.5f;
        if (Random.Range(0f, 1f) < shotChance)
        {
            en.shooting.StartShootingAfter(EnemyPatterns.ShootAtPlayer(bulletnormal, dmg, en.transform, shotSpeed, shotRate), delay);
        }
        return en;
    }


}