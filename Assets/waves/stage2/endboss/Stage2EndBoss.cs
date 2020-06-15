using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Stage2EndBoss : EnemyBossWave
{
    // Start is called before the first frame update
    [SerializeField] GameObject key, image;
    [SerializeField] Dialogue dialogue1, dialogue2;
    [SerializeField] float spawnY;

    [SerializeField] BulletPack bigBullet, pointedBullet;
    [SerializeField] Bullet magicCircle;

    [Header("Pattern 1")]
    [SerializeField] float timeToRadius, radius;
    [SerializeField] float bigBulletSpeed1, bigBulletSpread, bigBulletShotRate;
    [SerializeField] int bigBulletNumber;
    [SerializeField] float initialSpeed1, radius1, finalSpeed1, smallBulletdelay1, angularVel, shotRate1, pulseRate;
    [SerializeField] int numberOfRings1, numberPerRings1;
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
        currentBoss.shooting.StartCoroutine(Pattern1());
        SwitchToBoss();
        currentBoss.bosshealth.OnLifeDepleted += EndPhase1;
    }

    void EndPhase1() { 
    
    }
    IEnumerator Pattern1() {
        Bullet magicCircle1 = EnemyPatterns.SummonMagicCircle(magicCircle, currentBoss.transform, timeToRadius, 210, radius, 0);
        Bullet magicCircle2 = EnemyPatterns.SummonMagicCircle(magicCircle, currentBoss.transform, timeToRadius, 330, radius, 0);
        Bullet magicCircleTop = EnemyPatterns.SummonMagicCircle(magicCircle, currentBoss.transform, timeToRadius, 90, radius, 0);
        yield return new WaitForSeconds(timeToRadius);
        Shooting top = magicCircleTop.GetComponent<Shooting>();
        top.StartShooting(EnemyPatterns.ShootAtPlayerWithLines(bigBullet.GetBullet(DamageType.Fire), magicCircleTop.transform, bigBulletSpeed1, bigBulletShotRate, bigBulletSpread, bigBulletNumber));
        Bullet bul = pointedBullet.GetBullet(DamageType.Water);
        SubPattern1(magicCircle1, true, bul);
        SubPattern1(magicCircle2, false, bul);
    }


    void SubPattern1(Bullet magicCircle, bool left, Bullet bul){
        Shooting shooting = magicCircle.GetComponent<Shooting>();
        float angular = left ? angularVel : -angularVel;
        Functions.StartMultipleCustomCoroutines(shooting, i => Functions.RepeatAction(() => EnemyPatterns.OutAndSpinRingOfBullets(bul, magicCircle.transform, initialSpeed1, radius1, finalSpeed1, angular, smallBulletdelay1, 0, numberPerRings1), pulseRate), numberOfRings1, shotRate1);
    }

}
