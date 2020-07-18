using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct WavePattern8Subwave
{
    public float spawnDelay;
    public float highY;
    public float lowY;
    public int numberOfEnemies;
    public bool left;
    public DamageType type;

}
public class WavePattern8 : EnemyWave
{
    /*
    Creates a wave of enemies with randomized Y-coordinates streaming horizontally.
    Each enemy will constantly shoot lines aimed at the player
    On death, the enemy will explode into a ring of bullets
    */
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] EnemyStats stats;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float ringSpeed = 2f;
    [SerializeField] float shotRate = 0.1f;
    [SerializeField] float pulseRate = 1f;
    [SerializeField] float bulletDamage = 200f;
    [SerializeField] float stationaryTime = 1f;
    [SerializeField] int numberOfBulletsInDeathRing = 10;
    [SerializeField] int numberOfBulletsPerLine = 10;
    [SerializeField] List<WavePattern8Subwave> subwaveList;
    [SerializeField] float spawnRate;
    [SerializeField] bool random, ringsDependOnType, enemyDependOnType;
    protected BulletPack lineBullets, ringBullets;
    protected EnemyPack enemies;
    protected Enemy enemy;
    protected Bullet lineBullet;
    protected Bullet ringBullet;

    public virtual void SetUp()
    {

    }

    public override void SpawnWave()
    {
        SetUp();
        foreach (WavePattern8Subwave subwave in subwaveList) 
        {
            StartCoroutine(SpawnSubwave(subwave.spawnDelay, subwave.lowY, subwave.highY, subwave.numberOfEnemies, subwave.left, subwave.type));
        }
    }

    IEnumerator SpawnSubwave(float spawnDelay, float lowY, float highY, int numberOfEnemies, bool left, DamageType type) 
    {
        yield return new WaitForSeconds(spawnDelay);
        this.StartCoroutine(Functions.RepeatCustomActionXTimes(i => this.StartCoroutine(SpawnEnemy(moveSpeed, highY, lowY, left, type)), spawnRate, numberOfEnemies));
    }

    IEnumerator SpawnEnemy(float speed, float upperYBound, float lowerYBound, bool left, DamageType type)
    {
        float yPos = UnityEngine.Random.Range(lowerYBound, upperYBound);
        float initialX = left ? -4.2f : 4.2f;
        Enemy en = enemyDependOnType ? enemies.GetItem(type) : this.enemy;
        Enemy enemy = Instantiate(en, new Vector2(initialX, yPos), Quaternion.identity);
        enemy.SetEnemy(stats, false);
        float offset = Random.Range(0f, 360f);
        Bullet rbul;
        if (ringsDependOnType) {
            rbul = ringBullets.GetItem(type);
        } else {
            rbul = ringBullet;
        }
        enemy.deathEffects.OnDeath += () => Patterns.RingOfBullets(rbul, bulletDamage, enemy.transform.position, numberOfBulletsInDeathRing, offset, ringSpeed,null);
        float timeToMove = enemy.movement.MoveTo(new Vector2(-initialX, yPos), moveSpeed);
        enemy.shooting.StartCoroutine(
            Functions.RepeatAction(
                () => {
                    Bullet bullet;
                    if (random) {
                        float dice = Random.Range(0f, 3f);
                        bullet = dice < 1 ? lineBullets.GetItem(DamageType.Water) : dice < 2 ? lineBullets.GetItem(DamageType.Earth) : lineBullets.GetItem(DamageType.Fire);
                    } else {
                        bullet = lineBullet;
                    }
                    enemy.shooting.StartShootingFor(
                    EnemyPatterns.ShootAtPlayer(bullet, bulletDamage, enemy.transform, bulletSpeed, shotRate,GameManager.gameData.firestreamingSFX), 0, numberOfBulletsPerLine * shotRate);},
                numberOfBulletsPerLine * shotRate + pulseRate));
        Destroy(enemy.gameObject, timeToMove);
        yield return new WaitForSeconds(0.1f);
    }
}
