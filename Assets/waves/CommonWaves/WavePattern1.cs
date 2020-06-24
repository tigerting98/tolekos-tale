using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct WavePattern1SubWave
{
    public int number;
    public float startTime;
    public bool left;
    public int enemyid;
    public float shootChance;
}
public class WavePattern1 : EnemyWave
{
    // Start is called before the first frame update
    [SerializeField] protected float ymin, ymax;
    [SerializeField] protected float startMoveMax, startMoveMin;
    [SerializeField] protected float spawnRate = 0.1f;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float shotRate;
    [SerializeField] protected int bulletNumber;
    [SerializeField] protected int lines;
    [SerializeField] protected List<WavePattern1SubWave> subwaves;
    [SerializeField] protected EnemyStats stats;
    [SerializeField] protected float dmg = 150;
    protected List<Enemy> enemies = new List<Enemy>();
    protected Bullet bullet;

    public virtual void SetUp() { 
    
    }
    public override void SpawnWave()
    {
        SetUp();
        foreach (WavePattern1SubWave subwave in subwaves)
        {
            Enemy en = enemies[subwave.enemyid];
            StartCoroutine(SpawnEnemy(en, subwave.left, subwave.startTime, subwave.number, subwave.shootChance));
        }

    }

    IEnumerator SpawnEnemy(Enemy en, bool left, float delay, int number, float shootChance)
    {
 
        yield return new WaitForSeconds(delay);
        float r = 4.1f;
        yield return Functions.RepeatActionXTimes(() =>
        {
            Enemy enemy = Instantiate(en, new Vector2(left ? -r : r, Random.Range(ymin, ymax)), Quaternion.identity);
            enemy.SetEnemy(stats, false);
            float startSpeed = Random.Range(startMoveMin, startMoveMax);
            float time = 2 * r / startSpeed;
            float acc = startSpeed / time;
            enemy.movement.SetAcceleration(new Vector2((left ? 1 : -1) * startSpeed, 0), t => new Vector2((left ? 1 : -1) * (t < time ? -acc : acc), 0));
            if (Random.Range(0f, 1f) < shootChance)
            {
                enemy.shooting.StartShootingFor(EnemyPatterns.PulsingBulletsRandomAngle(bullet, dmg, enemy.transform, bulletSpeed, shotRate, lines), time, shotRate * bulletNumber);
            }
        }, spawnRate, number);
    }


}
