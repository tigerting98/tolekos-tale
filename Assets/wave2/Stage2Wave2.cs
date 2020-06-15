using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Stage2Wave2 : EnemyWave 
{
    // Start is called before the first frame update
    [SerializeField] int number;
    [SerializeField] float startX, stopY, delay, moveSpeed, spawnRate;
    [Header("Bullet Behavior")]
    [SerializeField] float shotRate, shotRate2;
    [SerializeField] float speedDifference, minSpeed, angularVel, angularVel2;
    [SerializeField] int numberPerLine, lines;

    public override void SpawnWave() {
        Assert.IsTrue(number > 1);
        StartCoroutine(Functions.RepeatCustomActionXTimes(i => StartCoroutine(SpawnEnemy(-startX + i * 2 * startX / (number - 1))), spawnRate, number));
        Invoke("SpawnNextHalf", number * spawnRate );
    }

    IEnumerator SpawnEnemy(float x)
    {
        Enemy enemy = Instantiate(enemies[0], new Vector2(x, 4.1f), Quaternion.identity);
        float time = enemy.movement.MoveTo(new Vector2(x, stopY), moveSpeed);
        enemy.deathEffects.OnDeath += GameManager.DestoryAllEnemyBullets;
        yield return new WaitForSeconds(time);
        if (enemy) {
           
            EnemyPatterns.StartFanningPattern(bullets[0], enemy.shooting, minSpeed, angularVel,0,  lines, shotRate, numberPerLine, speedDifference);
        }
        yield return new WaitForSeconds(delay);
        if (enemy) {
            enemy.shooting.StopAllCoroutines();
            enemy.movement.SetSpeed(new Vector2(0, -moveSpeed));
        }
    }
    
    public void SpawnNextHalf() {
        StartCoroutine(SpawnEnemy2());
    }
    IEnumerator SpawnEnemy2() {
        Enemy enemy = Instantiate(enemies[1], new Vector2(0, 4.1f), Quaternion.identity);
        float time = enemy.movement.MoveTo(new Vector2(0, stopY), moveSpeed);
        enemy.deathEffects.OnDeath += GameManager.DestoryAllEnemyBullets;
        yield return new WaitForSeconds(time);
        if (enemy) {
            EnemyPatterns.StartFanningPattern(bullets[0], enemy.shooting, minSpeed, angularVel2, 180, 1, shotRate2, numberPerLine, speedDifference);
            EnemyPatterns.StartFanningPattern(bullets[0], enemy.shooting, minSpeed, -angularVel2, 0, 1, shotRate2, numberPerLine, speedDifference);
           
        }
    }

    
}
