using System;
using System.Collections;
using UnityEngine;


public class Stage1MidBoss : EnemyBossWave
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float endY = 2f;
    [Header("Ring Bullet pattern")]
    [SerializeField] int ballNumber;
    [SerializeField] float ballSpeed;
    [SerializeField] float ballShotRate;
    [SerializeField] float delay;
    [SerializeField] float numberofPulses;
    [Header("Straight Bullet pattern")]
    [SerializeField] int straightLines;
    [SerializeField] float straightSpeed;
    [SerializeField] float straightShotRate;
    [SerializeField] float spreadAngle;
    [SerializeField] float straightPulseTime;
    [SerializeField] float pauseTime;
    [SerializeField] float dmgBall = 100, dmgPointed = 50;



    public void Defeated() {
        OnDefeat?.Invoke();
        GameManager.DestoryAllEnemyBullets();
        DestroyAfter(5);
    }

    public override void SpawnWave() {
        StartCoroutine(BossPatterns());
    
    }
    IEnumerator BossPatterns() {
        currentBoss = Instantiate(boss, new Vector2(0, 4.2f), Quaternion.identity);
        currentBoss.GetComponent<BasicDroppable>().otherDrops.Add(GameManager.gameData.defaultBombDrop);
        GameManager.currentBoss = currentBoss;
        currentBoss.bosshealth.OnDeath += Defeated;
        float time = currentBoss.movement.MoveTo(new Vector2(0, endY), movementSpeed);
        yield return new WaitForSeconds(time);
        if (currentBoss)
        {
            for (int i = 0; i < numberofPulses; i++)
            {
                currentBoss.shooting.StartShootingAfter(EnemyPatterns.PulsingBulletsRandom(GameManager.gameData.smallRoundBullet.GetAllItems(), dmgBall, 
                    currentBoss.transform, ballSpeed, ballShotRate, ballNumber,bulletSpawnSound),
                  delay * i);
            }
            while (currentBoss) {
                currentBoss.shooting.StartShootingFor(EnemyPatterns.ShootAtPlayerWithLines(GameManager.gameData.pointedBullet.GetItem(UnityEngine.Random.Range(0, 4)), dmgPointed, currentBoss.transform,
                    straightSpeed, straightShotRate, spreadAngle, straightLines, bulletSpawnSound), 0, straightPulseTime);
                yield return new WaitForSeconds(pauseTime);
            }
        }

    }
}
