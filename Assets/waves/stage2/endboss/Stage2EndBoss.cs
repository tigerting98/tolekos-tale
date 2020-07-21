using System;
using System.Collections;
using UnityEngine;


public class Stage2EndBoss : EnemyBossWave
{
    // Start is called before the first frame update
    [SerializeField] GameObject key, image;
    [SerializeField] Dialogue dialogue1, dialogue2, endDialogue;
    [SerializeField] float spawnY;

     Bullet ellipseBullet, pointedBullet, smallBullet, arrowBullet;
    BulletPack bigBullets;
    Bullet magicCircle;
    [SerializeField] ParticleSystem spawnEffect;
    [SerializeField] bool harder = false;

    [Header("Pattern 1")]
    [SerializeField] float timeToRadius, radius;
    [SerializeField] float bigBulletSpeed1, bigBulletSpread, bigBulletShotRate, dmg1Big = 300, dmg1ellipse = 200;
    [SerializeField] int bigBulletNumber;
    [SerializeField] float initialSpeed1, radius1, finalSpeed1, smallBulletdelay1, angularVel, shotRate1, pulseRate1;
    [SerializeField] int numberOfRings1, numberPerRings1;

    [Header("Pattern 2")]
    [SerializeField] float lineBulletSpeed2, circleBulletSpeed, lineshotRate2, spacing, delay, lineduration, pulseRate2, dmg2line = 150;
    [SerializeField] float CircleBullets2shotRate, spread2, circlebulletpulseRate2, bossmovespeed, pulseDuration2, dmg2circle = 200;
    [SerializeField] int numberOfCircleBullets2;
    [SerializeField] float hardAngularVel2;
    [Header("Pattern 3")]
    [SerializeField] float radius3, pulseRate3, angleperPulse3, smallspeed3;
    [SerializeField] int numberOfsmallBullet3, numberOfLargeBullet3;
    [SerializeField] float shotRateLarge3, spreadAngleLarge3, largespeed3, dmg3arrow = 200, dmg3circle = 200, dmg3Big= 300;

    [Header("Pattern 4")]
    [SerializeField] float spawnradius4, initialSpeed4, finalSpeed4, shotRate4, pulseRate4, dmg4pointed = 300, dmg4arrow = 200;
    [SerializeField] int spawnRatio4, numberPerPulse4;
    ActionTrigger<Movement> triggerEvent;
    [SerializeField] int harderlines4;
    [Header("Patttern5")]
    [SerializeField] Enemy waterfairy, slime;
    [SerializeField] float bossShootRate5, bossShootSpeed5, bossshootSpread5, dmg5Big = 300; 
    [SerializeField] int bossShootNumber5;

    [Header("FairyBehavior")]
    [SerializeField] float spawnRatefairy, shotRatefairy, speedbulletfairy, speedfairy, fairypulseRate, fairypulseDuration, fairyangularVel, fairyamp, fairyY,dmg5fairy = 100;
    [Header("SlimeBehavior")]
    [SerializeField] float spawnRateslime, shotRateslime, speedbulletslime, speedslime;
    [SerializeField] int numberofShotsSlime, numberofexplodingBullets;
    [SerializeField] float explodingBulletSpeedFast, explodingBulletSpeedSlow, dmg5arrow, dmg5star;
    
    public override void SpawnWave() {
        ellipseBullet = GameManager.gameData.ellipseBullet.GetItem(DamageType.Water);
        pointedBullet = GameManager.gameData.pointedBullet.GetItem(DamageType.Water);
        smallBullet = GameManager.gameData.smallRoundBullet.GetItem(DamageType.Water);
        arrowBullet = GameManager.gameData.arrowBullet.GetItem(DamageType.Water);
        bigBullets = GameManager.gameData.bigBullet;
        magicCircle = GameManager.gameData.waterCircle;
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
        AudioManager.current.PlaySFX(GameManager.gameData.splashSFX);
       Destroy( Instantiate(spawnEffect, new Vector2(0, spawnY), Quaternion.identity).gameObject, 5f);
        bossImage = Instantiate(image, new Vector2(0, spawnY), Quaternion.identity);
        
        yield return new WaitForSeconds(0.5f);
        GameManager.PlayEndBossMusic();
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
        Invoke("Phase2", endPhaseTransition);
    }

