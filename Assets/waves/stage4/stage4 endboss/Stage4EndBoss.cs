using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Stage4EndBoss : EnemyBossWave
{
    // Start is called before the first frame update
    [SerializeField] GameObject key;
    [SerializeField] Dialogue preFight1;
    [SerializeField] BulletOrientation fireCircle;
    [SerializeField] ParticleSystem spawnEffect;
    [SerializeField] float delaybeforeSpinning, delaybeforeparticle, spinAcceleration, delayBeforeDialogue = 2f;
    [SerializeField] GameObject image;
    [SerializeField] Vector2 spawnLocation;
    [Header("Pattern1")]
    [SerializeField] float period1 = 0.5f, shootSpeed1 = 3f, spread1 = 50f, timeperPulse1 = 5f, shotRate1 = 0.05f;
    [SerializeField] float delaystart1 = 0.5f, angularvel = 10f, totaltimemove1 = 3f;
    [SerializeField] float delaytoNext = 2f, bounds = 0.8f, speed1 = 7f;
    [SerializeField] float fireBalldmg1 = 300f, fireBeamdmg1 = 3000f;
    [Header("Pattern2")]
    [SerializeField] GameObject explosionCircle;
    [SerializeField] float delay2 = 1.5f, slowSpawnRate2 = 0.1f, fastSpawnRate2= 0.05f;
    [SerializeField] int numberofbulletsperexplosion2min = 10, numberofbulletsperexplosion2max = 30;
    [SerializeField] float maxSpeed2 =5, minSpeed2 = 1, dmg2star = 300f;
    [SerializeField] float maxangularvel = 500f;
    [Header("Pattern3")]
    [SerializeField] float fireballdmg3 = 350f, fireballspeed3, fireballshotrate, fireballpulserate, fireballpulsetime, fireballspread3 = 20f;
    [SerializeField] float laserDmg3 = 600f;
    [SerializeField] int fireballNumber3, numberOfCircles3, numberoflasers3;
    [SerializeField] float y3, x3, delayBetweenEach3=0.05f, lasershotRate3= 0.5f, spreadlaser3 = 10, laserspeed = 2f;
    [SerializeField] Vector2 bossLocation3;
    [Header("Pattern4")]
    [SerializeField] float dmg4;
    [SerializeField] float speed4circle, radialVel4;
    [SerializeField] float delayBeforeMovingOff4 = 5f, delayPerBullet4 = 0.03f, shotRate4 = 0.1f, acc4 = 3f, timeAccelerating4 = 1f, timeBetweenCircles4 = 10f;
    [SerializeField] int numberOfCircles4= 8;
    [Header("Pattern5")]
    [SerializeField] float arcAngle5= 320f, arcshotRate5 = 1f, arcspeed5 = 3f, spacingAngle5 = 1f,firearcdmg5= 500f;
    [SerializeField] float speedBullet5Min = 2f, speedBullet5Max = 4f, shotRateMax = 0.1f, shotRateMin = 0.05f, stardmg5 = 300f, maxSpin5 = 500f;
    [SerializeField] int bulletCountMultiplier = 2;
    protected override void SwitchToBoss()
    {
        bossImage.GetComponent<ParticleSystem>().Stop();
        base.SwitchToBoss();
    }

    protected override void SwitchToImage()
    {
        bossImage.GetComponent<ParticleSystem>().Play();
        base.SwitchToImage();
    }
    public override void SpawnWave()
    {
        StartCoroutine(PreFight1());

    }
    IEnumerator PreFight1() {
        BulletOrientation circle = Instantiate(fireCircle, spawnLocation, Quaternion.identity);
        yield return new WaitForSeconds(delaybeforeSpinning);
        circle.SetCustomAngularVel(t => spinAcceleration * t);
        yield return new WaitForSeconds(delaybeforeparticle);
        Destroy(circle.gameObject);
        Destroy(Instantiate(spawnEffect, spawnLocation, Quaternion.Euler(-90, 0, 0)).gameObject, 5f);
        bossImage = Instantiate(image, spawnLocation, Quaternion.identity);
        yield return new WaitForSeconds(delayBeforeDialogue);
        StartCoroutine(DialogueManager.StartDialogue(preFight1, Phase1));

    }
    void Phase1() {
        currentBoss = Instantiate(boss, spawnLocation, Quaternion.identity);
        SwitchToBoss();
        currentBoss.shooting.StartShooting(Functions.RepeatCustomAction(i => {
            float time = currentBoss.movement.MoveTo(Functions.RandomLocation(spawnLocation-new Vector2(0,bounds/2), bounds), speed1);
            currentBoss.shooting.StartShootingAfter(PulsePhase1(

             Functions.AimAt(currentBoss.transform.position, GameManager.playerPosition)), time);
            
            }, delaytoNext + timeperPulse1));
        currentBoss.bosshealth.OnLifeDepleted += EndPhase1;
    }
    void EndPhase1() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase1;
        EndPhase();
        Invoke("StartPhase2", endPhaseTransition);
    }
    void StartPhase2() {
        SpellCardUI(namesOfSpellCards[0]);
        Invoke("Phase2", spellCardTransition);
    }
    void Phase2() {
        SwitchToBoss();
        float maxHP = currentBoss.bosshealth.maxHP;
        currentBoss.shooting.StartShooting(Functions.RepeatCustomActionCustomTime(
            i=> currentBoss.shooting.StartShooting(SummonExplosion(Functions.RandomLocation(-3.5f,3.5f,-3.5f,3.5f)
            , (int)((numberofbulletsperexplosion2max-numberofbulletsperexplosion2min)*(1-currentBoss.bosshealth.GetCurrentHP()/maxHP))+ numberofbulletsperexplosion2min))
             ,i=> (slowSpawnRate2- fastSpawnRate2) * currentBoss.bosshealth.GetCurrentHP() / maxHP+ fastSpawnRate2));
        currentBoss.bosshealth.OnLifeDepleted += EndPhase2;
    }

    void EndPhase2()
    {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase2;
        EndPhase();
        Invoke("StartPhase3", endPhaseTransition);
    }
    void StartPhase3()
    {
        SwitchToBoss();
        float time = currentBoss.movement.MoveTo(bossLocation3, speed1);
        currentBoss.shooting.StartShootingAfter(Functions.RepeatAction(
            () => currentBoss.shooting.StartShooting(Phase3BossPulse(Functions.AimAt(currentBoss.transform.position, GameManager.playerPosition))),
            fireballpulserate), time);
        for (int i = 0; i < numberOfCircles3; i++)
        {
            Bullet circle = GameManager.bulletpools.SpawnBullet(GameManager.gameData.fireCircle, currentBoss.transform.position);
            circle.movement.MoveTo(new Vector2(-x3 + i * 2 * x3 / (numberOfCircles3 - 1), y3), speed1);
            circle.GetComponent<Shooting>().StartShootingAfter(EnemyPatterns.ShootAtPlayerWithLines(
                GameManager.gameData.fireShortLaser, laserDmg3, circle.transform, laserspeed, lasershotRate3, spreadlaser3, numberoflasers3)
                , delayBetweenEach3 * i + 1.5f);
        }
        currentBoss.bosshealth.OnLifeDepleted += EndPhase3;
    }

    void EndPhase3() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase3;
        EndPhase();
        Invoke("StartPhase4", endPhaseTransition);

    }

    void StartPhase4() {
        SpellCardUI(namesOfSpellCards[1]);
        Invoke("Phase4", spellCardTransition);
    }
    void Phase4() {
        SwitchToBoss();
        float time = currentBoss.movement.MoveTo(new Vector2(0, 0), speed1);

        currentBoss.shooting.StartShootingAfter(Functions.RepeatCustomAction(
            i =>
            {
                for (int j = 0; j < numberOfCircles4; j++)
                {
                    int z = i % 4;
                    ShootMagicCircleRadiallyOutward(j * 360f / numberOfCircles4, z < 2, z % 2 == 0);
                }
            }, timeBetweenCircles4)
            , time);
        currentBoss.bosshealth.OnLifeDepleted += EndPhase4;
    }
    void EndPhase4() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase4;
        EndPhase();
        Invoke("Phase5", endPhaseTransition);
    }
    void Phase5() {
        SwitchToBoss();
        Bullet fire = GameManager.gameData.fireBullet;
        Bullet star = GameManager.gameData.fireStarBullet;
        float time = currentBoss.movement.MoveTo(spawnLocation, speed1);
        currentBoss.shooting.StartShootingAfter(Functions.RepeatCustomAction(
            i =>
            {
                float originAngle = 90 + (i % 2 == 0 ? 1 : -1) * arcAngle5 / 2;
                Patterns.ShootMultipleStraightBullet(fire, firearcdmg5, currentBoss.transform.position, arcspeed5, originAngle,
                    spacingAngle5, (int)(arcAngle5 / (spacingAngle5 * 2)));

            }, arcshotRate5
            ), time);
        currentBoss.shooting.StartShootingAfter(Functions.RepeatCustomActionCustomTime(
            i=> {
                for (int j = 0; j < bulletCountMultiplier; j++) {
                    float speed = UnityEngine.Random.Range(speedBullet5Min, speedBullet5Max);
                    Bullet star2 = Patterns.ShootStraight(star, stardmg5, new Vector2(UnityEngine.Random.Range(-3.9f, 3.9f), 4.1f), -90,
                        speed);
                    star2.orientation.StartRotating(speed / speedBullet5Max * maxSpin5);
                }
            }, i=> UnityEngine.Random.Range(shotRateMin, shotRateMax)
            ),time);
        currentBoss.bosshealth.OnLifeDepleted += EndPhase5;
    }
    void EndPhase5() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase5;
        EndPhase();
        Invoke("StartPhase6", spellCardTransition);
    }
    void StartPhase6() {
        SwitchToBoss();
    }
    void ShootMagicCircleRadiallyOutward(float startAngle, bool outwards, bool left) {
        Bullet magicCircle = GameManager.bulletpools.SpawnBullet(GameManager.gameData.fireCircle, currentBoss.transform.position);
        magicCircle.movement.Reset();
        magicCircle.movement.destroyBoundary = 8f;
        magicCircle.movement.SetCustomGraph(t => new Polar((radialVel4*(t+ 0.01f)),Mathf.Rad2Deg*(left?1:-1)*speed4circle/radialVel4 * (Mathf.Log((t+0.01f)/0.01f)) + startAngle).rect, MovementMode.Position);
        magicCircle.GetComponent<Shooting>().StartShooting(Functions.RepeatCustomAction(
            i => Pattern4Bullet(magicCircle.orientation.angle + (outwards? 90: -90), magicCircle.transform.position, delayBeforeMovingOff4 +
            i * delayPerBullet4, acc4, timeAccelerating4), shotRate4));


    }
    Bullet  Pattern4Bullet(float angle, Vector2 origin, float delay, float acceleration, float totalAccelerationTime) {
        Bullet bul = Patterns.ShootCustomBullet(GameManager.gameData.fireStarBullet, dmg4, origin, 
            Movement.RotatePath(angle, t => new Vector2(t<delay? 0 : t< delay + totalAccelerationTime? acceleration:0,0)), MovementMode.Acceleration);
        bul.transform.localScale = 0.8f * bul.transform.localScale;
        return bul;

        
    }
    IEnumerator PulsePhase1(float angle) {

        Bullet fireBall = GameManager.gameData.fireBullet;
        Bullet fireBeam = GameManager.gameData.fireBeam;
        currentBoss.shooting.StartShootingFor(EnemyPatterns.CustomSpinningStraightBullets(
            fireBall, fireBalldmg1, currentBoss.transform, shootSpeed1, t => angle + spread1 * Mathf.Sin((float)(2 * Math.PI / period1 * t)),
            4, shotRate1
            ), 0, timeperPulse1);
         Bullet laserLeft = GameManager.bulletpools.SpawnBullet(fireBeam, currentBoss.transform.position, Quaternion.Euler(0, 0, angle - spread1));
         Bullet laserRight = GameManager.bulletpools.SpawnBullet(fireBeam, currentBoss.transform.position, Quaternion.Euler(0, 0, angle + spread1));
        laserLeft.SetDamage(fireBeamdmg1);
        laserRight.SetDamage(fireBeamdmg1);
        laserLeft.orientation.SetFixedOrientation(angle - spread1);
        laserRight.orientation.SetFixedOrientation(angle + spread1);
     
        yield return new WaitForSeconds(delaystart1);
        
        laserLeft.orientation.SetCustomAngularVel(t => t < totaltimemove1 ? angularvel: 0);
        laserRight.orientation.SetCustomAngularVel(t => t < totaltimemove1 ? -angularvel : 0);
        yield return new WaitForSeconds(timeperPulse1 - delaystart1);
        laserLeft.Deactivate();
        laserRight.Deactivate();
    }
    IEnumerator SummonExplosion(Vector2 location, int numberofBullets) {
        GameObject obj = Instantiate(explosionCircle, location, Quaternion.identity);
        Destroy(obj, delay2);
        yield return new WaitForSeconds(delay2);
        for (int i = 0; i < numberofBullets; i++) {
            float speed = UnityEngine.Random.Range(minSpeed2, maxSpeed2);
            Bullet bul = Patterns.ShootStraight(GameManager.gameData.fireStarBullet, dmg2star, location, UnityEngine.Random.Range(0f, 360f),
                speed);
            bul.orientation.StartRotating(maxangularvel * speed / maxSpeed2);


        }
        
    }

    IEnumerator Phase3BossPulse(float angle) {
        Bullet fireball = GameManager.gameData.smallRoundBullet.GetItem(DamageType.Fire);
        return Functions.RepeatActionXTimes(()=>Patterns.ShootMultipleStraightBullet(fireball, fireballdmg3, currentBoss.transform.position, fireballspeed3,
            angle, fireballspread3, fireballNumber3), fireballshotrate, (int)(fireballpulsetime / fireballshotrate));
    }
}
