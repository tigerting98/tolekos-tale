using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms;

public class Stage2EndBoss : EnemyBossWave
{
    // Start is called before the first frame update
    [SerializeField] GameObject key, image;
    [SerializeField] Dialogue dialogue1, dialogue2, endDialogue;
    [SerializeField] float spawnY;

    [SerializeField] BulletPack bigBullet, ellipseBullet, pointedBullet, smallBullet, arrowBullet;
    [SerializeField] Bullet magicCircle;

    [Header("Pattern 1")]
    [SerializeField] float timeToRadius, radius;
    [SerializeField] float bigBulletSpeed1, bigBulletSpread, bigBulletShotRate;
    [SerializeField] int bigBulletNumber;
    [SerializeField] float initialSpeed1, radius1, finalSpeed1, smallBulletdelay1, angularVel, shotRate1, pulseRate1;
    [SerializeField] int numberOfRings1, numberPerRings1;

    [Header("Pattern 2")]
    [SerializeField] float lineBulletSpeed2, circleBulletSpeed, lineshotRate2, spacing, delay, lineduration, pulseRate2;
    [SerializeField] float CircleBullets2shotRate, spread2, circlebulletpulseRate2, bossmovespeed, pulseDuration2;
    [SerializeField] int numberOfCircleBullets2;

    [Header("Pattern 3")]
    [SerializeField] float radius3, pulseRate3, angleperPulse3, smallspeed3;
    [SerializeField] int numberOfsmallBullet3, numberOfLargeBullet3;
    [SerializeField] float shotRateLarge3, spreadAngleLarge3, largespeed3;

    [Header("Pattern 4")]
    [SerializeField] float spawnradius4, initialSpeed4, finalSpeed4, shotRate4, pulseRate4;
    [SerializeField] int spawnRatio4, numberPerPulse4;

    [Header("Patttern5")]
    [SerializeField] Enemy waterfairy, slime;
    [SerializeField] float bossShootRate5, bossShootSpeed5, bossshootSpread5; 
    [SerializeField] int bossShootNumber5;

    [Header("FairyBehavior")]
    [SerializeField] float spawnRatefairy, shotRatefairy, speedbulletfairy, speedfairy, fairypulseRate, fairypulseDuration, fairyangularVel, fairyamp, fairyY;
    [Header("SlimeBehavior")]
    [SerializeField] float spawnRateslime, shotRateslime, speedbulletslime, speedslime;
    [SerializeField] int numberofShotsSlime, numberofexplodingBullets;
    [SerializeField] float explodingBulletSpeedFast, explodingBulletSpeedSlow;
    public override void SpawnWave() {
        StartCoroutine(PreFight1());
    }
    IEnumerator PreFight1() {
        try
        {
            GameObject.Find("stage2quad").GetComponent<Animator>().SetTrigger("StopMoving");
        }
        catch (Exception ex) { Debug.Log(ex.ToString()); }
        yield return new WaitForSeconds(1f);
        Instantiate(key, new Vector2(0, 3.5f), Quaternion.identity);
        StartCoroutine(DialogueManager.StartDialogue(dialogue1, () => StartCoroutine(AfterDialogue1())));


    }

    IEnumerator AfterDialogue1() {
        bossImage = Instantiate(image, new Vector2(0, spawnY), Quaternion.identity);
        
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(DialogueManager.StartDialogue(dialogue2, Phase1));
    }

    void Phase1() {
        currentBoss = Instantiate(boss, new Vector2(0, spawnY), Quaternion.identity);
        GameManager.currentBoss = currentBoss;
        currentBoss.shooting.StartCoroutine(Pattern1());
        SwitchToBoss();
        currentBoss.bosshealth.OnLifeDepleted += EndPhase1;
    }

    void EndPhase1() {
        EndPhase();
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase1;
        Invoke("Phase2", 1f);
    }

    void Phase2() {
        SpellCardUI(namesOfSpellCards[0]);
        Invoke("Pattern2", 2f);
    }

    void Pattern2() {
        SwitchToBoss();
         float y = 4f;
         int i = 0;
         while (y > -4.1f) {
            float y1 = y;
            int i1 = i;
             currentBoss.shooting.StartShootingAfter(EnemyPatterns.RepeatSubPatternWithInterval(() => SummonLine(pointedBullet.GetBullet(DamageType.Water), i1 % 2 == 0, y1, lineduration)
             , currentBoss.shooting, pulseRate2), delay * i1);
             i++;
             y -= spacing / 2;
         }
         y = 4f - spacing / 4;
        i = 0;
         while (y > -4.1f)
         {
            float y1 = y;
            int i1 = i;
            currentBoss.shooting.StartShootingAfter(EnemyPatterns.RepeatSubPatternWithInterval(() => SummonLine(pointedBullet.GetBullet(DamageType.Water), i1 % 2 == 0, y1, lineduration)
              , currentBoss.shooting, pulseRate2), pulseRate2/2+ delay * i1);
             i++;
             y -= spacing / 2;
         }
        currentBoss.shooting.StartCoroutine(MovingAndShooting());
        currentBoss.bosshealth.OnLifeDepleted += EndPhase2;


    }
    void EndPhase2()
    {
        EndPhase();
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase2;
        Invoke("Phase3", 1f);
    }

