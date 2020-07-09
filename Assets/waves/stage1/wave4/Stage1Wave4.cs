//using System;
using System.Collections;

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms;
using UnityEngine.Windows.Speech;
using UnityEngine.XR.WSA;

public class Stage1Wave4 : EnemyWave
{
    // Start is called before the first frame update

    [Header("Sprite Movement Behaviour")]
    [SerializeField] float moveSpeedMin = 1f;
    [SerializeField] float moveSpeedMax = 4f;
    [SerializeField] float centreminX = -3f;
    [SerializeField] float centremaxX = 3f;
    [SerializeField] float centreminY = -3f;
    [SerializeField] float centremaxY = 3f;
    [SerializeField] float minSpawnY = 0f;
    [SerializeField] EnemyStats stats;
    [Header("SpawnRate")]
    [SerializeField] float spawnRateMin = 0.5f;
    [SerializeField] float spawnRateMax = 1f;
    [SerializeField] float number = 50;
    [Header("Bullet Behavior")]
    [SerializeField] int minLines = 2;
    [SerializeField] int maxLines = 5;
    [SerializeField] float bulletSpeedMin = 1f;
    [SerializeField] float bulletSpeedMax = 4f;
    [SerializeField] float shotRateMin = 1f;
    [SerializeField] float shotRateMax = 5f;
    [SerializeField] float dmg1 = 100;

    [Header("Second Wave")]
    [SerializeField] float arrivalDelay = 1f;
    [SerializeField] float posY = 3f;
    [SerializeField] float number2 = 10;
    [SerializeField] float shotRate2 = 0.1f;
    [SerializeField] float duration = 1f;
    [SerializeField] float delay = 1f;
    [SerializeField] float pause = 1f;
    [SerializeField] float speed = 2f;
    [Header("Second Wave Bullets")]
    [SerializeField] float bulletSpeed2 = 2f;
    [SerializeField] float angle = 15f;
    [SerializeField] int lines = 1;
    [SerializeField] float dmg2 = 50;
    [Header("Spawn Sound")]
    [SerializeField] SFX tpSFX;
    Enemy en;
    public override void SpawnWave() {
        en = GameManager.gameData.patternSprite;
        StartCoroutine(TheWave());


    }
    
    IEnumerator TheWave() {
        for (int i = 0; i < number; i++){
            SpawnEnemy();
            yield return new WaitForSeconds(Random.Range(spawnRateMin, spawnRateMax));
        }
        yield return new WaitForSeconds(arrivalDelay);
            SecondHalf();
        yield return new WaitForSeconds(3);
        GameManager.SummonEndBoss();
        DestroyAfter(5f);

    }

    void SpawnEnemy() {
        float boundary = 4.2f;
        float ran = Random.Range(0f, 1f);
        float sideChance = (boundary - minSpawnY) / ((boundary - minSpawnY) * 2 + boundary*2);
        Vector2 spawnLocation;
        if (ran < sideChance)
        {
            spawnLocation = new Vector2(-boundary, minSpawnY + (boundary - minSpawnY) * (ran / sideChance));

        }
        else if (ran > 1 - sideChance)
        {
            spawnLocation = new Vector2(boundary, minSpawnY + (boundary - minSpawnY) * ((1 - ran) / sideChance));

        }
        else {
            spawnLocation = new Vector2(-boundary + boundary * 2 * ((ran - sideChance) / (1 - 2 * sideChance)), boundary);
        
        }

        Vector2 pivot = new Vector2(Random.Range(centreminX, centremaxX), Random.Range(centreminY, centremaxY));
        Enemy enemy = Instantiate(en, spawnLocation, Quaternion.identity);
        en.SetEnemy(stats, false);
        enemy.movement.SetSpeed((pivot - spawnLocation).normalized * Random.Range(moveSpeedMin, moveSpeedMax));
        enemy.movement.destroyBoundary = 4.5f;
        SetShooting(enemy);

    }

    void SetShooting(Enemy enemy) {
        float shotRate = Random.Range(shotRateMin, shotRateMax);
        Bullet bul1 = GameManager.gameData.arrowBullet.GetItem(Random.Range(0, 4));
        Bullet bul2 = GameManager.gameData.ellipseBullet.GetItem(Random.Range(0, 4));
        enemy.shooting.ShootWhenInBound(EnemyPatterns.PulsingBulletsRandomAngle(bul1, dmg1, enemy.transform,
            Random.Range(bulletSpeedMin, bulletSpeedMax), shotRate, Random.Range(minLines, maxLines+1),null));
        enemy.shooting.ShootWhenInBound(EnemyPatterns.PulsingBulletsRandomAngle(bul2, dmg1, enemy.transform,
            Random.Range(bulletSpeedMin, bulletSpeedMax), shotRate, Random.Range(minLines, maxLines + 1),null));
        
    }

    void SecondHalf() {
        for (int i = 0; i < number2; i++) {
            float x = -3.5f + 7 * i / (number2 - 1);
            StartCoroutine(enemy2(x));
        
        }
    
    }
    IEnumerator enemy2(float x) {
        Enemy enemy = Instantiate(en, new Vector2(x, posY), Quaternion.identity);
        enemy.SetEnemy(stats, false);
        Bullet bul = GameManager.gameData.whiteArrowBullet;
        tpSFX.PlayClip();
        yield return new WaitForSeconds(delay);
        if (enemy)
        { enemy.shooting.StartShootingFor(EnemyPatterns.ShootAtPlayerWithLines(bul, dmg2, enemy.transform, bulletSpeed2, shotRate2, angle, lines,null), 0, duration);
        }
        yield return new WaitForSeconds(duration + pause);
        if (enemy) {
           float time =  enemy.movement.MoveTo(new Vector2(x, 4.5f), speed);
            enemy.DestroyAfter(time);
        }
    }
}

