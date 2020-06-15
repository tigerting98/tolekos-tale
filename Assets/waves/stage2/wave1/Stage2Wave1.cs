using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
struct SubWave {
    public int number;
    public float startTime;
    public bool left;
    public int enemyid;
    public float shootChance;
}
public class Stage2Wave1 : EnemyWave
{
    // Start is called before the first frame update
    [SerializeField] float ymin, ymax;
    [SerializeField] float startMoveMax, startMoveMin;
    [SerializeField] float spawnRate = 0.1f;
    [SerializeField] float bulletSpeed;
    [SerializeField] float shotRate;
    [SerializeField] int bulletNumber;
    [SerializeField] int lines;
    [SerializeField] List<SubWave> subwaves;


    public override void SpawnWave() {
        foreach (SubWave subwave in subwaves) {
            StartCoroutine(SpawnEnemy(enemies[subwave.enemyid], subwave.left, subwave.startTime, subwave.number, subwave.shootChance));
        }

    }

    IEnumerator SpawnEnemy(Enemy en, bool left, float delay, int number, float shootChance)
    {
        yield return new WaitForSeconds(delay);
        float r= 4.1f;
        yield return Functions.RepeatActionXTimes(() =>
        {
            Enemy enemy = Instantiate(en, new Vector2(left ? -r : r, Random.Range(ymin, ymax)), Quaternion.identity);
            float startSpeed = Random.Range(startMoveMin, startMoveMax);
            float time = 2 * r / startSpeed;
            float acc = startSpeed / time;
            enemy.movement.SetAcceleration(new Vector2((left ? 1 : -1) * startSpeed, 0), t => new Vector2((left ? 1 : -1) * (t < time ? -acc : acc), 0));
            if (Random.Range(0f,1f)< shootChance) {
                enemy.shooting.StartShootingFor(EnemyPatterns.PulsingBulletsRandomAngle(bullets[0], enemy.transform, bulletSpeed, shotRate, lines), time, shotRate * bulletNumber);
                }
        }, spawnRate, number);
    }

    
}