    void Phase3() {
        SwitchToBoss();
        float time = currentBoss.movement.MoveTo(new Vector2(0, 2), bossmovespeed);
        Bullet small = smallBullet.GetBullet(DamageType.Water);
        Bullet arrow = arrowBullet.GetBullet(DamageType.Water);

        currentBoss.shooting.StartShootingAfter(Functions.RepeatCustomAction(i => Patterns.RingOfBullets(small, (Vector2)currentBoss.transform.position + new Polar(radius3, i * angleperPulse3).rect
            , numberOfsmallBullet3, UnityEngine.Random.Range(0, 360), smallspeed3), pulseRate3), time);
        currentBoss.shooting.StartShootingAfter(Functions.RepeatCustomAction(i => Patterns.RingOfBullets(arrow, (Vector2)currentBoss.transform.position + Functions.RandomLocation(-1,1,-1,1)
            , numberOfsmallBullet3, UnityEngine.Random.Range(0, 360), smallspeed3), pulseRate3), time + UnityEngine.Random.Range(0, pulseRate3));
        currentBoss.shooting.StartShootingAfter(Functions.RepeatCustomAction(i => Patterns.ShootMultipleStraightBullet(bigBullet.GetBullet(i % 4),
            currentBoss.transform.position, largespeed3, Patterns.AimAt(currentBoss.transform.position, GameManager.playerPosition), spreadAngleLarge3, numberOfLargeBullet3), shotRateLarge3), time);
        currentBoss.bosshealth.OnLifeDepleted += EndPhase3;
    }

    void EndPhase3() {
        EndPhase();
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase3;
        Invoke("Phase4", 1f);
    }

    void Phase4() {
        SpellCardUI(namesOfSpellCards[1]);
        Invoke("Pattern4", 2f);
    }

    void Pattern4() {
        SwitchToBoss();
        float time = currentBoss.movement.MoveTo(new Vector2(0, 1), bossmovespeed);
        currentBoss.shooting.StartShootingAfter(EnemyPatterns.RepeatSubPatternWithInterval(pulseOfRain, currentBoss.shooting, pulseRate4),time);
        currentBoss.bosshealth.OnLifeDepleted += EndPhase4;
    }

    void EndPhase4() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase4;
        EndPhase();

