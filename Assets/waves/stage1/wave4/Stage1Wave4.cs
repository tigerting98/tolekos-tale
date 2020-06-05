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
    [Header("SpawnRate")]
    [SerializeField] float spawnRateMin = 0.5f;
    [SerializeField] float spawnRateMax = 1f;
    [SerializeField] float number = 50;
    [Header("Bullet Behavior")]
    [SerializeField] BulletPack bullet1;
    [SerializeField] BulletPack bullet2;
    [SerializeField] int minLines = 2;
    [SerializeField] int maxLines = 5;
    [SerializeField] float bulletSpeedMin = 1f;
    [SerializeField] float bulletSpeedMax = 4f;
    [SerializeField] float shotRateMin = 1f;
    [SerializeField] float shotRateMax = 5f;

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
    [SerializeField] BulletPack pack;
    [SerializeField] float bulletSpeed2 = 2f;
    [SerializeField] float angle = 15f;
    [SerializeField] int lines = 1;
    [Header("Spawn Sound")]
    [SerializeField] AudioClip clip;
    [SerializeField] float volume2 = 0.1f;
    public override void SpawnWave() {
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
        GameManager.SummonBoss();

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
        Enemy enemy = Instantiate(enemies[0], spawnLocation, Quaternion.identity);
        enemy.movement.SetSpeed((pivot - spawnLocation).normalized * Random.Range(moveSpeedMin, moveSpeedMax));
        enemy.movement.SetDestroyWhenOut(true);
        SetShooting(enemy);

    }

    void SetShooting(Enemy enemy) {
        float shotRate = Random.Range(shotRateMin, shotRateMax);
        enemy.shooting.ShootWhenInBound(EnemyPatterns.PulsingBulletsRandomAngle(bullet1.GetBullet(Random.Range(0, 4)), enemy,
            Random.Range(bulletSpeedMin, bulletSpeedMax), shotRate, Random.Range(minLines, maxLines+1)));
        enemy.shooting.ShootWhenInBound(EnemyPatterns.PulsingBulletsRandomAngle(bullet2.GetBullet(Random.Range(0, 4)), enemy,
            Random.Range(bulletSpeedMin, bulletSpeedMax), shotRate, Random.Range(minLines, maxLines + 1)));
        
    }

    void SecondHalf() {
        for (int i = 0; i < number2; i++) {
            float x = -3.5f + 7 * i / (number2 - 1);
            StartCoroutine(enemy2(x));
        
        }
    
    }
    IEnumerator enemy2(float x) {
        Enemy enemy = Instantiate(enemies[0], new Vector2(x, posY), Quaternion.identity);
        AudioSource.PlayClipAtPoint(clip, GameManager.mainCamera.transform.position, volume2);
        yield return new WaitForSeconds(delay);
        if (enemy)
        { enemy.shooting.StartShootingFor(EnemyPatterns.ShootAtPlayerWithLines(pack.GetBullet(0), enemy.transform, bulletSpeed2, shotRate2, angle, lines), 0, duration);
        }
        yield return new WaitForSeconds(duration + pause);
        if (enemy) {
           float time =  enemy.movement.MoveTo(new Vector2(x, 4.5f), speed);
            enemy.DestroyAfter(time);
        }
    }
}

