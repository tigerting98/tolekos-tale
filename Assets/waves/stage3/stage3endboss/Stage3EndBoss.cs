using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Stage3EndBoss : EnemyBossWave
{
    // Start is called before the first frame update
    [SerializeField] GameObject earthCircle, key;
    GameObject actualCircle;
    [SerializeField] GameObject image;
    [SerializeField] ParticleSystem slamEffect;
    [SerializeField] float initialMoveSpeed;
    [SerializeField] float spawnLocationY;
    [SerializeField] float spawnDelay;
    [SerializeField] Dialogue preFightDialogue1, preFightDialogue2;
    [Header("Pattern1")]
    [SerializeField] float bulletspeed1;
    [SerializeField]float topBulletSpread, sideBulletSpread, sideOriginAngle, rockdmg1, leafdmg1, bossSpeed1, shotRate1, shotPulseDuration1, pause1, minipause1;
   
    [SerializeField] Vector2 topPosition1, rightPosition1, leftPosition1;
    Bullet rock, leaf1, leaf2, leaf3;
    [Header("Pattern2")]
    [SerializeField] float rockspeed2, rockSpawnRate2, x2, y2, movespeed2, delay2, leafshotRate2, leafSpread2, leafSped2, leafdmg2;
    [SerializeField] int leaflines2;

    public override void SpawnWave() {
        rock = GameManager.gameData.rockBullet;
        leaf1 = GameManager.gameData.leafBullet1;
        leaf2 = GameManager.gameData.leafBullet2;
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
    IEnumerator PreFight2() {
        yield return new WaitForSeconds(1f);
        bossImage = Instantiate(image, new Vector2(-4.1f,4.1f), Quaternion.identity);
        float time = bossImage.GetComponent<Movement>().MoveTo(new Vector2(0, spawnLocationY), initialMoveSpeed);
        yield return new WaitForSeconds(time);
        Instantiate(slamEffect, new Vector2(0, spawnLocationY), Quaternion.identity);
        StartCoroutine(DialogueManager.StartDialogue(preFightDialogue2, Phase1));

    }

    void Phase1() {
        currentBoss = Instantiate(boss, new Vector2(0, spawnLocationY), Quaternion.identity);
        GameManager.currentBoss = currentBoss;
        currentBoss.shooting.StartShooting(Pattern1());
        currentBoss.bosshealth.OnLifeDepleted += EndPhase1;
        SwitchToBoss();
    }

    void EndPhase1() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase1;
        EndPhase();
        Invoke("Phase2", 1f);
    }

    IEnumerator Pattern1() {
        while (true) {
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

    void Phase2() {
        SpellCardUI(namesOfSpellCards[0]);
        Invoke("StartPattern2", 2f);

    }

    void StartPattern2() {
        SwitchToBoss();
        currentBoss.shooting.StartShooting(RockEmergingFromGround());
        currentBoss.shooting.StartShooting(MoveLeftAndRight2());
        currentBoss.bosshealth.OnLifeDepleted += EndPhase2;
    }
    void EndPhase2() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase2;
        EndPhase();
        Invoke("Phase3", 1f);

    }
    void End() {
        EndPhase();
        Destroy(bossImage);
        OnDefeat?.Invoke();
        DestroyAfter(5);
    }

    IEnumerator BarrageOfReflectingBullets(Bullet bul, float dmg, float angleMin, float angleMax, float speed, float shotRate) {
        return Functions.RepeatAction(() => ReflectingBullet(bul, dmg, currentBoss.transform.position,
            UnityEngine.Random.Range(angleMin, angleMax), speed), shotRate);
    }

    Bullet ReflectingBullet(Bullet bul, float dmg, Vector2 origin, float initialAngle, float initialSpeed) {
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

    IEnumerator RockEmergingFromGround() {
        return Functions.RepeatAction(() =>
            Patterns.ShootStraight(rock, rockdmg1, new Vector2(UnityEngine.Random.Range(-4f, 4f), -4), 90, rockspeed2), rockSpawnRate2);
        
    }

    IEnumerator MoveLeftAndRight2() {
        bool left = true;
        Vector2 leftPos = new Vector2(-x2, y2), rightPos = new Vector2(x2, y2);
        float time = currentBoss.movement.MoveTo(leftPos, initialMoveSpeed) ;
        yield return new WaitForSeconds(time);
        while (true) { 
            float time1 = currentBoss.movement.MoveTo(left?rightPos: leftPos, movespeed2);
            currentBoss.shooting.StartShootingFor(
                EnemyPatterns.ShootAtPlayerWithLines(leaf2, leafdmg2, currentBoss.transform, leafSped2, leafshotRate2, leafSpread2, leaflines2), 0, time1);
            yield return new WaitForSeconds(time1 + delay2);
            left = !left;
        }
    }
}