        Invoke("Phase5", 1f);
    }

    void Phase5() {
        SpellCardUI(namesOfSpellCards[2]);
        Invoke("Pattern5", 2f);

    }

    void Pattern5() {
        SwitchToBoss();
        float time = currentBoss.movement.MoveTo(new Vector2(0,2), bossmovespeed);
        currentBoss.shooting.StartShootingAfter(Functions.RepeatCustomAction(i => Patterns.ShootMultipleStraightBullet(bigBullet.GetBullet(i % 4),
           currentBoss.transform.position, bossShootSpeed5, Patterns.AimAt(currentBoss.transform.position, GameManager.playerPosition), bossshootSpread5, bossShootNumber5), bossShootRate5), time);
        currentBoss.shooting.StartShootingAfter(SummonWaterFairy(), time);
        currentBoss.shooting.StartShootingAfter(SummonSlime(), time);
        currentBoss.health.OnDeath += EndPhase5;
    }

    void EndPhase5() {
        EndPhase();
        Destroy(bossImage);
        Invoke("EndDialogue", 1f);
    }

    void EndDialogue() {
        StartCoroutine(DialogueManager.StartDialogue(endDialogue, NextStage));
    }


    IEnumerator Pattern1() {
        Bullet magicCircle1 = EnemyPatterns.SummonMagicCircle(magicCircle, currentBoss.transform, timeToRadius, 210, radius, 0);
        Bullet magicCircle2 = EnemyPatterns.SummonMagicCircle(magicCircle, currentBoss.transform, timeToRadius, 330, radius, 0);
        Bullet magicCircleTop = EnemyPatterns.SummonMagicCircle(magicCircle, currentBoss.transform, timeToRadius, 90, radius, 0);
        yield return new WaitForSeconds(timeToRadius);
        Shooting top = magicCircleTop.GetComponent<Shooting>();
        top.StartShooting(EnemyPatterns.ShootAtPlayerWithLines(bigBullet.GetBullet(DamageType.Fire), magicCircleTop.transform, bigBulletSpeed1, bigBulletShotRate, bigBulletSpread, bigBulletNumber));
        Bullet bul = ellipseBullet.GetBullet(DamageType.Water);
        SubPattern1(magicCircle1, true, bul);
        SubPattern1(magicCircle2, false, bul);
    }


    void SubPattern1(Bullet magicCircle, bool left, Bullet bul){
        Shooting shooting = magicCircle.GetComponent<Shooting>();
        float angular = left ? angularVel : -angularVel;
        Functions.StartMultipleCustomCoroutines(shooting, i => Functions.RepeatAction(() => EnemyPatterns.OutAndSpinRingOfBullets(bul, magicCircle.transform, initialSpeed1, radius1, finalSpeed1, angular, smallBulletdelay1, 0, numberPerRings1), pulseRate1), numberOfRings1, shotRate1);
    }



    IEnumerator SummonLine(Bullet bul, bool left, float y, float duration) {
        return Functions.RepeatActionXTimes(() => Patterns.ShootStraight(bul, new Vector2(left ? -4.1f : 4.1f, y), left ? 0 : 180,lineBulletSpeed2), lineshotRate2,  (int)(duration / lineshotRate2));
    }


    IEnumerator MovingAndShooting() {
        while (currentBoss) {
            float time = currentBoss.movement.MoveTo(Functions.RandomLocation(1, 3, 1, 3), bossmovespeed);

            currentBoss.shooting.StartShootingFor(EnemyPatterns.ShootAtPlayerWithLines(smallBullet.GetBullet(DamageType.Water), currentBoss.transform, circleBulletSpeed, CircleBullets2shotRate, spread2, numberOfCircleBullets2), time, pulseDuration2);
            yield return new WaitForSeconds(circlebulletpulseRate2);
        }
    }

    void SmallBullets(Vector2 position, Bullet smallBullet, int number) {
        float angle = Patterns.AimAt(new Vector2(0, 0), position);
        float shootangle = (Mathf.RoundToInt(angle / 90 + 2) % 4) * 90;
        for (int i = 0; i < number; i++)
        {
            Patterns.ShootStraight(smallBullet, position, shootangle + UnityEngine.Random.Range(-90f, 90f), finalSpeed4);
        }
        

    }
    Bullet ShootRain(Bullet bigBullet, Bullet smallBullet, Vector2 playerPosition) {
        Vector2 spawn = Functions.RandomLocation(currentBoss.transform.position, spawnradius4);

        Bullet bul = Patterns.ShootStraight(bigBullet, spawn, Patterns.AimAt(spawn, playerPosition), initialSpeed4);
        bul.movement.destroyBoundary = 4f;
        bul.movement.OnOutOfBound += pos => SmallBullets(pos, smallBullet, spawnRatio4);
        return bul;
    }



    IEnumerator pulseOfRain() {
        Vector2 pos = GameManager.playerPosition;
        return Functions.RepeatActionXTimes(() => ShootRain(pointedBullet.GetBullet(DamageType.Water), arrowBullet.GetBullet(DamageType.Water), pos),shotRate4, numberPerPulse4) ;
    }
    IEnumerator SummonWaterFairy() {
        Bullet waterBullet = pointedBullet.GetBullet(DamageType.Water);
        while (true)
        {
            float x = UnityEngine.Random.Range(-3.5f, 3.5f);
            Enemy fairy = Instantiate(waterfairy, new Vector2(x, 4.1f), Quaternion.identity);
            float time = fairy.movement.MoveTo(new Vector2(x, fairyY), speedfairy);
            fairy.shooting.StartShootingAfter(
                Functions.RepeatAction(() =>
                {
                    float angle = Patterns.AimAt(fairy.transform.position, GameManager.playerPosition);
                    fairy.shooting.StartShootingFor(EnemyPatterns.ShootSine(waterBullet, fairy.transform, angle,
                    speedbulletfairy, shotRatefairy, fairyangularVel, fairyamp), 0, fairypulseDuration);
                }, fairypulseRate), time);
            yield return new WaitForSeconds(spawnRatefairy);
        }
        
    }
    IEnumerator SummonSlime() { 
        Bullet waterBullet = smallBullet.GetBullet(DamageType.Water);
        while (true)
        {
            Debug.Log("Summoned");
            Enemy en = Instantiate(slime, currentBoss.transform.position, Quaternion.identity);
            float angle = Patterns.AimAt(en.transform.position, GameManager.playerPosition);
            en.movement.SetSpeed(Quaternion.Euler(0, 0, angle) * new Vector2(speedslime, 0));
            en.movement.destroyBoundary = 4f;
            en.shooting.StartShootingAfter(EnemyPatterns.PulsingBulletsRandomAngle(waterBullet, en.transform, speedbulletslime, shotRateslime, numberofShotsSlime), 0.5f);
            en.movement.OnOutOfBound += SlimeOut;
            yield return new WaitForSeconds(spawnRateslime);
        }
    }

    void SlimeOut(Vector2 pos) {
        Bullet water = arrowBullet.GetBullet(DamageType.Water);
        float angle = UnityEngine.Random.Range(0f, 360f);
        Patterns.RingOfBullets(water, pos, numberofexplodingBullets, angle, explodingBulletSpeedSlow);
        Patterns.RingOfBullets(water, pos, numberofexplodingBullets, angle, explodingBulletSpeedFast);
    }


    
}
