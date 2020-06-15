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
    [SerializeField] Bullet magicCircle;
    [SerializeField] float radius, radiusVariance, angularVel;
    [SerializeField] float timeToRadius;
    [SerializeField] float delayBeforeShooting;
    [SerializeField] Bullet waterBullet;
    [SerializeField] float bulletPattern1Speed, pattern1ShotRate, pattern1PulseRate;
    [SerializeField] int numberOfLines, pattern1BulletsPerPulse;

    [Header("Pattern 2")]
    [SerializeField] float bulletPattern2Speed, pattern2ShotRate, pattern2angularvel;
    [SerializeField] int numberOfLines2;


    public override void SpawnWave() {
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
        StartCoroutine(DialogueManager.StartDialogue(preFightDialogue2, Phase1));

    }

    void Phase1() {
        currentBoss = Instantiate(boss, spawnLocation, Quaternion.identity);
        GameManager.currentBoss = currentBoss;
        actualCircle.transform.SetParent(currentBoss.transform);
        SwitchToBoss();
        currentBoss.shooting.StartCoroutine(Pattern1());
        currentBoss.bosshealth.OnLifeDepleted += EndPhase1;
    }

    void EndPhase1() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase1;
        EndPhase();
        Invoke("Phase2", 1f);
    }

    void Phase2() {
        SpellCardUI(namesOfSpellCards[0]);
        Invoke("StartPattern2", 2f);
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
        currentBoss.shooting.StartCoroutine(EnemyPatterns.BorderOfWaveAndParticle(waterBullet,
            currentBoss.transform, bulletPattern2Speed, pattern2ShotRate, numberOfLines2, pattern2angularvel));
   
    }
    IEnumerator Pattern1() {
        List<Bullet> magicCircles = Patterns.CustomRing(

            angle =>  Patterns.ShootCustomBullet(magicCircle, currentBoss.transform.position, Movement.RotatePath(
             angle, t => new Polar(t > timeToRadius ? radius + (float)(radiusVariance*Math.Sin(t-timeToRadius)) : radius * t / timeToRadius, angularVel * t).rect), MovementMode.Position)
            , 0, numberofMagicCircles);

        yield return new WaitForSeconds(delayBeforeShooting);
        currentBoss.enemyAudio.PlayAudioForXTimeAndPause(bulletSpawnSound, pattern1ShotRate, pattern1BulletsPerPulse, pattern1PulseRate);
        int i = 0;
        foreach (Bullet bul in magicCircles) {
            bul.transform.SetParent(currentBoss.transform);

            int y = i;
            Shooting shooting = bul.GetComponent<Shooting>();          
            shooting.StartCoroutine(EnemyPatterns.RepeatSubPatternWithInterval(
                ()=> EnemyPatterns.PulsingLines(waterBullet, bul.transform, bulletPattern1Speed, 
                y%2==0? 0:Patterns.AimAt(bul.transform.position, GameManager.playerPosition), pattern1ShotRate, numberOfLines, pattern1BulletsPerPulse), shooting, pattern1PulseRate));
            i++;
        }
     
        
    
    }

   

    

}
