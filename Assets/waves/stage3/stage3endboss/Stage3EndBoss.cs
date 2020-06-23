using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Stage3EndBoss : EnemyBossWave
{
    // Start is called before the first frame update
    [SerializeField] GameObject key;
    [SerializeField] GameObject image;
    [SerializeField] ParticleSystem slamEffect;
    [SerializeField] float initialMoveSpeed;
    [SerializeField] float spawnLocationY;
    [SerializeField] Dialogue preFightDialogue1, preFightDialogue2;
    [Header("Pattern1")]
    [SerializeField] float bulletspeed1;
    [SerializeField] float topBulletSpread, sideBulletSpread, sideOriginAngle, rockdmg1, leafdmg1, bossSpeed1, shotRate1, shotPulseDuration1, pause1, minipause1;

    [SerializeField] Vector2 topPosition1, rightPosition1, leftPosition1;
    Bullet rock, leaf1, leaf2, leaf3;
    [Header("Pattern2")]
    [SerializeField] float rockspeed2, rockSpawnRate2, x2, y2, movespeed2, delay2, leafshotRate2, leafSpread2, leafSped2, leafdmg2;
    [SerializeField] int leaflines2;
    [Header("Pattern3")]
    [SerializeField] float angularvel = 30, firstspeed = 1f, speeddiff = 0.1f, y3 = 1, leaf3dmg = 200, shotRate3 = 0.1f, leaf3scale = 0.5f;

    [SerializeField] int leafnumbers3, leaflines3;
    [Header("Pattern4")]
    [SerializeField] EnemyStats mushroomStats4;
    [SerializeField] float spawnRate4 = 0.05f, mushroomspeed4 = 3f, mushroomspinning4 = 360f, pulseRate4 = 5f, delayinitial4 = 0.5f, mushroomspawnangularvel= 30f;
    [SerializeField] float shootDuration4 = 1.5f, totalMoveDuration4 = 1.8f;
    [SerializeField] int ratio4first = 10, ratio4next = 5, mushroomlines = 3;
    [SerializeField] float speedbig4 = 3f, delaynext = 0.5f, movetime4 = 0.5f, speedsmall4 = 2f;
    [SerializeField] float bigDmg = 500f, smallDmg = 100f;
    [Header("Pattern5")]
    [SerializeField] Vector2 midPoint5;
    [SerializeField] Vector2 topRight, topLeft;
    [SerializeField] int numberOfPillars = 3, numberofLeafPerLines5 = 3, numberOfLeafLine5 = 10;
    [SerializeField] float pillarSpeed1 = 2f, pillarSpeed2 = 6f, pillarSpeed3= 3f, bossmovespeed5 = 8f, leafspeed5 = 4f, leafspeeddiff5 = 0.1f;
    [SerializeField] float delaypillar1 = 1f, delaypillar2 = 0.5f, pillarRate = 6f, leafspreadAngle5 = 15f;
    [Header("Pattern6")]
    [SerializeField] Vector2 bossPosition6;
    [SerializeField] Vector2 leftMushroomPosition6, rightMushroomPosition6;
    [SerializeField] float mushroomSpeed6 = 3f;
    [Header("Pattern6 Left Mushroom")]
    [SerializeField] float shootSpeed6l = 4f;
    [SerializeField] float angleSpread6l = 50f, deacceleration6l = 4f, finalSpeed6l = 2.5f, shotRate6l = 0.1f, pulseDuration6l = 2f, pulsePause6l = 1f;
    [SerializeField] EnemyStats stats6;
    [Header("Pattern6 Right Mushroom")]
    [SerializeField] float shootSpeed6r = 3f;
    [SerializeField] float pulserate6r = 1, shotrate6r = 0.05f, greendmg = 200f;
    [SerializeField] int numberofLines6r = 30, numberofBulletsPerLine6r = 8;
    [Header("Pattern6 Boss")]
    [SerializeField] float shootrate6 = 0.05f, shotSpeed6 = 2f, spreadAngle6 =45f;
    [SerializeField] Dialogue endDialogue;

    public override void SpawnWave()
    {
        rock = GameManager.gameData.rockBullet;
        leaf1 = GameManager.gameData.leafBullet1;
        leaf2 = GameManager.gameData.leafBullet2;
        leaf3 = GameManager.gameData.leafBullet3;
        StartCoroutine(PreFight1());
    }
    IEnumerator PreFight1()
    {
        try
        {
            GameObject.Find("stage3quad").GetComponent<Animator>().SetTrigger("StopMoving");
        }
        catch (Exception ex) { Debug.Log(ex.ToString()); }
        yield return new WaitForSeconds(1f);
        Instantiate(key, new Vector2(0, 3.5f), Quaternion.identity);
        StartCoroutine(DialogueManager.StartDialogue(preFightDialogue1, () => StartCoroutine(PreFight2())));


    }
    IEnumerator PreFight2()
    {
        yield return new WaitForSeconds(1f);
        bossImage = Instantiate(image, new Vector2(-4.1f, 4.1f), Quaternion.identity);
        float time = bossImage.GetComponent<Movement>().MoveTo(new Vector2(0, spawnLocationY), initialMoveSpeed);
        yield return new WaitForSeconds(time);
        Instantiate(slamEffect, new Vector2(0, spawnLocationY), Quaternion.identity);
        StartCoroutine(DialogueManager.StartDialogue(preFightDialogue2, Phase1));

    }

    void Phase1()
    {
        currentBoss = Instantiate(boss, new Vector2(0, spawnLocationY), Quaternion.identity);
        GameManager.currentBoss = currentBoss;
        currentBoss.shooting.StartShooting(Pattern1());
        
        currentBoss.bosshealth.OnLifeDepleted += EndPhase1;
        SwitchToBoss();
    }

    void EndPhase1()
    {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase1;
        EndPhase();
        Invoke("Phase2", endPhaseTransition);
    }

    IEnumerator Pattern1()
    {
        while (true)
        {
            float time1 = currentBoss.movement.MoveTo(leftPosition1, bossSpeed1);
            yield return new WaitForSeconds(time1);
            currentBoss.shooting.StartShootingFor(BarrageOfReflectingBullets(leaf1, leafdmg1,
                180 + sideOriginAngle - sideBulletSpread, 180 + sideOriginAngle + sideBulletSpread, bulletspeed1, shotRate1), 0, shotPulseDuration1);

            yield return new WaitForSeconds(shotPulseDuration1 + minipause1);
            float time2 = currentBoss.movement.MoveTo(rightPosition1, bossSpeed1);
            yield return new WaitForSeconds(time2);
            currentBoss.shooting.StartShootingFor(BarrageOfReflectingBullets(leaf1, leafdmg1,
                -sideOriginAngle - sideBulletSpread, -sideOriginAngle + sideBulletSpread, bulletspeed1, shotRate1), 0, shotPulseDuration1);
            yield return new WaitForSeconds(shotPulseDuration1 + minipause1);
            float time3 = currentBoss.movement.MoveTo(topPosition1, bossSpeed1);
            yield return new WaitForSeconds(time3);
            currentBoss.shooting.StartShootingFor(BarrageOfReflectingBullets(rock, rockdmg1,
                90 - sideBulletSpread, 90 + sideBulletSpread, bulletspeed1, shotRate1), 0, shotPulseDuration1);
            yield return new WaitForSeconds(shotPulseDuration1 + pause1);
        }

    }

    void Phase2()
    {
        SpellCardUI(namesOfSpellCards[0]);
        Invoke("StartPattern2", spellCardTransition);

    }

    void StartPattern2()
    {
        SwitchToBoss();
        currentBoss.shooting.StartShooting(RockEmergingFromGround());
        currentBoss.shooting.StartShooting(MoveLeftAndRight2());
        currentBoss.bosshealth.OnLifeDepleted += EndPhase2;
    }
    void EndPhase2()
    {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase2;
        EndPhase();
        Invoke("Phase3", endPhaseTransition);

    }

    void Phase3()
    {
        SwitchToBoss();
        float time = currentBoss.movement.MoveTo(new Vector2(0, y3), initialMoveSpeed);
        currentBoss.shooting.StartCoroutine(Pattern3(time, angularvel, 0));
        currentBoss.bosshealth.OnLifeDepleted += EndPhase3;

    }
    void EndPhase3() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase3;
        EndPhase();
        Invoke("Phase4", endPhaseTransition);
    }

    void Phase4() {
        SpellCardUI(namesOfSpellCards[1]);
        Invoke("StartPattern4", spellCardTransition);
        
    }
    void StartPattern4()
    {
        SwitchToBoss();
        currentBoss.shooting.StartShooting(Pattern4());
        currentBoss.bosshealth.OnLifeDepleted += EndPhase4;
    }

    void EndPhase4()
    {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase4;
        EndPhase();
        Invoke("Phase5", endPhaseTransition);
    }
    void Phase5()
    {
        SpellCardUI(namesOfSpellCards[2]);
        Invoke("StartPattern5", spellCardTransition);

    }

    void StartPattern5() {
        SwitchToBoss();
        currentBoss.shooting.StartShooting(Pattern5());
        currentBoss.bosshealth.OnLifeDepleted += EndPhase5;

    }
    void EndPhase5() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase5;
        EndPhase();
        Invoke("Phase6", endPhaseTransition); 
    }
    void Phase6() {
        SpellCardUI(namesOfSpellCards[3]);
        Invoke("StartPattern6", spellCardTransition);
    }
    void StartPattern6() {
        SwitchToBoss();
        currentBoss.shooting.StartCoroutine(Pattern6());
        currentBoss.bosshealth.OnDeath += EndPhase6;
    }
    void EndPhase6()
    {
        
        EndPhase();
        Destroy(bossImage);
        StartCoroutine(DialogueManager.StartDialogue(endDialogue, NextStage));
    }
  
    Enemy ShootMushroom(float speed, float angle, DamageType type, float spinningvel) {
        Enemy mushroom = Instantiate(GameManager.gameData.mushrooms.GetItem(type), currentBoss.transform.position, Quaternion.identity);
        mushroom.movement.SetSpeed(speed, angle);
        mushroom.SetEnemy(mushroomStats4, false);
        mushroom.GetComponent<DamageDealer>().damageType = type;
        mushroom.GetComponent<BulletOrientation>().SetCustomOrientaion(t => Quaternion.Euler(0, 0, spinningvel * t));
        return mushroom;
    }

    Bullet explodingBullets(Bullet bigBul, Bullet smallBul, float bigdmg, float smalldmg, Vector2 pos, int ratio, float angle, float speedbig, float explodeoffset, float explodespeed, float movetime, float delay) {
        ActionTrigger<Movement> trigger = new ActionTrigger<Movement>(movement => movement.time > delay + movetime);
        trigger.OnTriggerEvent += movement =>
        {
            Patterns.RingOfBullets(smallBul, smalldmg, movement.transform.position, ratio, explodeoffset, explodespeed);
            movement.RemoveObject();
        };
        Bullet bul = Patterns.ShootCustomBullet(bigBul, bigdmg, pos, 
            t=> t < movetime ? (Vector2)(Quaternion.Euler(0,0,angle)*new Vector2(speedbig,0)): new Vector2(0,0), MovementMode.Velocity);
        bul.movement.triggers.Add(trigger);
        return bul;
    }

    IEnumerator Pattern4() {
    
        while (true) {
            List<Enemy> enemies = new List<Enemy>();
            currentBoss.shooting.StartShootingFor(Functions.RepeatCustomAction(
                i => {
                    
                    for (int y = 0; y < mushroomlines; y++)
                    {
                        Enemy em = ShootMushroom(mushroomspeed4, i * spawnRate4 * mushroomspawnangularvel + y*360f/mushroomlines, Functions.RandomType(false),
                       mushroomspinning4);

                        enemies.Add(em);
                    }
                }, spawnRate4), 0, shootDuration4);
           
        
            yield return new WaitForSeconds(totalMoveDuration4);
            foreach (Enemy en in enemies) {
                if (en)
                {
                    en.movement.StopMoving();
                    BulletOrientation ori = en.GetComponent<BulletOrientation>();
                    ori.SetFixedOrientation(ori.orientation);
                }
            }
            yield return new WaitForSeconds(delayinitial4);
            foreach (Enemy en in enemies) {
                if (en)
                {
                    DamageType type = en.GetComponent<DamageDealer>().damageType;
                    Bullet small = GameManager.gameData.arrowBullet.GetItem(type);
                    Bullet big = GameManager.gameData.smallRoundBullet.GetItem(type);
                    Patterns.CustomRing(angle => explodingBullets(big, small, bigDmg, smallDmg, en.transform.position, ratio4next, angle,
                        speedbig4, angle, speedsmall4, movetime4, delaynext), UnityEngine.Random.Range(0, 360f), ratio4first);
                    Destroy(en.gameObject);
                }
            }
            yield return new WaitForSeconds(pulseRate4 - delayinitial4 - totalMoveDuration4);
        }
    }

    IEnumerator Pattern5() {
        float time = currentBoss.movement.MoveTo(midPoint5, bossmovespeed5);
        yield return new WaitForSeconds(time);
        while (true) {
            Functions.StartMultipleCustomCoroutines(currentBoss.shooting, i => SummonPillar(1), numberOfPillars);
            yield return new WaitForSeconds(pillarRate);
            time = currentBoss.movement.MoveTo(topLeft, bossmovespeed5) ;
            Functions.StartMultipleCustomCoroutines(currentBoss.shooting, i => SummonPillar(2), numberOfPillars);
            yield return new WaitForSeconds(pillarRate);
            time = currentBoss.movement.MoveTo(topRight, bossmovespeed5);
            Functions.StartMultipleCustomCoroutines(currentBoss.shooting, i => SummonPillar(0), numberOfPillars);
            yield return new WaitForSeconds(pillarRate);
            time = currentBoss.movement.MoveTo(midPoint5, bossmovespeed5);

        }
    }
    IEnumerator SummonPillar(int pillarlocation) {
        float pos = UnityEngine.Random.Range(-3.5f, 3.5f);
        Quaternion orientation = Quaternion.Euler(0, 0, pillarlocation == 0 ? 0 : pillarlocation == 1 ? 90 : 180);
        Bullet pillar = GameManager.bulletpools.SpawnBullet(GameManager.gameData.mushroomPillar, pillarlocation == 0 ? new Vector2(-4.1f, pos) :
            pillarlocation == 1 ? new Vector2(pos, -4.1f) : new Vector2(4.1f, pos), orientation);
        pillar.orientation.SetFixedOrientation(orientation);
        pillar.movement.MoveAndStopAfter(pillarlocation == 0 ? new Vector2(pillarSpeed1, 0) : pillarlocation == 1 ?
            new Vector2(0, pillarSpeed1) : new Vector2(-pillarSpeed1, 0), 0.6f/pillarSpeed1);
        
        yield return new WaitForSeconds(0.6f / pillarSpeed1 + delaypillar1);
        float time2 = pillar.movement.MoveTo(pillarlocation == 0 ? new Vector2(4.1f, pos) : pillarlocation == 1 ?
            new Vector2(pos, 4.1f) : new Vector2(-4.1f, pos), pillarSpeed2);
        yield return new WaitForSeconds(time2);

        currentBoss.shooting.StartShooting(Functions.RepeatCustomActionXTimes(
            i => Patterns.ShootMultipleStraightBullet(leaf1, leafdmg1, pillar.transform.position, leafspeed5 + i * leafspeeddiff5,
            pillarlocation == 0 ? 180 : pillarlocation == 1 ? -90 : 0, leafspreadAngle5, numberOfLeafLine5), 0.02f, numberofLeafPerLines5));
                 
       
        yield return new WaitForSeconds(delaypillar2);
        pillar.movement.SetSpeed(pillarSpeed3, pillarlocation == 0 ? 180 : pillarlocation == 1 ? 270 : 0);

    }
    IEnumerator BarrageOfReflectingBullets(Bullet bul, float dmg, float angleMin, float angleMax, float speed, float shotRate)
    {
        return Functions.RepeatAction(() => ReflectingBullet(bul, dmg, currentBoss.transform.position,
            UnityEngine.Random.Range(angleMin, angleMax), speed), shotRate);
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

    IEnumerator RockEmergingFromGround()
    {
        return Functions.RepeatAction(() =>
            Patterns.ShootStraight(rock, rockdmg1, new Vector2(UnityEngine.Random.Range(-4f, 4f), -4), 90, rockspeed2), rockSpawnRate2);

    }

    IEnumerator MoveLeftAndRight2()
    {
        bool left = true;
        Vector2 leftPos = new Vector2(-x2, y2), rightPos = new Vector2(x2, y2);
        float time = currentBoss.movement.MoveTo(leftPos, initialMoveSpeed);
        yield return new WaitForSeconds(time);
        while (true)
        {
            float time1 = currentBoss.movement.MoveTo(left ? rightPos : leftPos, movespeed2);
            currentBoss.shooting.StartShootingFor(
                EnemyPatterns.ShootAtPlayerWithLines(leaf2, leafdmg2, currentBoss.transform, leafSped2, leafshotRate2, leafSpread2, leaflines2), 0, time1);
            yield return new WaitForSeconds(time1 + delay2);
            left = !left;
        }
    }

    IEnumerator Pattern3(float delay, float vel, float offset) {
        yield return new WaitForSeconds(delay);
        Functions.StartMultipleCustomCoroutines(currentBoss.shooting,
            i => EnemyPatterns.CustomSpinningCustomBulletsCustomSpacing(
                    angle => {
                        Bullet bul = ReflectingBullet(leaf3, leaf3dmg, currentBoss.transform.position, angle, firstspeed + speeddiff * i);
                        bul.transform.localScale *= leaf3scale;
                        return bul;
                        },
                    x => 360f * x / leaflines3, t => vel * t + offset, leaflines3, shotRate3), leafnumbers3);
           
        
    }
    IEnumerator Pattern6() {
        Bullet green = GameManager.gameData.ellipseBullet.GetItem(DamageType.Earth);
        float time = currentBoss.movement.MoveTo(bossPosition6, initialMoveSpeed);
        yield return new WaitForSeconds(time);
        Enemy mushroomLeft = Instantiate(GameManager.gameData.midBossMushroomMob, currentBoss.transform.position, Quaternion.identity);
        float time1 = mushroomLeft.movement.MoveTo(leftMushroomPosition6, mushroomSpeed6);
        mushroomLeft.SetEnemy(stats6, false);
        Enemy mushroomRight = Instantiate(GameManager.gameData.midBossMushroomMob, currentBoss.transform.position, Quaternion.identity);
        float time2 = mushroomRight.movement.MoveTo(rightMushroomPosition6, mushroomSpeed6);
        mushroomRight.SetEnemy(stats6, false);
        yield return new WaitForSeconds(Math.Max(time1, time2));
        mushroomLeft.shooting.StartShooting(Functions.RepeatAction(()=>
            Functions.StartMultipleCustomCoroutines(this,
            i => UpThenHomeBullet(leaf2, leafdmg2, Functions.RandomLocation(mushroomLeft.transform.position, 0.5f), shootSpeed6l,
            90 + UnityEngine.Random.Range(-angleSpread6l, angleSpread6l), deacceleration6l, finalSpeed6l), (int)(pulseDuration6l / shotRate6l), shotRate6l)
            , pulseDuration6l + pulsePause6l));
        mushroomRight.shooting.StartShooting(Functions.RepeatAction(
            () => mushroomRight.shooting.StartShooting(EnemyPatterns.PulsingLines(green, greendmg, mushroomRight.transform, shootSpeed6r, 0, shotrate6r, numberofLines6r, numberofBulletsPerLine6r)),
            pulserate6r));

        currentBoss.shooting.StartShooting(BarrageOfReflectingBullets(rock, rockdmg1,
                90 - spreadAngle6, 90 + spreadAngle6, shotSpeed6, shootrate6 ));

    }

    IEnumerator UpThenHomeBullet(Bullet bul, float dmg, Vector2 spawnPos, float shootSpeed, float angle, float deacceleration, float finalSpeed) {

        Bullet bullet = GameManager.bulletpools.SpawnBullet(bul, spawnPos);
        bullet.SetDamage(dmg);
        bullet.movement.SetAcceleration(Quaternion.Euler(0, 0, angle) * new Vector2(shootSpeed, 0), t => new Vector2(0, -deacceleration));
        bullet.movement.destroyBoundary = 6f;
        yield return new WaitForSeconds(shootSpeed * Mathf.Sin(Mathf.Deg2Rad * angle) / deacceleration);
        if (bullet && bullet.gameObject.activeInHierarchy) {
            float angle2 = Functions.AimAt(bullet.transform.position, GameManager.playerPosition);
            bullet.movement.SetSpeed(finalSpeed, angle2);
        }
    }  
}
