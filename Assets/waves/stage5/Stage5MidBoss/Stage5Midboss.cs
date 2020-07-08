using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5Midboss : EnemyBossWave
{
    [SerializeField] ParticleSystem spawnEffect;
    [SerializeField] GameObject image;
    [SerializeField] Vector2 spawnLocation;

    [Header("Pattern1")]
    [SerializeField] float damage1;
    [SerializeField] float bulletSpawnerOffset;
    [SerializeField] float minStationaryTime = 1f, maxStationaryTime = 2f;
    [SerializeField] List<Vector2> trianglePath;
    [SerializeField] float moveSpeed1 = 3.5f;
    [SerializeField] float bulletSpeed1 = 3f;
    [SerializeField] float spread = 135f;
    [SerializeField] float frequency = 0.5f;
    [SerializeField] float shotRate1 = 0.05f;
    [SerializeField] int lines1 = 1;

    [Header("Pattern2")]
    [SerializeField] Bullet bookBullet;
    [SerializeField] Bullet pageBullet;

    [Header("Pattern3")]
    [SerializeField] Bullet groundLaser;

    public override void SpawnWave() {
        
        StartCoroutine(PreFight());
    
    }
    IEnumerator PreFight() {
        // Destroy(Instantiate(spawnEffect, spawnLocation - new Vector2(0, 0.5f), Quaternion.Euler(-90,0,0)).gameObject, 5f);
        yield return new WaitForSeconds(0.5f);
        bossImage = Instantiate(image, spawnLocation, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        StartPhase1();
    }

    void StartPhase1() {
        SpellCardUI(namesOfSpellCards[0]);
        Invoke("Phase1", spellCardTransition);
    }
    void Phase1() {
        currentBoss = Instantiate(boss, spawnLocation, Quaternion.identity);
        GameManager.currentBoss = currentBoss;
        SwitchToBoss();
        currentBoss.shooting.StartCoroutine(MoveInTriangle());
        Pattern1();
        currentBoss.bosshealth.OnLifeDepleted += EndPhase1;
    }

    void EndPhase1() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase1;
        EndPhase();
        Invoke("StartPhase2", endPhaseTransition);
    }
    void StartPhase2() {
        SpellCardUI(namesOfSpellCards[1]);
        bossImage.GetComponent<Movement>().MoveTo(spawnLocation, 3f);
        Invoke("Phase2", spellCardTransition);
    }
    void Phase2() {
        SwitchToBoss();
        currentBoss.transform.rotation = Quaternion.identity;
        currentBoss.shooting.StartCoroutine(Pattern2());
        currentBoss.bosshealth.OnLifeDepleted  += EndPhase2;
    }

    void EndPhase2() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase2;
        EndPhase();
        Destroy(bossImage.gameObject);
        OnDefeat?.Invoke();
    }


    void Pattern1()
    {
        Bullet circle1 = CreateOffsetCircle(bulletSpawnerOffset, 180f);
        Bullet circle2 = CreateOffsetCircle(-bulletSpawnerOffset, 180f);
        circle1.GetComponent<Shooting>().StartShooting(EnemyPatterns.CustomSpinningCustomBulletsCustomSpacing(angle => ReflectingBullet(GameManager.gameData.pageBullet, damage1, circle1.transform.position, angle, bulletSpeed1), i => 50f * i,t => -185f + spread * Mathf.Sin(2 * Mathf.PI * frequency * t), lines1, shotRate1));
        circle2.GetComponent<Shooting>().StartShooting(EnemyPatterns.CustomSpinningCustomBulletsCustomSpacing(angle => ReflectingBullet(GameManager.gameData.pageBullet, damage1, circle2.transform.position, angle, bulletSpeed1), i => 50f * i,t => -95f + spread * Mathf.Sin(2 * Mathf.PI * frequency * -t), lines1, shotRate1));
    }

    IEnumerator Pattern2()
    {
        yield return new WaitForSeconds(1f);
    }


    IEnumerator MoveInTriangle() 
    {
        int index = 0;
        while (true) 
        {
            float stationaryTime = Random.Range(minStationaryTime, maxStationaryTime);
            yield return new WaitForSeconds(stationaryTime);
            float timeToMove = currentBoss.movement.MoveTo(trianglePath[index], moveSpeed1);
            yield return new WaitForSeconds(timeToMove);
            if (index == trianglePath.Count - 1) {
                index = 0;
            } else {
                index ++;
            }
        }
    }

    Bullet CreateOffsetCircle(float offset, float angularVel)
    {
        Bullet circle = Instantiate(GameManager.gameData.earthCircle, (Vector2)currentBoss.transform.position + new Vector2(offset, 0f),Quaternion.identity);
        circle.orientation.StartRotating(angularVel, 0f);
        circle.transform.parent = currentBoss.transform;
        return circle;
    }

    Bullet ReflectingBullet(Bullet bul, float dmg, Vector2 origin, float initialAngle, float initialSpeed)
    {
        ActionTrigger<Movement> reflectOnBound = new ActionTrigger<Movement>(
        movement => !Functions.WithinBounds(movement.transform.position, 4f) && movement.transform.position.y > -4);
        reflectOnBound.OnTriggerEvent += movement =>
        {
            Vector2 pos = movement.transform.position;
            if (pos.y >= 4)
            {


                movement.transform.position = new Vector2(movement.transform.position.x, 3.99f);
                movement.graph = Movement.ReflectPathAboutX(movement.graph);
            }
            else
            {


                movement.graph = Movement.ReflectPathAboutY(movement.graph);
                movement.ResetTriggers();

            }
        };
        Bullet bullet = Patterns.ShootStraight(bul, dmg, origin, initialAngle, initialSpeed);
        bullet.movement.triggers.Add(reflectOnBound);
        return bullet;

    }

    void End()
    {
        EndPhase();
        Destroy(bossImage);
        OnDefeat?.Invoke();
        DestroyAfter(5);
    }
}
