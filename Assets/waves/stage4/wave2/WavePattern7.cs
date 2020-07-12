using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[System.Serializable]
struct WavePattern7Subwave
{
    public float spawnDelay;
    public float xPos;
    public float yPos;

}

public class WavePattern7 : EnemyWave 
/* 
Creates a singular tanky "miniboss" enemy that shoots rings of two different types of projectile
The smaller projectile comes out in pulses, while the bigger projectile is shot one ring at a time
The time between each pulse of small bullets and time between each wave of big bullets can be configured.
Along with the number of projectiles per ring and how long the enemy remains active.
*/
{
    //harder version
    [Header("Harder Version")]
    [SerializeField] bool harder = false;
    protected Bullet harderBullet;
    [SerializeField] int hardernumber = 40;
    [SerializeField] float harderradialvel = 3f, harderangularvel = 13f, harderpulseRate = 2f, harderdmg = 400f;
    //
    [Header("Settings")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] EnemyStats stats;
    [SerializeField] float bigBulletSpeed = 2f;
    [SerializeField] float smallBulletSpeed = 5f;
    [SerializeField] float timeBetweenBigBulletRing = 3f;
    [SerializeField] float timeBetweenSmallBulletPulse = 2f;
    [SerializeField] float smallBulletRingFireRate = 0.2f;
    [SerializeField] int numberOfSmallBulletsPerRing = 30;
    [SerializeField] int numberOfBigBulletsPerRing = 10;
    [SerializeField] int numberOfSmallBulletRingsPerPulse = 3;
    [SerializeField] float bulletDamage = 200f;
    [SerializeField] float stationaryTime = 15f;
    [SerializeField] List<WavePattern7Subwave> subwaveList;
    protected Enemy enemy;
    protected Bullet smallBullet;
    protected Bullet bigBullet;

    public virtual void SetUp()
    {

    }

    public override void SpawnWave()
    {
        SetUp();
        foreach (WavePattern7Subwave subwave in subwaveList) 
        {
            StartCoroutine(SpawnSubwave(subwave.spawnDelay, subwave.xPos, subwave.yPos));
        }
    }

    IEnumerator SpawnSubwave(float spawnDelay, float xPos, float yPos) 
    {
        yield return new WaitForSeconds(spawnDelay);
        this.StartCoroutine(SpawnEnemy(moveSpeed, xPos, yPos));
    }

    IEnumerator SpawnEnemy(float speed, float xPos, float yPos)
    {
        Vector2 initialPos = xPos == 0 
                            ? new Vector2(xPos, 4.2f)
                            : xPos < 0 
                            ? new Vector2(-4.2f, yPos)
                            : new Vector2(4.2f, yPos);
        
        Enemy enemy = Instantiate(this.enemy, initialPos, Quaternion.identity);
        enemy.SetEnemy(stats, false);
        float timeToPos = enemy.movement.MoveTo(new Vector2(xPos, yPos), moveSpeed);
        yield return new WaitForSeconds(timeToPos);

        if (enemy)
        {
            enemy.shooting.StartCoroutine(Functions.RepeatAction(()=>Patterns.RingOfBullets(bigBullet, bulletDamage, enemy.transform.position, numberOfBigBulletsPerRing, RandomOffset(), bigBulletSpeed,null)
                                                                , timeBetweenBigBulletRing));
            enemy.shooting.StartCoroutine(
                Functions.RepeatAction(()=>{
                    float offset = RandomOffset();
                    enemy.shooting.StartCoroutine
                    (Functions.RepeatActionXTimes(
                        ()=>Patterns.RingOfBullets(smallBullet, bulletDamage, enemy.transform.position, numberOfSmallBulletsPerRing, offset, smallBulletSpeed,null),
                    smallBulletRingFireRate, numberOfSmallBulletRingsPerPulse));
                }, 
                timeBetweenSmallBulletPulse));
            enemy.shooting.StartShooting(Functions.RepeatCustomAction(
                i => Patterns.SpirallingOutwardsRing(harderBullet, harderdmg, enemy.transform.position, harderradialvel, (i % 2 == 0 ? -1 : 1) * harderangularvel, hardernumber, 0, null), harderpulseRate));
        }

        yield return new WaitForSeconds(stationaryTime);

        if(enemy)
        {
            enemy.shooting.StopAllCoroutines();
            float timeToEnd = enemy.movement.MoveTo(initialPos, moveSpeed);
            Destroy(enemy.gameObject, timeToEnd);
        }
    }

    float RandomOffset() 
    {
        return Random.Range(0f, 360f);
    }
}
