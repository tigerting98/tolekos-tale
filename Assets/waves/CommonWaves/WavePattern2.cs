using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WavePattern2SubWave
{
    public float y;
    public float timing;
    public int number;
    public bool left;
}

public class WavePattern2 : EnemyWave
{

    // Start is called before the first frame update
    [Header("subwave")]
    [SerializeField] protected float movespeed, spawnRate;
    [SerializeField] protected EnemyStats stats;
    [SerializeField] protected List<WavePattern2SubWave> subwaves;
    [Header("Bullet Behavior")]
    [SerializeField] protected float shotRate = 0.2f, dmg = 150, shotSpeed = 3, delay = 0.5f;
    protected Enemy enemy;
    protected Bullet bullet;

    public virtual void SetUp()
    {

    }
    public override void SpawnWave()
    {
        SetUp();
        foreach (WavePattern2SubWave subwave in subwaves)
        {
            StartCoroutine(SpawnSubWave(subwave.y, subwave.timing, subwave.number, subwave.left));
        }
    }

    IEnumerator SpawnSubWave(float y, float timing, int number, bool left)
    {

        yield return new WaitForSeconds(timing);
        StartCoroutine(Functions.RepeatActionXTimes(() =>
        {
            Enemy en = Instantiate(enemy, new Vector2(left ? -4.1f : 4.1f, y), Quaternion.identity);
            en.SetEnemy(stats, false);
            en.movement.SetSpeed(new Vector2(left ? movespeed : -movespeed, 0));
            en.shooting.StartShootingAfter(EnemyPatterns.ShootAtPlayer(bullet, dmg
                , en.transform, shotSpeed, shotRate), delay);

        }
        , spawnRate, number));

    }
}
