using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6MidBoss : EnemyBossWave
{
    [SerializeField] ParticleSystem spawnEffect;
    [SerializeField] GameObject image;
    [SerializeField] Vector2 spawnLocation;
    [SerializeField] bool harder = false;

    [Header("Pattern1")]
    [SerializeField] float damage1;
    [SerializeField] float iniBulletSpeed1 = 0.5f;
    [SerializeField] float shotRate1 = 0.05f, angularVel1 = 134f, angularVel2 = 134f, delay = 1f, delayRandom = 0.5f, accelerationTime = 5f, acceleration = 1f, accelerationRandom = 0.5f;
    [SerializeField] int lines1 = 1;

    [Header("Pattern1 Harder")]
    [SerializeField] float bubblePulseRate = 1.5f; 
    [SerializeField] float radialVelocity = 20f, angularVel3 = 20f; 
    [SerializeField] int numberOfBulletsPerRing1 = 10;


    [Header("Pattern2")]
    [SerializeField] float ringDamage = 300f; 
    [SerializeField] float bubbleDamage = 400f;
    [SerializeField] float ringSpeed = 2f, bubbleSpeedMin = 1f, bubbleSpeedMax = 3f, bubbleRandomFactor = 15f;
    [SerializeField] float ringPulseRate = 2f, bubbleShotRate = 0.1f;
    [SerializeField] float moveSpeed = 1.5f;
    [SerializeField] int numberOfBulletsPerRing2 = 20, numberOfLasersPairs = 5;
    private bool homing = false;


    public override void SpawnWave() {
        
        StartCoroutine(PreFight());
    
    }
    IEnumerator PreFight() {
        // Destroy(Instantiate(spawnEffect, spawnLocation - new Vector2(0, 0.5f), Quaternion.Euler(-90,0,0)).gameObject, 5f);
        yield return new WaitForSeconds(0.5f);
        bossImage = Instantiate(image, spawnLocation + new Vector2(0f, 2.5f), Quaternion.identity);
        float wait = bossImage.GetComponent<Movement>().MoveTo(spawnLocation, 1f);
        yield return new WaitForSeconds(wait);
        StartPhase1();
    }

    void StartPhase1() {
        SpellCardUI(namesOfSpellCards[0]);
        Invoke("Phase1", spellCardTransition);
    }
    void Phase1() {
        currentBoss = Instantiate(boss, spawnLocation, Quaternion.identity);
        currentBoss.GetComponent<BasicDroppable>().otherDrops.Add(GameManager.gameData.lifeDropFull);
        GameManager.currentBoss = currentBoss;
        SwitchToBoss();
        currentBoss.shooting.StartShooting(Pattern1());
        currentBoss.bosshealth.OnLifeDepleted += EndPhase1;
    }

    void EndPhase1() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase1;
        EndPhase();
        Invoke("StartPhase2", endPhaseTransition);
    }
    void StartPhase2() {
        SpellCardUI(namesOfSpellCards[1]);
        Invoke("Phase2", spellCardTransition);
    }
    void Phase2() {
        SwitchToBoss();
        currentBoss.transform.rotation = Quaternion.identity;
        currentBoss.shooting.StartCoroutine(MoveTowardsPlayer());
        Pattern2();
        currentBoss.bosshealth.OnLifeDepleted  += EndPhase2;
    }

    void EndPhase2() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase2;
        EndPhase();
        Destroy(bossImage.gameObject);
        Invoke("Endboss", 3f);
    }

    void Endboss()
    {
        GameManager.SummonEndBoss();
    }

    IEnumerator Pattern1()
    {
        currentBoss.shooting.StartShooting(
            EnemyPatterns.CustomSpinningCustomBulletsCustomSpacing(
                angle => AcceleratingBullet(angle), 
                i => 360/lines1 * i, 
                t => 0 + angularVel1*t, 
                lines1, 
                shotRate1)
            );
        currentBoss.shooting.StartShooting(
            EnemyPatterns.CustomSpinningCustomBulletsCustomSpacing(
                angle => AcceleratingBullet(angle),
                i => 360/lines1 * i, 
                t => 0 - angularVel2*t, 
                lines1, 
                shotRate1)
            );
        yield return new WaitForSeconds(bubblePulseRate/2);

        if (harder) {
            currentBoss.shooting.StartShooting(Functions.RepeatAction(()=> Patterns.SpirallingOutwardsRing(GameManager.gameData.smallRoundBullet.GetItem(DamageType.Water), damage1, currentBoss.transform.position, radialVelocity, angularVel3, numberOfBulletsPerRing1, Random.Range(0f, 360f), null), bubblePulseRate));
            yield return new WaitForSeconds(bubblePulseRate/2);
            currentBoss.shooting.StartShooting(Functions.RepeatAction(()=> Patterns.SpirallingOutwardsRing(GameManager.gameData.smallRoundBullet.GetItem(DamageType.Water), damage1, currentBoss.transform.position, radialVelocity, -angularVel3, numberOfBulletsPerRing1, Random.Range(0f, 360f), null), bubblePulseRate));
        }
    }

    void Pattern2()
    {  
        currentBoss.shooting.StartShooting(Functions.RepeatAction(
            () => Patterns.CustomRing(
                angle => ReflectingBullet(GameManager.gameData.featherBullet, 
                ringDamage, currentBoss.transform.position, angle, ringSpeed), 
                Random.Range(0f, 360f), 
                numberOfBulletsPerRing2),
            ringPulseRate)
        );

        currentBoss.shooting.StartShooting(Functions.RepeatAction(
            () => { Bullet bul =
                Patterns.ShootStraight(
                GameManager.gameData.bigBullet.GetItem(DamageType.Water), 
                bubbleDamage, currentBoss.transform.position, 
                Functions.AimAtPlayer(currentBoss.transform) + Random.Range(-bubbleRandomFactor, 
                bubbleRandomFactor), 
            Random.Range(bubbleSpeedMin, bubbleSpeedMax), 
            null);
            bul.transform.localScale *= 0.2f;}, 
        bubbleShotRate));
    }


    IEnumerator MoveTowardsPlayer() 
    {
        while (true) {
            if (homing && GameManager.player && GameManager.player.gameObject.activeInHierarchy) {}
            else if (homing && !(GameManager.player && GameManager.player.gameObject.activeInHierarchy)) 
            {
                currentBoss.movement.StopMoving();
                homing = false;
            }
            else if (!homing && GameManager.player && GameManager.player.gameObject.activeInHierarchy)
            {
                currentBoss.movement.StartMoving();
                currentBoss.movement.Homing(GameManager.player.gameObject, moveSpeed);
                homing = true;
            }
            yield return null;
        }
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
        Bullet bullet = Patterns.ShootStraight(bul, dmg, origin, initialAngle, initialSpeed,null);
        bullet.movement.triggers.Add(reflectOnBound);
        return bullet;
    }

    Bullet AcceleratingBullet(float angle) 
    {
        float randomizedDelay = delay + Random.Range(-delayRandom, delayRandom);
        float randomizedAcceleration = acceleration + Random.Range(-accelerationRandom, accelerationRandom);
        return Patterns.ShootCustomBullet(GameManager.gameData.featherBullet, 
                                    damage1, 
                                    currentBoss.transform.position, 
                                    time => new Polar(time < randomizedDelay ? iniBulletSpeed1
                                                        : time < accelerationTime 
                                                        ? (time - randomizedDelay) * randomizedAcceleration + iniBulletSpeed1
                                                        : (accelerationTime) * randomizedAcceleration + iniBulletSpeed1, angle).rect,
                                    MovementMode.Velocity, null);
    }

    void End()
    {
        EndPhase();
        Destroy(bossImage);
        OnDefeat?.Invoke();
        DestroyAfter(5);
    }
}
