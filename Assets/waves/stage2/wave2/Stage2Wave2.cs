using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Stage2Wave2 : EnemyWave 
{
    // Start is called before the first frame update
    [SerializeField] int number;
    [SerializeField] float startX, stopY, delay, moveSpeed1, moveSpeed2 = 1f, spawnRate;
    [SerializeField] EnemyStats bigStat, smallStat;
    [Header("Bullet Behavior")]
    [SerializeField] float shotRate, shotRate2;
    [SerializeField] float speedDifference, minSpeed, angularVel, angularVel2, dmg;
    [SerializeField] int numberPerLine, lines;
    [SerializeField] float beforeBossTime = 10f;
    Bullet bul;
    public override void SpawnWave() {
        bul = GameManager.gameData.pointedBullet.GetItem(DamageType.Water);
        Assert.IsTrue(number > 1);
        StartCoroutine(Functions.RepeatCustomActionXTimes(i => StartCoroutine(SpawnEnemy(-startX + i * 2 * startX / (number - 1))), spawnRate, number));
        Invoke("SpawnNextHalf", number * spawnRate );
    }

    IEnumerator SpawnEnemy(float x)
    {
        Enemy enemy = Instantiate(GameManager.gameData.waterFairy, new Vector2(x, 4.1f), Quaternion.identity);
        enemy.SetEnemy(smallStat, true);
        float time = enemy.movement.MoveTo(new Vector2(x, stopY), moveSpeed1);
        enemy.deathEffects.OnDeath += GameManager.DestoryAllEnemyBullets;
        yield return new WaitForSeconds(time);
        if (enemy) {
           
            EnemyPatterns.StartFanningPattern(bul, dmg, enemy.shooting, minSpeed, angularVel,0,  lines, shotRate, numberPerLine, speedDifference);
        }
        yield return new WaitForSeconds(delay);
        if (enemy) {
            enemy.shooting.StopAllCoroutines();
            enemy.movement.SetSpeed(new Vector2(0, -moveSpeed2));
        }
    }
    
    public void SpawnNextHalf() {
        StartCoroutine(SpawnEnemy2());
    }
    IEnumerator SpawnEnemy2() {
        Enemy enemy = Instantiate(GameManager.gameData.waterFairy, new Vector2(0, 4.1f), Quaternion.identity);
        enemy.SetEnemy(bigStat, true);
        enemy.transform.localScale = 1.5f * enemy.transform.localScale;
        float time = enemy.movement.MoveTo(new Vector2(0, stopY), moveSpeed1);
        enemy.deathEffects.OnDeath += GameManager.DestoryAllEnemyBullets;
        yield return new WaitForSeconds(time);
        if (enemy) {
            EnemyPatterns.StartFanningPattern(bul, dmg, enemy.shooting, minSpeed, angularVel2, 180, 1, shotRate2, numberPerLine, speedDifference);
            EnemyPatterns.StartFanningPattern(bul, dmg, enemy.shooting, minSpeed, -angularVel2, 0, 1, shotRate2, numberPerLine, speedDifference);
           
        }
        yield return new WaitForSeconds(beforeBossTime);
        GameManager.DestoryAllEnemyBullets();
        GameManager.DestroyAllNonBossEnemy(false);
        GameManager.SummonMidBoss();
    }

    
}
