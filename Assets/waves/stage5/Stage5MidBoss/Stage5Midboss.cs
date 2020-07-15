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
    [SerializeField] float ringDamage = 300f, pageDamage = 200f, laserDamage = 100f;
    [SerializeField] float ringSpeed = 2f, pageSpeedMinY = 1f, pageSpeedMaxY = 2f, pageXFactor = 0.5f, laserPositionFactor = 0.3f;
    [SerializeField] float ringPulseRate = 2f, pageShotRate = 0.1f, laserShotRate = 1f, laserCooldown = 3f;
    [SerializeField] int numberOfBulletsPerRing = 20, numberOfLasersPairs = 5;


    public override void SpawnWave()
    {

        StartCoroutine(PreFight());

    }
    IEnumerator PreFight()
    {
        // Destroy(Instantiate(spawnEffect, spawnLocation - new Vector2(0, 0.5f), Quaternion.Euler(-90,0,0)).gameObject, 5f);
        yield return new WaitForSeconds(0.5f);
        bossImage = Instantiate(image, new Vector2(0f, 4.3f), Quaternion.identity);
        float wait = bossImage.GetComponent<Movement>().MoveTo(spawnLocation, 1.5f);
        yield return new WaitForSeconds(wait);
        StartPhase1();
    }

    void StartPhase1()
    {
        SpellCardUI(namesOfSpellCards[0]);
        Invoke("Phase1", spellCardTransition);
    }
    void Phase1()
    {
        currentBoss = Instantiate(boss, spawnLocation, Quaternion.identity);
        GameManager.currentBoss = currentBoss;
        SwitchToBoss();
        currentBoss.shooting.StartCoroutine(MoveInTriangle());
        Pattern1();
        currentBoss.bosshealth.OnLifeDepleted += EndPhase1;
    }

    void EndPhase1()
    {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase1;
        EndPhase();
        Invoke("StartPhase2", endPhaseTransition);
    }
    void StartPhase2()
    {
        SpellCardUI(namesOfSpellCards[1]);
        bossImage.GetComponent<Movement>().MoveTo(new Vector2(1f, 2f), 3f);
        Invoke("Phase2", spellCardTransition);
    }
    void Phase2()
    {
        SwitchToBoss();
        currentBoss.transform.rotation = Quaternion.identity;
        currentBoss.movement.SetPolarPath(t => new Polar(1f, 103 * t));
        currentBoss.shooting.StartCoroutine(Pattern2());
        currentBoss.bosshealth.OnLifeDepleted += EndPhase2;
    }

    void EndPhase2()
    {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase2;
        EndPhase();
        Destroy(bossImage.gameObject);
        OnDefeat?.Invoke();
    }


    void Pattern1()
    {
        Bullet circle1 = CreateOffsetCircle(bulletSpawnerOffset, 180f);
        Bullet circle2 = CreateOffsetCircle(-bulletSpawnerOffset, 180f);
        circle1.GetComponent<Shooting>().StartShooting(EnemyPatterns.CustomSpinningCustomBulletsCustomSpacing(angle => ReflectingBullet(GameManager.gameData.pageBullet, damage1, circle1.transform.position, angle, bulletSpeed1), i => 50f * i, t => -185f + spread * Mathf.Sin(2 * Mathf.PI * frequency * t), lines1, shotRate1));
        circle2.GetComponent<Shooting>().StartShooting(EnemyPatterns.CustomSpinningCustomBulletsCustomSpacing(angle => ReflectingBullet(GameManager.gameData.pageBullet, damage1, circle2.transform.position, angle, bulletSpeed1), i => 50f * i, t => -95f + spread * Mathf.Sin(2 * Mathf.PI * frequency * -t), lines1, shotRate1));
    }

    IEnumerator Pattern2()
    {
        currentBoss.shooting.StartShooting(Functions.RepeatAction(() => Patterns.RingOfBullets(GameManager.gameData.bigBullet.GetItem(DamageType.Water), ringDamage, currentBoss.transform.position, numberOfBulletsPerRing, Random.Range(0f, 360f), ringSpeed, null), ringPulseRate));
        currentBoss.shooting.StartShooting(Functions.RepeatAction(() => SpawnRandomVerticalBullet(true), pageShotRate));
        currentBoss.shooting.StartShooting(Functions.RepeatAction(() => SpawnRandomVerticalBullet(false), pageShotRate));
        currentBoss.shooting.StartShooting(Functions.RepeatAction(() => currentBoss.shooting.StartShooting(SpawnLaserPillars(true, laserShotRate, numberOfLasersPairs)), laserCooldown));
        currentBoss.shooting.StartShooting(Functions.RepeatAction(() => currentBoss.shooting.StartShooting(SpawnLaserPillars(false, laserShotRate, numberOfLasersPairs)), laserCooldown));
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
            if (index == trianglePath.Count - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
        }
    }

    Bullet CreateOffsetCircle(float offset, float angularVel)
    {
        Bullet circle = Instantiate(GameManager.gameData.earthCircle, (Vector2)currentBoss.transform.position + new Vector2(offset, 0f), Quaternion.identity);
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
        Bullet bullet = Patterns.ShootStraight(bul, dmg, origin, initialAngle, initialSpeed, null);
        bullet.movement.triggers.Add(reflectOnBound);
        return bullet;

    }

    Bullet SpawnRandomVerticalBullet(bool down)
    {
        Vector2 origin = down ? new Vector2(Random.Range(-4f, 4f), 4.2f) : new Vector2(Random.Range(-4f, 4f), -4.2f);
        Bullet bul = GameManager.bulletpools.SpawnBullet(GameManager.gameData.pageBullet, origin);
        bul.SetDamage(pageDamage);
        float speed = down ? Random.Range(-pageSpeedMaxY, -pageSpeedMinY) : Random.Range(pageSpeedMinY, pageSpeedMaxY);
        bul.movement.SetAcceleration(new Vector2(0, speed), t => new Vector2(UnityEngine.Random.Range(-pageXFactor, pageXFactor), 0));
        return bul;
    }

    IEnumerator SpawnLaserPillars(bool goRight, float timeBetweenPulse, int numOfLasers)
    {
        float spacing = 8f / numOfLasers;
        float spacingRandomOffset = laserPositionFactor * spacing;
        float startingXOffset = Random.Range(spacingRandomOffset, spacing - spacingRandomOffset);

        Vector2 spawn = goRight ? new Vector2(-4 + startingXOffset, -4f) : new Vector2(4 - startingXOffset, -4f);

        for (int i = 0; i < numOfLasers; i++)
        {
            Bullet laser = Instantiate(GameManager.gameData.fireBeam2, spawn, Quaternion.Euler(0, 0, 90));
            laser.orientation.SetFixedOrientation(90);
            Destroy(laser.gameObject, 2f);

            float deltaX = Random.Range(spacing - spacingRandomOffset, spacing + spacingRandomOffset);
            float deltaXDirection = goRight ? deltaX : -deltaX;

            spawn.x += deltaXDirection;

            yield return new WaitForSeconds(laserShotRate);
        }
    }

}
