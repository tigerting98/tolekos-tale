using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Stage2MidBoss : EnemyBossWave
{
    // Start is called before the first frame update
    [SerializeField] GameObject waterCircle;
    GameObject actualCircle;
    [SerializeField] GameObject image;
    [SerializeField] Vector2 spawnLocation;
    [SerializeField] float spawnDelay;
    [SerializeField] Dialogue preFightDialogue1, preFightDialogue2;
    [Header("Pattern 1")]
    [SerializeField] int numberofMagicCircles;
    [SerializeField] float radius, radiusVariance, angularVel;
    [SerializeField] float timeToRadius;
    [SerializeField] float delayBeforeShooting;
    Bullet waterBullet;
    [SerializeField] float bulletPattern1Speed, pattern1ShotRate, pattern1PulseRate, dmg1 = 130;
    [SerializeField] int numberOfLines, pattern1BulletsPerPulse;

    [Header("Pattern 2")]
    [SerializeField] float bulletPattern2Speed, pattern2ShotRate, pattern2angularvel, dmg2 = 200;
    [SerializeField] int numberOfLines2;


    public override void SpawnWave() {
        waterBullet = GameManager.gameData.pointedBullet.GetItem(DamageType.Water);
        StartCoroutine(PreFight1());
    }
     IEnumerator PreFight1() {
        actualCircle = Instantiate(waterCircle, spawnLocation, Quaternion.identity);
        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(DialogueManager.StartDialogue(preFightDialogue1, () => StartCoroutine(PreFight2())));
        

    }
    IEnumerator PreFight2() {
        bossImage = Instantiate(image, spawnLocation, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Destroy(actualCircle.gameObject);
        StartCoroutine(DialogueManager.StartDialogue(preFightDialogue2, Phase1));

    }

    void Phase1() {
        currentBoss = Instantiate(boss, spawnLocation, Quaternion.identity);
        currentBoss.GetComponent<BasicDroppable>().otherDrops.Add(GameManager.gameData.lifeDrop300);
        GameManager.currentBoss = currentBoss;
        SwitchToBoss();
        currentBoss.shooting.StartCoroutine(Pattern1());
        currentBoss.bosshealth.OnLifeDepleted += EndPhase1;
    }

    void EndPhase1() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase1;
        EndPhase();
        Invoke("Phase2", endPhaseTransition);
    }

    void Phase2() {
        SpellCardUI(namesOfSpellCards[0]);
        Invoke("StartPattern2", spellCardTransition);
        currentBoss.bosshealth.OnDeath += End;

    }

    void End() {
        EndPhase();
        Destroy(bossImage);
        OnDefeat?.Invoke();
        DestroyAfter(5);
    }
    void StartPattern2() {
        SwitchToBoss();
        currentBoss.shooting.StartCoroutine(EnemyPatterns.BorderOfWaveAndParticle(waterBullet, dmg2,
            currentBoss.transform, bulletPattern2Speed, pattern2ShotRate, numberOfLines2, pattern2angularvel,GameManager.gameData.waterstreaming1SFX));
   
    }
    IEnumerator Pattern1() {
        List<Bullet> magicCircles = Patterns.CustomRing(

            angle =>  Patterns.ShootCustomBullet(GameManager.gameData.waterCircle,0, currentBoss.transform.position, Movement.RotatePath(
             angle, t => new Polar(t > timeToRadius ? radius + (float)(radiusVariance*Math.Sin(t-timeToRadius)) :
             radius * t / timeToRadius, angularVel * t).rect), MovementMode.Position, GameManager.gameData.magicCircleSummonSFX)
            , 0, numberofMagicCircles);

        yield return new WaitForSeconds(delayBeforeShooting);
        int i = 0;
        foreach (Bullet bul in magicCircles) {
            bul.transform.SetParent(currentBoss.transform);

            int y = i;
            Shooting shooting = bul.GetComponent<Shooting>();          
            shooting.StartCoroutine(EnemyPatterns.RepeatSubPatternWithInterval(
                ()=> EnemyPatterns.PulsingLines(waterBullet, dmg1, bul.transform, bulletPattern1Speed, 
                y%2==0? 0:Functions.AimAtPlayer(bul.transform), pattern1ShotRate, numberOfLines, 
                pattern1BulletsPerPulse, GameManager.gameData.shortarrowSFX), shooting, pattern1PulseRate));
            i++;
        }
     
        
    
    }

   

    

}
