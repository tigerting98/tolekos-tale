using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stage6EndBoss : EnemyBossWave
{
    [SerializeField] GameObject background;
    [SerializeField] GameObject imageofboss;
    [SerializeField] Dialogue dialogue1, dialogue2;
    [SerializeField] ParticleSystem startparticle, spawnParticle;
    [SerializeField] float movespeed;
    [SerializeField] Sprite defaultPylfer, greenPylfer, redPylfer, bluePylfer;
    [Header("Pattern1")]
    [SerializeField] float y1, buldmg1;
    [SerializeField] float movespeed1;
    [SerializeField] int petalCount1, bulletperhalfpatel1;
    [SerializeField] float fastspeed1, slowspeed1;
    [SerializeField] int stalkCount, stalkTopcount;
    [SerializeField] float stalktopspeeddiff, stalktopspread, stalktopspeed, stalkslowspeed, pulserate1;
    [Header("Pattern2")]
    [SerializeField] float dmg2;
    [SerializeField] Vector2 pos2;
    [SerializeField] float minspeed2, maxspeed2, shotrate2, delaybeforestop2, delaybeforechanging2, acc2, time2;
    [SerializeField] float shotrate2ring, angularvel2ring, radialvel2ring, ringdmg2;
    [SerializeField] int numberperring2;
    [Header("Pattern3")]
    [SerializeField] float dmg3;
    [SerializeField] float circleangularvel3, circlemovespeed3, radius3;
    [SerializeField] float shotrate3circle, shotspeed3, shotspeeddiff3, bulletangularvel3;
    [SerializeField] int numberoflines3, numberperlines3;
    public override void SpawnWave()
    {
        Destroy(Instantiate(startparticle, new Vector2(0, 0), Quaternion.identity), 5f);
        Invoke("StartAnimation", 0.8f); 
    }
    public override void EndPhase()
    {
        ChangePylfer(DamageType.Pure);
        ChangePylferResist(0.8f, 0.8f, 0.8f, 1);
        base.EndPhase();

    }
    void ChangePylferResist(float water, float earth, float fire, float pure) {
        currentBoss.damagetaker.WaterMultiplier = water;
        currentBoss.damagetaker.EarthMultiplier = earth;
        currentBoss.damagetaker.FireMultiplier = fire;
        currentBoss.damagetaker.PureMultiplier = pure;
    }
    void ChangePylfer(DamageType type) {
        currentBoss.GetComponent<SpriteRenderer>().sprite = type == DamageType.Water ? bluePylfer : type == DamageType.Earth ? greenPylfer : type == DamageType.Fire ? redPylfer : defaultPylfer;
    }
    public void StartAnimation() {
        Instantiate(background, new Vector3(0, 0, 0.85f), Quaternion.identity);
        Invoke("StartDialogue1", 2f);
    }
    public void StartDialogue1() {
        StartCoroutine(DialogueManager.StartDialogue(dialogue1, SpawnAnimation));
    }
    void SpawnBossImage() {
        bossImage = Instantiate(imageofboss, new Vector2(0, 0), Quaternion.identity);
    }
    public void SpawnAnimation() {
        Destroy(Instantiate(spawnParticle, new Vector2(0, 0), Quaternion.identity), 5);
        Invoke("SpawnBossImage", 0.5f);
        Invoke("StartDialogue2", 2f);
    }
    void StartDialogue2() {
        StartCoroutine(DialogueManager.StartDialogue(dialogue2, Phase1));
    }
    void Phase1() {
        currentBoss = Instantiate(boss, new Vector2(0, 0), Quaternion.identity);
        GameManager.currentBoss = currentBoss;
        SwitchToBoss();
        float time = currentBoss.movement.MoveTo(new Vector2(0, y1), movespeed);
        currentBoss.shooting.StartShootingAfter(BossPattern1(), time);
        currentBoss.bosshealth.OnLifeDepleted += EndPhase1;
    }
    void EndPhase1() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase1;
        EndPhase();
        Invoke("StartPhase2", endPhaseTransition);
    }
    void StartPhase2() {
        SpellCardUI(namesOfSpellCards[0]);
        bossImage.GetComponent<Movement>().MoveTo(pos2, movespeed);
        Invoke("Phase2", spellCardTransition);
    }
    void Phase2() {
        SwitchToBoss();
        ChangePylfer(DamageType.Water);
        ChangePylferResist(0.5f, 1.5f, 0.25f, 1f);
        currentBoss.shooting.StartShooting(Functions.RepeatAction(
            () => ChangingBullet(UnityEngine.Random.Range(minspeed2, maxspeed2), acc2, time2, delaybeforestop2, delaybeforechanging2, UnityEngine.Random.Range(0f,360f)), shotrate2
            ));
        currentBoss.bosshealth.OnLifeDepleted += EndPhase2;
       /*
        currentBoss.shooting.StartShooting(Functions.RepeatAction(
            () => Patterns.SpirallingOutwardsRing(GameManager.gameData.bigBullet.GetItem(DamageType.Pure), ringdmg2, currentBoss.transform.position, radialvel2ring, angularvel2ring, numberperring2, 0, null)
           , shotrate2ring));*/
    }
    void EndPhase2() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase2;

        EndPhase();
        Invoke("Phase3", endPhaseTransition);
    }
    void Phase3() {
        SwitchToBoss();
        magicCircle3(DamageType.Water, -30);
        magicCircle3(DamageType.Earth, -150);
        magicCircle3(DamageType.Fire, 90);

    }

    Bullet magicCircle3(DamageType type, float angle) {
        Bullet magicCircle = GameManager.gameData.magicCircles.GetItem(type);
        Bullet bul = GameManager.gameData.pointedBullet.GetItem(type);
        Bullet circle = GameManager.bulletpools.SpawnBullet(magicCircle, (Vector2)(currentBoss.transform.position) + new Polar(radius3, angle).rect);
        circle.movement.SetPolarPath(t => new Polar(radius3, angle + circleangularvel3 * t));
        circle.transform.localScale *= 0.5f;
        circle.transform.parent = currentBoss.transform;
        EnemyPatterns.StartFanningPattern(bul, dmg3, circle.GetComponent<Shooting>(), shotspeed3, -bulletangularvel3, 0, numberoflines3, shotrate3circle, numberperlines3, shotspeeddiff3, null);
        return circle;
    }
    Bullet ChangingBullet(float speed1, float acc2, float time2, float delay1, float delay2, float angle) {
        Bullet bul = Patterns.ShootCustomBullet(GameManager.gameData.fireBall, dmg2, Functions.RandomLocation(currentBoss.transform.position, 0.3f),
            t => new Polar(t < delay1 ? speed1 : 0, angle).rect, MovementMode.Velocity, null);
        bul.movement.destroyBoundary = 4.05f;
        ActionTrigger<Movement> trigger = new ActionTrigger<Movement>(movement => movement.time > delay1 + delay2);
        trigger.OnTriggerEvent += movement =>
        {
            float angle2 = Functions.AimAtPlayer(movement.transform);
            Patterns.ShootCustomBullet(GameManager.gameData.icicle, dmg2, movement.transform.position,
                t => new Polar(t < time2 ? acc2 : 0, angle2).rect, MovementMode.Acceleration, null);
            movement.GetComponent<Bullet>().Deactivate();
        };
        bul.movement.triggers.Add(trigger);
        return bul;
        
    }
    IEnumerator BossPattern1() {
        int i = 0;
        while (currentBoss) {
            currentBoss.shooting.StartShooting(FlowerPattern((DamageType)(i%4), (DamageType)((i+1) % 4), (DamageType)((i + 2) % 4), (DamageType)((i + 3) % 4), UnityEngine.Random.Range(0,360f)));
            yield return new WaitForSeconds(pulserate1);
            float time = currentBoss.movement.MoveTo(Functions.RandomLocation(-1, 1, y1, y1), movespeed1);
            yield return new WaitForSeconds(time);
            i++;


        }
    }

    IEnumerator FlowerPattern(DamageType type1, DamageType type2, DamageType type3, DamageType type4, float offset) {
        currentBoss.shooting.StartShooting(FlowerStalks(type4, stalktopspeed, stalkslowspeed, offset));
        yield return null;
        SingleFlower(type1, fastspeed1, slowspeed1, offset);
        yield return null;
        SingleFlower(type2, fastspeed1*2, slowspeed1*2, offset);
        yield return null;
        SingleFlower(type3, fastspeed1*3, slowspeed1*3, offset);
    }

    IEnumerator FlowerStalks(DamageType type, float fastspeed, float slowspeed, float offset) {
        Bullet bul = GameManager.gameData.starBullet.GetItem(type);
        float speed = slowspeed;
        float diff = (fastspeed - slowspeed) / (stalkCount - 1);
        int z = 0;
        for (int i = 0; i < stalkCount; i++) {
            Patterns.RingOfBullets(bul, buldmg1, currentBoss.transform.position, petalCount1, offset, speed, null);
            speed += diff;
            z++;
            if (z > 3) {
                yield return null;
                z = 0;
            }
        }
        for (int j = 1; j <= stalkTopcount; j++) {
            Patterns.RingOfBullets(bul, buldmg1, currentBoss.transform.position, petalCount1, offset + j * stalktopspread , fastspeed + (j-stalkTopcount)*stalktopspeeddiff, null);
            Patterns.RingOfBullets(bul, buldmg1, currentBoss.transform.position, petalCount1, offset - j * stalktopspread, fastspeed + (j - stalkTopcount) * stalktopspeeddiff, null);
        
        }
        
    }
    void SingleFlower(DamageType type, float fastspeed, float slowspeed, float offset) {
       
        float incre = 360f / (petalCount1 * bulletperhalfpatel1 * 2);
        float angle = offset;
        float speed = slowspeed;
        float speedincre = (fastspeed - slowspeed) / bulletperhalfpatel1;
        Bullet bul = GameManager.gameData.ellipseBullet.GetItem(type);
        for (int i = 0; i < petalCount1; i++) {
            for (int j = 0; j < bulletperhalfpatel1; j++) {
                Patterns.ShootStraight(bul, buldmg1, currentBoss.transform.position, angle, speed, null);
                angle += incre;
                speed += speedincre;
            }
            for (int j = 0; j < bulletperhalfpatel1; j++)
            {
                Patterns.ShootStraight(bul, buldmg1, currentBoss.transform.position, angle, speed, null);
                angle += incre;
                speed -= speedincre;
            }

        }
       
    }
}


