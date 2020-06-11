using System;
using System.Collections;
using UnityEngine;


public class Stage1MidBoss : EnemyWave
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float endY = 2f;
    [SerializeField] Boss bossEnemy;
    [Header("Ring Bullet pattern")]
    [SerializeField] BulletPack balls;
    [SerializeField] int ballNumber;
    [SerializeField] float ballSpeed;
    [SerializeField] float ballShotRate;
    [SerializeField] float delay;
    [SerializeField] float numberofPulses;
    [Header("Straight Bullet pattern")]
    [SerializeField] BulletPack roundedBullets;
    [SerializeField] int straightLines;
    [SerializeField] float straightSpeed;
    [SerializeField] float straightShotRate;
    [SerializeField] float spreadAngle;
    [SerializeField] float straightPulseTime;
    [SerializeField] float pauseTime;




    public event Action OnDefeat;
    public void Defeated() {
        OnDefeat?.Invoke();
        GameManager.DestoryAllEnemyBullets();
        DestroyAfter(5);
    }

    public override void SpawnWave() {
        StartCoroutine(BossPatterns());
    
    }
    IEnumerator BossPatterns() {
        Boss boss = Instantiate(bossEnemy, new Vector2(0, 4.2f), Quaternion.identity);
        GameManager.currentBoss = boss;
        boss.bosshealth.OnDeath += Defeated;
        float time = boss.movement.MoveTo(new Vector2(0, endY), movementSpeed);
        yield return new WaitForSeconds(time);
        if (boss)
        {
            for (int i = 0; i < numberofPulses; i++)
            {
                boss.shooting.StartShootingAfter(EnemyPatterns.PulsingBulletsRandom(balls.GetAllBullets(), boss.transform, ballSpeed, ballShotRate, ballNumber),
                  delay * i);
                boss.enemyAudio.PlayAudio(bulletSpawnSound, ballShotRate, delay * i);
            }
            while (true && boss) {
                boss.shooting.StartShootingFor(EnemyPatterns.ShootAtPlayerWithLines(roundedBullets.GetBullet(UnityEngine.Random.Range(0, 4)), boss.transform,
                    straightSpeed, straightShotRate, spreadAngle, straightLines), 0, straightPulseTime);
                boss.enemyAudio.PlayAudioDuration(bulletSpawnSound, straightShotRate, straightPulseTime);
                yield return new WaitForSeconds(pauseTime);
            }
        }

    }
}
