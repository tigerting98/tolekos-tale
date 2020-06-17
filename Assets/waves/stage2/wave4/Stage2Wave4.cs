using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct Stage2Wave4SubWave
{
    public float y;
    public float timing;
    public int number;
    public bool left;
}

public class Stage2Wave4 : EnemyWave 
{
    
    // Start is called before the first frame update
    [Header("subwave")]
    [SerializeField] float movespeed, spawnRate;
    [SerializeField] EnemyStats stats;
    [SerializeField] List<Stage2Wave4SubWave> subwaves;
    [Header("Bullet Behavior")]
    [SerializeField] float shotRate = 0.2f, dmg = 150, shotSpeed = 3, delay = 0.5f;

    public override void SpawnWave()
    {
        foreach (Stage2Wave4SubWave subwave in subwaves) {
           StartCoroutine(SpawnSubWave(subwave.y, subwave.timing, subwave.number, subwave.left));
        }
    }

    IEnumerator SpawnSubWave(float y, float timing, int number, bool left) {
        Enemy en = GameManager.gameData.patternSprite;
        Debug.Log("h");
        yield return new WaitForSeconds(timing);
        StartCoroutine(Functions.RepeatActionXTimes(() =>
        {
            Enemy enemy = Instantiate(en, new Vector2(left? -4.1f : 4.1f, y), Quaternion.identity);
            enemy.SetEnemy(stats, false);
            enemy.movement.SetSpeed(new Vector2(left ? movespeed: -movespeed, 0));
            enemy.shooting.StartShootingAfter(EnemyPatterns.ShootAtPlayer(GameManager.gameData.ellipseBullet.GetItem(DamageType.Pure), dmg
                , enemy.transform, shotSpeed, shotRate), delay);

        }
        , spawnRate, number));
    
    }
}