    void Phase2() {
        SpellCardUI(namesOfSpellCards[0]);
        Invoke("Pattern2", spellCardTransition);
    }

    void Pattern2() {
        SwitchToBoss();
       
            float y = 4f;
            int i = 0;
            while (y > -4.1f)
            {
                float y1 = y;
                int i1 = i;

                currentBoss.shooting.StartShootingAfter(EnemyPatterns.RepeatSubPatternWithInterval(() => SummonLine(pointedBullet, dmg2line, i1 % 2 == 0, y1, lineduration)
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
                currentBoss.shooting.StartShootingAfter(EnemyPatterns.RepeatSubPatternWithInterval(() => SummonLine(pointedBullet, dmg2line, i1 % 2 == 0, y1, lineduration)
                  , currentBoss.shooting, pulseRate2), pulseRate2 / 2 + delay * i1);
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
        Invoke("Phase3", endPhaseTransition);
    }

    void Phase3() {
        SwitchToBoss();
        float time = currentBoss.movement.MoveTo(new Vector2(0, 2), bossmovespeed);


        currentBoss.shooting.StartShootingAfter(Functions.RepeatCustomAction(i => Patterns.RingOfBullets(smallBullet, dmg3circle, (Vector2)currentBoss.transform.position + new Polar(radius3, i * angleperPulse3).rect
            , numberOfsmallBullet3, UnityEngine.Random.Range(0, 360), smallspeed3, GameManager.gameData.waterpulseSFX), pulseRate3), time);
        currentBoss.shooting.StartShootingAfter(Functions.RepeatCustomAction(i => Patterns.RingOfBullets(arrowBullet, dmg3arrow, (Vector2)currentBoss.transform.position + Functions.RandomLocation(-1,1,-1,1)
            , numberOfsmallBullet3, UnityEngine.Random.Range(0, 360), smallspeed3,null), pulseRate3), time + UnityEngine.Random.Range(0, pulseRate3));
        currentBoss.shooting.StartShootingAfter(Functions.RepeatCustomAction(i => Patterns.ShootMultipleStraightBullet(bigBullets.GetItem(i % 4), dmg3Big,
            currentBoss.transform.position, largespeed3, Functions.AimAtPlayer(currentBoss.transform), spreadAngleLarge3, numberOfLargeBullet3,GameManager.gameData.gunSFX), shotRateLarge3), time);
        currentBoss.bosshealth.OnLifeDepleted += EndPhase3;
    }

    void EndPhase3() {
        EndPhase();
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase3;
        Invoke("Phase4", endPhaseTransition);
    }

    void Phase4() {
        SpellCardUI(namesOfSpellCards[1]);
        Invoke("Pattern4", spellCardTransition);
    }

    void Pattern4() {
        SwitchToBoss();
        float time = currentBoss.movement.MoveTo(new Vector2(0, 1), bossmovespeed);
        triggerEvent = new ActionTrigger<Movement>(movement => !Functions.WithinBounds(movement.transform.position, 4f));
        triggerEvent.OnTriggerEvent += movement =>
        {
            SmallBullets(movement.transform.position, arrowBullet, dmg4arrow, spawnRatio4);
            movement.RemoveObject();
        };
        currentBoss.shooting.StartShootingAfter(EnemyPatterns.RepeatSubPatternWithInterval(()=>pulseOfRain(0), currentBoss.shooting, pulseRate4),time);
        if (harder) {
            for (int i = 1; i < harderlines4; i++) {
                int j = i;
                currentBoss.shooting.StartShootingAfter(EnemyPatterns.RepeatSubPatternWithInterval(() => pulseOfRain(j*360f/harderlines4), currentBoss.shooting, pulseRate4), time);
            }
        }
        currentBoss.bosshealth.OnLifeDepleted += EndPhase4;
    }

    void EndPhase4() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase4;
        EndPhase();

        Invoke("Phase5", endPhaseTransition);
    }

    void Phase5() {
        SpellCardUI(namesOfSpellCards[2]);
        Invoke("Pattern5", spellCardTransition);

    }

    void Pattern5() {
        SwitchToBoss();
        float time = currentBoss.movement.MoveTo(new Vector2(0,2), bossmovespeed);
        currentBoss.shooting.StartShootingAfter(Functions.RepeatCustomAction(i => Patterns.ShootMultipleStraightBullet(bigBullets.GetItem(i % 4), dmg5Big,
           currentBoss.transform.position, bossShootSpeed5, Functions.AimAtPlayer(currentBoss.transform), bossshootSpread5, bossShootNumber5,GameManager.gameData.gunSFX), bossShootRate5), time);
        currentBoss.shooting.StartShootingAfter(SummonWaterFairy(), time);
        currentBoss.shooting.StartShootingAfter(SummonSlime(), time);
        currentBoss.health.OnDeath += EndPhase5;
    }

    void EndPhase5() {
        EndPhase();
        Destroy(bossImage);
        Invoke("Collect", 0.1f);
        Invoke("EndDialogue", endPhaseTransition);
    }

    void EndDialogue() {
        StartCoroutine(DialogueManager.StartDialogue(endDialogue, NextStage));
    }


    IEnumerator Pattern1() {
        Bullet magicCircle1 = EnemyPatterns.SummonMagicCircle(magicCircle, 0,currentBoss.transform, timeToRadius, 210, radius, 0,GameManager.gameData.magicCircleSummonSFX);
        Bullet magicCircle2 = EnemyPatterns.SummonMagicCircle(magicCircle, 0,currentBoss.transform, timeToRadius, 330, radius, 0,null);
        Bullet magicCircleTop = EnemyPatterns.SummonMagicCircle(magicCircle,0, currentBoss.transform, timeToRadius, 90, radius, 0,null);
        magicCircle1.orientation.StartRotating(180);
        magicCircle2.orientation.StartRotating(-180);
        magicCircleTop.orientation.StartRotating(97);
        yield return new WaitForSeconds(timeToRadius);
        Shooting top = magicCircleTop.GetComponent<Shooting>();
        top.StartShooting(EnemyPatterns.ShootAtPlayerWithLines(bigBullets.GetItem(DamageType.Fire), dmg1Big,
            magicCircleTop.transform, bigBulletSpeed1, bigBulletShotRate, bigBulletSpread, bigBulletNumber,GameManager.gameData.gunSFX));
        Bullet bul = ellipseBullet;
        SubPattern1(magicCircle1, true, bul, dmg1ellipse);
        SubPattern1(magicCircle2, false, bul, dmg1ellipse);
    }


    void SubPattern1(Bullet magicCircle, bool left, Bullet bul , float dmg){
        Shooting shooting = magicCircle.GetComponent<Shooting>();
        float angular = left ? angularVel : -angularVel;
        Functions.StartMultipleCustomCoroutines(shooting, i => Functions.RepeatAction(
            () => EnemyPatterns.OutAndSpinRingOfBullets(bul, dmg, magicCircle.transform, initialSpeed1, radius1, finalSpeed1, angular, 
            smallBulletdelay1, 0, numberPerRings1,GameManager.gameData.magicPulse1SFX), pulseRate1), numberOfRings1, shotRate1);
    }



    IEnumerator SummonLine(Bullet bul, float dmg, bool left, float y, float duration) {
        return Functions.RepeatActionXTimes(() => Patterns.ShootStraight(bul, dmg, new Vector2(left ? -4.1f : 4.1f, y), left ?  0 : 180,lineBulletSpeed2,null), lineshotRate2,  (int)(duration / lineshotRate2));
    }


    IEnumerator MovingAndShooting() {
        while (currentBoss) {
            float time = currentBoss.movement.MoveTo(Functions.RandomLocation(1, 3, 1, 3), bossmovespeed);
            if (!harder)
            { currentBoss.shooting.StartShootingFor(EnemyPatterns.ShootAtPlayerWithLines(smallBullet, dmg2circle, currentBoss.transform, circleBulletSpeed, CircleBullets2shotRate, spread2, numberOfCircleBullets2, GameManager.gameData.waterpulseSFX), time, pulseDuration2); }
            else {
                currentBoss.shooting.StartShootingAfter(Functions.RepeatCustomActionXTimes(i =>
                Patterns.SpirallingOutwardsRing(smallBullet, dmg2circle, currentBoss.transform.position, circleBulletSpeed,
                (i%2==0?-1:1)*hardAngularVel2 ,numberOfCircleBullets2,0,GameManager.gameData.waterpulseSFX), CircleBullets2shotRate, (int)(pulseDuration2/CircleBullets2shotRate) +1), time);
            }
            
            yield return new WaitForSeconds(circlebulletpulseRate2);
        }
    }

    void SmallBullets(Vector2 position, Bullet smallBullet,float dmg, int number) {
        float angle = Functions.AimAt(new Vector2(0, 0), position);
        float shootangle = (Mathf.RoundToInt(angle / 90 + 2) % 4) * 90;
        for (int i = 0; i < number; i++)
        {
            Patterns.ShootStraight(smallBullet, dmg, position, shootangle + UnityEngine.Random.Range(-90f, 90f), finalSpeed4,null);
        }
        

    }
    Bullet ShootRain(Bullet bigBullet, float dmgBig, Vector2 playerPosition, float angle) {
        Vector2 spawn = Functions.RandomLocation(currentBoss.transform.position, spawnradius4);

        Bullet bul = Patterns.ShootStraight(bigBullet, dmgBig, spawn, Functions.AimAt(spawn, playerPosition) + angle, initialSpeed4, GameManager.gameData.waterstreaming1SFX);
        bul.movement.triggers.Add(triggerEvent);
        return bul;
    }



    IEnumerator pulseOfRain(float angle) {
        Vector2 pos = GameManager.playerPosition;
        return Functions.RepeatActionXTimes(() => ShootRain(pointedBullet, dmg4pointed, pos, angle),shotRate4, numberPerPulse4) ;
    }
    IEnumerator SummonWaterFairy() {
   
        while (true)
        {
            float x = UnityEngine.Random.Range(-3.5f, 3.5f);
            Enemy fairy = Instantiate(waterfairy, new Vector2(x, 4.1f), Quaternion.identity);
            float time = fairy.movement.MoveTo(new Vector2(x, fairyY), speedfairy);
            fairy.shooting.StartShootingAfter(
                Functions.RepeatAction(() =>
                {
                    float angle = Functions.AimAtPlayer(fairy.transform);
                    fairy.shooting.StartShootingFor(EnemyPatterns.ShootSine(pointedBullet, dmg5fairy, fairy.transform, angle,
                    speedbulletfairy, shotRatefairy, fairyangularVel, fairyamp,null), 0, fairypulseDuration);
                    AudioManager.current.PlaySFX(GameManager.gameData.waterswooshSFX);
                }, fairypulseRate), time);
            Destroy(fairy.gameObject, 15f);
            yield return new WaitForSeconds(spawnRatefairy);
        }
        
    }
    IEnumerator SummonSlime() {
        triggerEvent = new ActionTrigger<Movement>(movement => !Functions.WithinBounds(movement.transform.position, 4f));
        triggerEvent.OnTriggerEvent += movement =>
        {
            SlimeOut(movement.transform.position);
            movement.RemoveObject();
        };
        while (true)
        {
            Enemy en = Instantiate(slime, currentBoss.transform.position, Quaternion.identity);
            float angle = Functions.AimAtPlayer(en.transform);
            en.movement.SetSpeed(Quaternion.Euler(0, 0, angle) * new Vector2(speedslime, 0));
            en.shooting.StartShootingAfter(EnemyPatterns.PulsingBulletsRandomAngle(GameManager.gameData.starBullet.GetItem(DamageType.Water), dmg5star,  
                en.transform, speedbulletslime, shotRateslime, numberofShotsSlime, GameManager.gameData.magicPulse1SFX), 0.5f);
            en.movement.triggers.Add(triggerEvent);
            yield return new WaitForSeconds(spawnRateslime);
        }
    }

    void SlimeOut(Vector2 pos) {
        float angle = UnityEngine.Random.Range(0f, 360f);
        Patterns.RingOfBullets(arrowBullet, dmg5arrow, pos, numberofexplodingBullets, angle, explodingBulletSpeedSlow, GameManager.gameData.waterpulseSFX);
        Patterns.RingOfBullets(arrowBullet, dmg5arrow, pos, numberofexplodingBullets, angle, explodingBulletSpeedFast,null);
    }


    
}
