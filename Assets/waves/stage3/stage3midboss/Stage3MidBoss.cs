using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Stage3MidBoss : EnemyBossWave
{
    // Start is called before the first frame update
    [SerializeField] GameObject earthCircle;
    GameObject actualCircle;
    [SerializeField] GameObject image;
    [SerializeField] ParticleSystem slamEffect;
    [SerializeField] float initialMoveSpeed;
    [SerializeField] float spawnLocationY;
    [SerializeField] float spawnDelay;
    [SerializeField] Dialogue preFightDialogue1, preFightDialogue2;
    [Header("Pattern1")]
    [SerializeField] float movementSpeed1, pulseRate1, pauseDuration1, x1, y1, dmg1,anglerange1, initialspeed1min, intialspeed1max, acceleration1;
    [SerializeField] int pulses1, bulletsperPulse1;
    [Header("Pattern1")]
    [SerializeField] float y2, ringspeed, bulletspeedfast, bulletspeedtime, bulletspeedslow, radiusAroundPlayer, ballDmg, pulserate2, timetoExplode;
    [SerializeField] int numberOfRings, numberOfBulletsPerRing;




    public override void SpawnWave() {
        StartCoroutine(PreFight1());
      
    }
     IEnumerator PreFight1() {
        actualCircle = Instantiate(earthCircle, new Vector2(0, spawnLocationY), Quaternion.identity);
        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(DialogueManager.StartDialogue(preFightDialogue1, () => StartCoroutine(PreFight2())));
        

    }
    IEnumerator PreFight2() {
        yield return new WaitForSeconds(1f);
        bossImage = Instantiate(image, new Vector2(0, 4.1f), Quaternion.identity);
        float time = bossImage.GetComponent<Movement>().MoveTo(new Vector2(0, spawnLocationY), initialMoveSpeed);
        yield return new WaitForSeconds(time);
        Instantiate(slamEffect, new Vector2(0, spawnLocationY-0.5f), Quaternion.Euler(-90, 0, 0));
        StartCoroutine(DialogueManager.StartDialogue(preFightDialogue2, Phase1));

    }

    void Phase1() {
        currentBoss = Instantiate(boss, new Vector2(0, spawnLocationY), Quaternion.identity);
        GameManager.currentBoss = currentBoss;
        actualCircle.transform.SetParent(currentBoss.transform);
        SwitchToBoss();
        currentBoss.shooting.StartCoroutine(Pattern1());
        currentBoss.bosshealth.OnLifeDepleted += EndPhase1;
    }

    void EndPhase1() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase1;
        EndPhase();
        Invoke("Phase2", endPhaseTransition);
    }

    IEnumerator Pattern1() {
        float time = currentBoss.movement.MoveTo(new Vector2(-x1, y1), initialMoveSpeed);
        yield return new WaitForSeconds(time);
        currentBoss.shooting.StartCoroutine(MoveLeftAndRight());

    }

    void Phase2() {
        SpellCardUI(namesOfSpellCards[0]);
        Invoke("StartPattern2", spellCardTransition);
        currentBoss.bosshealth.OnDeath += End;

    }

    void StartPattern2() {
        SwitchToBoss();
        float time = currentBoss.movement.MoveTo(new Vector2(0, y2), initialMoveSpeed);
        currentBoss.shooting.StartShootingAfter(ShootRingsAtPlayer(), time);

    }
    void End() {
        EndPhase();
        Destroy(bossImage);
        OnDefeat?.Invoke();
        DestroyAfter(5);
    }


    IEnumerator MoveLeftAndRight() {
        bool left = true;
        while (true)
        { 
            Bullet leaf = GameManager.gameData.leafBullet1;
        float time = currentBoss.movement.MoveTo(new Vector2(left ? x1 : -x1, y1), movementSpeed1);
        currentBoss.shooting.StartCoroutine(Functions.RepeatActionXTimes(()=>
            FallingBullets(leaf, dmg1, currentBoss.transform.position, bulletsperPulse1) , pulseRate1, pulses1));
            left = !left;
        yield return new WaitForSeconds(pauseDuration1 + time);
        }
    }


    public List<Bullet> FallingBullets(Bullet bul, float dmg, Vector2 origin, int number)
    {
        List<Bullet> bullets = new List<Bullet>();
        for (int i = 0; i < number; i++)
        {
            float initialSpeed = UnityEngine.Random.Range(initialspeed1min, intialspeed1max);
            bullets.Add(EnemyPatterns.FallingBullet(bul, dmg, origin, 90 + UnityEngine.Random.Range(-anglerange1, anglerange1), acceleration1, initialSpeed * 2 / acceleration1, initialSpeed));
        }
        return bullets;
    }


    IEnumerator ShootRingsAtPlayer() {
        while (true)
        {
            Bullet ring = GameManager.gameData.earthCircle;
            Bullet ball = GameManager.gameData.smallRoundBullet.GetItem(DamageType.Earth);
            Functions.StartMultipleCustomCoroutines(currentBoss.shooting,
                    i => ShootRing(ring, ball, currentBoss.transform.position,
                   GameManager.playerPosition + (Vector2)(Quaternion.Euler(0, 0, i * 360f / numberOfRings) * new Vector2(radiusAroundPlayer, 0))
                   , timetoExplode), numberOfRings);
            yield return new WaitForSeconds(pulserate2);
        }
    
    }

    IEnumerator ShootRing(Bullet ring, Bullet ball, Vector2 origin, Vector2 target, float timeToExplode) {
        Bullet ring1 = GameManager.bulletpools.SpawnBullet(ring, origin);
        ring1.movement.destroyBoundary = 5f;
        ring1.movement.MoveTo(target, ringspeed);
        yield return new WaitForSeconds(timeToExplode);
        if (ring1&&ring1.gameObject.activeInHierarchy)
        {
            ring1.Deactivate();
            Patterns.ExplodingRingOfBullets(ball, ballDmg, target, numberOfBulletsPerRing, UnityEngine.Random.Range(0f, 360f),
                bulletspeedfast, bulletspeedslow, bulletspeedtime);
        }

        
    }

}
