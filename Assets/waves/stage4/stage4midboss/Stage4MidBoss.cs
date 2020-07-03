using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Stage4MidBoss : EnemyBossWave
{
    // Start is called before the first frame update
    [SerializeField] ParticleSystem spawnEffect;
    [SerializeField] GameObject image;
    [SerializeField] Vector2 spawnLocation;
    [SerializeField] Dialogue prefightDialogue;
    Bullet fireBullet, fireRoundBall;
    [Header("Pattern1")]
    [SerializeField] float dmgfirebullet1 = 300f, dmgfireball1 = 300f;
    [SerializeField] float fireSpreadAngle = 30f, shotSpeed = 0.05f;
    [SerializeField] float firebullet1Max = 3f, firebullet1Min = 2f, fireballspeed1 = 4f, fireballshotRate = 1f;
    [SerializeField] int fireballLines1 = 30;
    [Header("Pattern2")]
    [SerializeField] float moveSpeed2 = 9;
    [SerializeField] float y2 = 1.5f;
    [SerializeField] Vector2 fireCircleLocationLeft, fireCircleLocationRight;
    [SerializeField] float angularvel2 = 90, shotRate2 = 0.1f, bulletspeed2 = 2f, bulletdmg2 = 250;
    [SerializeField] float laserDelay = 1.5f, delaytoNextLaser = 4f, laserangularVel = 45f, firebeamdmg = 1000, spinDuration;
    public override void SpawnWave()
    {
        fireBullet = GameManager.gameData.fireBullet;
        fireRoundBall = GameManager.gameData.smallRoundBullet.GetItem(DamageType.Fire);
        StartCoroutine(PreFight());
  
    }

    IEnumerator PreFight() {
        Destroy(Instantiate(spawnEffect, spawnLocation - new Vector2(0, 0.5f), Quaternion.Euler(-90,0,0)).gameObject, 5f);
        yield return new WaitForSeconds(0.5f);
        bossImage = Instantiate(image, spawnLocation, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        StartCoroutine(DialogueManager.StartDialogue(prefightDialogue, StartPhase1));

    }

    void StartPhase1() {
        currentBoss = Instantiate(boss, spawnLocation, Quaternion.identity);
        GameManager.currentBoss = currentBoss;
        SwitchToBoss();
        currentBoss.GetComponent<BulletOrientation>().SetCustomOrientation(
                    t => 90 + Functions.AimAt(currentBoss.transform.position, GameManager.playerPosition));
        currentBoss.shooting.StartShooting(Functions.RepeatAction(() => {
            float angle = Functions.AimAt(currentBoss.transform.position, GameManager.playerPosition);
            Patterns.ShootStraight(fireBullet, dmgfirebullet1, 
                Functions.RandomLocation(currentBoss.transform.position + Quaternion.Euler(0,0,angle) * new Vector2(0.5f, 0), 0.3f), angle + UnityEngine.Random.Range(-fireSpreadAngle, fireSpreadAngle),
                UnityEngine.Random.Range(firebullet1Min, firebullet1Max));
        }, shotSpeed));
        currentBoss.shooting.StartShooting(EnemyPatterns.PulsingBulletsRandomAngle(fireRoundBall, dmgfireball1, currentBoss.transform, fireballspeed1, fireballshotRate, fireballLines1));
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
        currentBoss.GetComponent<BulletOrientation>().Reset();
        currentBoss.transform.rotation = Quaternion.identity;
        currentBoss.shooting.StartCoroutine(Pattern2());
        currentBoss.bosshealth.OnDeath += End;
    }
    void End()
    {
        EndPhase();
        Destroy(bossImage);
        OnDefeat?.Invoke();
        DestroyAfter(5);
    }


    IEnumerator Pattern2() {
        float time = currentBoss.movement.MoveTo(new Vector2(0, y2), moveSpeed2);
        yield return new WaitForSeconds(time);
        Bullet fireCircle = GameManager.gameData.fireCircle;
        currentBoss.shooting.StartShooting(ShootFireCircle(fireCircle, currentBoss.transform.position, fireCircleLocationLeft, moveSpeed2,
            fireRoundBall, bulletdmg2, 0, angularvel2, bulletspeed2, shotRate2));
        currentBoss.shooting.StartShooting(ShootFireCircle(fireCircle, currentBoss.transform.position, fireCircleLocationRight, moveSpeed2,
            fireRoundBall, bulletdmg2, 180, -angularvel2, bulletspeed2, shotRate2));
        currentBoss.shooting.StartShooting(ShootFireBeam());

    }

    IEnumerator ShootFireCircle(Bullet firebul, Vector2 origin, Vector2 end, float speed, Bullet bul, float dmg, float startAngle, float angularvel, float bulletspeed, float shotRate) {
        Bullet circle = GameManager.bulletpools.SpawnBullet(firebul, origin);
        float time = circle.movement.MoveTo(end, speed);
        yield return new WaitForSeconds(time);
        circle.StartCoroutine(EnemyPatterns.ConstantSpinningStraightBullets(bul, dmg, circle.transform, bulletspeed, angularvel, startAngle, 4, shotRate));
        circle.orientation.StartRotating(angularvel, 90 + startAngle);
    }

    IEnumerator ShootFireBeam() {
        BulletOrientation bossOrientation = currentBoss.GetComponent<BulletOrientation>();
        while (true) {
            bossOrientation.enabled = false;
            yield return new WaitForSeconds(0.01f);
            float angle = Functions.AimAt(currentBoss.transform.position, GameManager.playerPosition);
            currentBoss.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
            Bullet firebeam = GameManager.bulletpools.SpawnBullet(GameManager.gameData.fireBeam, currentBoss.transform.position+ Quaternion.Euler(0, 0, angle) * new Vector2(0.5f, 0), 
                Quaternion.Euler(0, 0, angle));
            firebeam.SetDamage(firebeamdmg);
            firebeam.orientation.enabled = false;
            firebeam.transform.parent = currentBoss.transform;
            yield return new WaitForSeconds(laserDelay);
            bossOrientation.enabled = true;
            bossOrientation.angle = angle + 90;
            bossOrientation.SetCustomAngularVel(t =>
            {
                float theta = Functions.modulo(Functions.AimAt(bossOrientation.transform.position, GameManager.playerPosition) - (
                    bossOrientation.angle - 90), 360f);
                return theta > 180 ? -laserangularVel : laserangularVel;
            });
            yield return new WaitForSeconds(spinDuration);
            Quaternion current = currentBoss.transform.rotation;
            bossOrientation.Reset();
            bossOrientation.enabled = false;
            currentBoss.transform.rotation = current;
            Destroy(firebeam.gameObject);
            yield return new WaitForSeconds(delaytoNextLaser);
     

        }
    }
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

}
