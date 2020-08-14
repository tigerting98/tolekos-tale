using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;



public class Stage3Wave2 : EnemyWave 
{

    // Start is called before the first frame update
    [Header("subwave")]
    [SerializeField] float spawnRate, shootingduration, delayBeforeMoving, enterSpeed = 8, leaveSpeed = 2;
    [SerializeField] EnemyStats stats;
    [SerializeField] int numberOfEnemy;
    [SerializeField] float delay = 5f;
    [SerializeField] float xBound, minY, maxY;
    Enemy enemy;
    [Header("Bullet Behavior")]
    [SerializeField] float shotRate = 0.05f, startSpeed = 2f, accelerationFactor = 1f, bulletDmg = 300, orientationangle = -135, angleRange;
    Bullet bullet;

    public override void SpawnWave()
    {
        enemy = GameManager.gameData.treeant;
        bullet = GameManager.gameData.leafBullet2;
        Functions.StartMultipleCustomCoroutines(this, i => SpawnEnemy(), numberOfEnemy, spawnRate);
        Functions.StartMultipleCustomCoroutines(this, i => SpawnEnemy(), numberOfEnemy, spawnRate);
        Invoke("SummonMidBoss", numberOfEnemy * spawnRate + delay);
    }

    void SummonMidBoss() 
    {
        GameManager.DestoryAllEnemyBullets();
        GameManager.DestroyAllNonBossEnemy(false);
        GameManager.SummonMidBoss();
    }
    IEnumerator SpawnEnemy()
    {
        Vector2 spawnPos = Functions.RandomLocation(-xBound, xBound, minY, maxY, true);
        Enemy em = Instantiate(enemy, new Vector2(GameManager.SupplyRandomFloat(-4f,4f), 4.1f), Quaternion.identity);
        em.SetEnemy(stats, false);
        float time = em.movement.MoveTo(spawnPos, enterSpeed);
        em.shooting.StartShootingFor(Functions.RepeatCustomAction(i =>
        {
            float angle = Functions.AimAtPlayer(em.transform) + Random.Range(-angleRange, angleRange);
            LeafDroppingBullet(em.transform.position, angle);

        }, shotRate), time, shootingduration + time);
        
        yield return new WaitForSeconds(time + delayBeforeMoving);
        if (em) {
            em.movement.destroyBoundary = 4.1f;
            em.movement.SetSpeed(leaveSpeed, spawnPos.x < 0 ? -135 : -45);
        }

    }
    Bullet LeafDroppingBullet(Vector2 origin, float angle) {

        Bullet bul = GameManager.bulletpools.SpawnBullet(bullet, origin);
        AudioManager.current.PlaySFX(GameManager.gameData.shortarrowSFX);
        bul.SetDamage(bulletDmg);
        bul.movement.SetAcceleration(Quaternion.Euler(0, 0, angle) * new Vector2(startSpeed, 0), t => new Vector2(Random.Range(-accelerationFactor, accelerationFactor), 0));
        bul.orientation.SetFixedOrientation(orientationangle);
        return bul;
    }
   




}
