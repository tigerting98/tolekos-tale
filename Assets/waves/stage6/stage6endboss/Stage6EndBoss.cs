
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class Stage6EndBoss : EnemyBossWave
{
    [SerializeField] bool harder;
    [SerializeField] GameObject background;
    [SerializeField] GameObject imageofboss;
    [SerializeField] Dialogue dialogue1, dialogue2, dialogue3;
    [SerializeField] ParticleSystem startparticle, spawnParticle;
    [SerializeField] float movespeed;
    [SerializeField] Sprite defaultPylfer, greenPylfer, redPylfer, bluePylfer;
    [SerializeField] ParticleSystem waterparticle, earthparticle, fireparticle, pureparticle;
    [SerializeField] ParticleSystem deathparticle;
    [SerializeField] Dialogue generalendDialogue, goodEndDialogue, badEndDialogue;
    [SerializeField] GameObject tpAway, tpAwayParticle;
    [SerializeField] SFX tpAwaySFX;
    [Header("Pattern1")]
    [SerializeField] float harderballspeed1, harderballpulserate1, harderballdmg1;
    [SerializeField] int harderballcount1;
    [SerializeField] float y1, buldmg1;
    [SerializeField] float movespeed1;
    [SerializeField] int petalCount1, bulletperhalfpatel1;
    [SerializeField] float fastspeed1, slowspeed1;
    [SerializeField] int stalkCount, stalkTopcount;
    [SerializeField] float stalktopspeeddiff, stalktopspread, stalktopspeed, stalkslowspeed, pulserate1;
    [Header("Pattern2")]
    [SerializeField] float harderstarspeed2, harderstarspreadangle2, harderstardmg2;
    [SerializeField] int harderstarcount2;
    [SerializeField] float dmg2;
    [SerializeField] Vector2 pos2;
    [SerializeField] float waterspeed2, waterpulserate2, watershotrate2, waterspacing2, waterrandomfactor2;
    [SerializeField] float boundaryY2, boundaryX2, maxstepY2, maxstepX2;
    [SerializeField] List<float> waterstartpos2, waterendpos2;
    [SerializeField] float laserdmg2;
    [SerializeField] float laserduration2, laserpulserate2, laserspacing2;
    [Header("Pattern3")]
    [SerializeField] float harderarrowspeed3, harderarrowpulserate3;
    [SerializeField] int harderarrowcount;
    [SerializeField] float dmg3;
    [SerializeField] float circleangularvel3, radius3;
    [SerializeField] float shotrate3circle, shotspeed3, shotspeeddiff3, bulletangularvel3;
    [SerializeField] int numberoflines3, numberperlines3;
    [SerializeField] float movespeed3, movingbounds3, movingdelaymin3, movingdelaymax3;
    [Header("Pattern4")]
    [SerializeField] float dmg4rain;
    [SerializeField] float anglespreadrain, rainminspeed, rainmaxspeed, rainshotrate;
    [SerializeField] float earthlaserdmg4, earthspeed4, leafinitialspeed4min, leafinitialspeed4max, leafacc4, leafacctime4, spreadleafangle4, leafdmg4;
    [SerializeField] int numberofleafs4;
    [SerializeField] int numberOfsideLasers4;
    [SerializeField] Vector2 locationright4, locationtop4, locationleft4;
    [SerializeField] float delayinpulse4, delaybeytweenpulse4, movespeed4, spreadanglelaser4, laserangle4;
    [Header("Pattern5")]
    [SerializeField] Vector2 pos5;
    [SerializeField] float starradius5, starshotrate5, stardmg5, delaybeforemoving5, delaybeforesplitting5, acc5, endspeed5, starspeed5, starttime5, starpulserate5 = 5f;
    [SerializeField] int numberOfStarsperside5;
    [Header("Pattern6")]
    [SerializeField] float startoffset;
    [SerializeField] float spacing6, shotrate6fire, firebulletspeed6, firebulletdmg6, angleperarc6, arcspacing6;
    [SerializeField] int bulletpershot6;
    [SerializeField] float earthbulletdmg6, earthpulserate6,earthspeed6;
    [SerializeField] int earthcount6;
    [Header("Pattern7")]
    [SerializeField] Vector2 pos7;
    [SerializeField] float bulletdmg7, bulletpulserate7, bulletspeed7, spawnX7, bounds7, movespeed7, delaymin7, delaymax7;
    [SerializeField] int bulletcount7;
    [Header("Pattern8")]
    [SerializeField] Vector2 pos8;
    float cooldown8 = 0;
    DamageType currentMode8 = DamageType.Pure;
    [SerializeField] float bullet8dmg, bullet8speed, bullet8cooldown;
    [SerializeField] float bulletspread8;
    [SerializeField] float balldmg8, ballangularvel8, ballradialvel8, ballpulserate8, ballshotrate8;
    [SerializeField] int ballcount8;
    [SerializeField] float switchingmin8, switchingmax8;
    [Header("Pattern9")]
    [SerializeField] Vector2 pos9;
    [SerializeField] float dmgbullet9, shotrate9, angularvel9, bulletspeed9;
    [SerializeField] int lines9 = 8;
    [SerializeField] float starlaserdmg9, starshotrate9, laseracc9, laseracctime9, starspeed9,starconvertimemin9, starconvertimemax9;
    [Header("Pattern10")]
    [SerializeField] float ellipseangularvel10, ellipseradialvel10, ellipsespacing10, ellipsepulserate10, ellipsedmg10;
   
    [SerializeField] int ellipsecount10;
    [SerializeField] int roundedcount10;
    int subphase = 1;
    [SerializeField] float dmg10rounded, roundedpulserate10, roundedspeed10;
    [SerializeField] float dmg10laser, laserspeed10, laserpulserate10, laserradius10;
    [SerializeField] int lasercount10;
    [SerializeField] float firelaserpulserate10, firelaserduration10, firelaserspacing10, firelaserdm10;
    [SerializeField] float fallinganglerange10, fallingspeed10min, fallingspeed10max, fallingshotrate10, fallingdmg10;
    [SerializeField] float phase4speed10, phase4pulserate10;
    [SerializeField] float bigcircledmg10, smallcircledmg10, arrowsdmg10, stardmg10;
    [SerializeField] int bigcirclecount10, smallcirclecount10, arrowcount10, starcount10;
    [SerializeField] float bigcircledelay10, smallcircledelay10, arrowdelay10, stardelay10;
    public override void SpawnWave()
    {
        Destroy(Instantiate(startparticle, new Vector2(0, 0), Quaternion.identity), 5f);
        StartCoroutine(PreWarmBullets());
        StartCoroutine(GameManager.maincamera.ShakeCamera(0.23f, 2f));
        AudioManager.current.PlaySFX(GameManager.gameData.stage6TpSFX);
        Invoke("StartAnimation", 0.8f);
    }
    public IEnumerator PreWarmBullets() {

        GameManager.bulletpools.PreWarmBullets(GameManager.gameData.ellipseBullet.GetItem(0), 40);
        yield return null;
        GameManager.bulletpools.PreWarmBullets(GameManager.gameData.ellipseBullet.GetItem(1), 40);
        yield return null;
        GameManager.bulletpools.PreWarmBullets(GameManager.gameData.ellipseBullet.GetItem(2), 40);
        yield return null;
        GameManager.bulletpools.PreWarmBullets(GameManager.gameData.ellipseBullet.GetItem(3), 40);
        yield return null;
        GameManager.bulletpools.PreWarmBullets(GameManager.gameData.starBullet.GetItem(0), 114);
        yield return null;
        GameManager.bulletpools.PreWarmBullets(GameManager.gameData.starBullet.GetItem(1), 114);
        yield return null;
        GameManager.bulletpools.PreWarmBullets(GameManager.gameData.starBullet.GetItem(2), 114);
        yield return null;
        GameManager.bulletpools.PreWarmBullets(GameManager.gameData.starBullet.GetItem(3), 114);
        yield return null;
    }
    public override void EndPhase()
    {
        ChangePylfer(DamageType.Pure);
        ChangePylferResist(0.85f, 0.85f, 0.85f, 1);
        base.EndPhase();

    }
    void ChangePylferResist(float water, float earth, float fire, float pure) {
        currentBoss.damagetaker.WaterMultiplier = water;
        currentBoss.damagetaker.EarthMultiplier = earth;
        currentBoss.damagetaker.FireMultiplier = fire;
        currentBoss.damagetaker.PureMultiplier = pure;
    }
    void ChangePylfer(DamageType type) {
        GameObject obj = Instantiate(
            type == DamageType.Water ? waterparticle : type == DamageType.Earth ? earthparticle : type == DamageType.Fire ? fireparticle : pureparticle , currentBoss.transform.position, Quaternion.identity).gameObject;
        Destroy(obj, 5f);
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
        AudioManager.current.PlaySFX(GameManager.gameData.pylferTpSFX);
        Destroy(Instantiate(spawnParticle, new Vector2(0, 0), Quaternion.identity), 5);
        Invoke("SpawnBossImage", 0.5f);
        Invoke("StartDialogue2", 2f);
    }
    void StartDialogue2() {
        StartCoroutine(DialogueManager.StartDialogue(dialogue2, StartDialogue3));
    }
    void StartDialogue3() {
        GameManager.PlayEndBossMusic();
        StartCoroutine(DialogueManager.StartDialogue(dialogue3, Phase1));
    }
    void Phase1() {
        currentBoss = Instantiate(boss, new Vector2(0, 0), Quaternion.identity);
        GameManager.currentBoss = currentBoss;
        SwitchToBoss();
        float time = currentBoss.movement.MoveTo(new Vector2(0, y1), movespeed);
        currentBoss.shooting.StartShootingAfter(BossPattern1(), time);
        if (harder)
        {
            currentBoss.shooting.StartShooting(EnemyPatterns.PulsingBulletsRandom(GameManager.gameData.smallRoundBullet.GetAllItems(),
              harderballdmg1, currentBoss.transform, harderballspeed1, harderballpulserate1, harderballcount1, GameManager.gameData.gunSFX));
                }
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
        boss.transform.position = pos2;
        ChangePylfer(DamageType.Water);
        ChangePylferResist(0.5f, 1.5f, 0.25f, 1f);
        currentBoss.shooting.StartShooting(WaterPattern2());
        currentBoss.shooting.StartShooting(Functions.RepeatAction(
            () =>
            {
                for (float x = 0; x < 4.5f; x += laserspacing2)
                {
                    Bullet bul = Instantiate(GameManager.gameData.fireBeam2, new Vector2(x, 4.2f), Quaternion.Euler(0, 0, -90));
                    bul.SetDamage(laserdmg2);
                    bul.orientation.SetFixedOrientation(-90);
                    Destroy(bul.gameObject, laserduration2);
                    Bullet bul2 = Instantiate(GameManager.gameData.fireBeam2, new Vector2(-x, 4.2f), Quaternion.Euler(0, 0, -90));
                    bul2.SetDamage(laserdmg2);
                    bul2.orientation.SetFixedOrientation(-90);
                    Destroy(bul2.gameObject, laserduration2);
                    
                }
            }, laserpulserate2));
        currentBoss.shooting.StartShootingAfter(Functions.RepeatAction(() => AudioManager.current.PlaySFX(GameManager.gameData.laser1SFX), laserpulserate2), 1f);
        currentBoss.bosshealth.OnLifeDepleted += EndPhase2;

    }
    void EndPhase2() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase2;
        Instantiate(GameManager.gameData.lifeDrop1000, currentBoss.transform.position, Quaternion.identity);
        float time= bossImage.GetComponent<Movement>().MoveTo(pos2, movespeed);
        EndPhase();
        Invoke("Phase3", Mathf.Max(time,endPhaseTransition));
    }
    void Phase3() {
        SwitchToBoss();
        magicCircle3(DamageType.Water, -30);
        magicCircle3(DamageType.Earth, -150);
        magicCircle3(DamageType.Fire, 90);
        if (harder) {
            currentBoss.shooting.StartShooting(EnemyPatterns.PulsingBulletsRandomAngle(GameManager.gameData.arrowBullet.GetItem(DamageType.Pure), dmg3, currentBoss.transform, harderarrowspeed3,
                harderarrowpulserate3, harderarrowcount, GameManager.gameData.magicPulse1SFX));
        }
        currentBoss.shooting.StartShooting(EnemyPatterns.MoveRandomly(currentBoss.movement, pos2, movingbounds3, movespeed3, movingdelaymin3, movingdelaymax3));
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
        ChangePylfer(DamageType.Earth);
        ChangePylferResist(0.25f, 0.5f, 1.5f, 1f);
        Bullet rain = GameManager.gameData.raindrop;
        currentBoss.shooting.StartShooting(Functions.RepeatAction(
        () => Patterns.ShootStraight(rain, dmg4rain, new Vector2(UnityEngine.Random.Range(-4f, 4f), 4.1f), -90 + UnityEngine.Random.Range(-anglespreadrain, anglespreadrain),
        UnityEngine.Random.Range(rainminspeed, rainmaxspeed), null), rainshotrate
        ));
        currentBoss.shooting.StartShooting(Pattern4());
        currentBoss.bosshealth.OnLifeDepleted += EndPhase4;
    }
    void EndPhase4() {
        Instantiate(GameManager.gameData.defaultBombDrop, currentBoss.transform.position, Quaternion.identity);
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase4;
        EndPhase();
        Invoke("Phase5", endPhaseTransition);
    }
    void Phase5() {
        SwitchToBoss();
        float time = currentBoss.movement.MoveTo(pos5, movespeed);
        currentBoss.shooting.StartShootingAfter(Functions.RepeatAction(
            () => {
                float angle2 = UnityEngine.Random.Range(0, 360f);
                float angle = UnityEngine.Random.Range(0, 360f);
                currentBoss.shooting.StartShooting(StarPattern(angle, currentBoss.transform.position, DamageType.Pure, 0, 30 + angle2, starttime5));
                currentBoss.shooting.StartShooting(StarPattern(angle, currentBoss.transform.position, DamageType.Water, starspeed5, 30 + angle2, starttime5));
                currentBoss.shooting.StartShooting(StarPattern(angle, currentBoss.transform.position, DamageType.Earth, starspeed5, 150 + angle2, starttime5));
                currentBoss.shooting.StartShooting(StarPattern(angle, currentBoss.transform.position, DamageType.Fire, starspeed5, 270 + angle2, starttime5));
            }, starpulserate5

            ), time);
        currentBoss.bosshealth.OnLifeDepleted += EndPhase5;
    }
    void EndPhase5() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase5;
        EndPhase();
        Invoke("StartPhase6", endPhaseTransition);
    }
    void StartPhase6() {
        SpellCardUI(namesOfSpellCards[2]);
        bossImage.GetComponent<Movement>().MoveTo(new Vector2(0, 0), movespeed);
        Invoke("Phase6", spellCardTransition);
    }
    void Phase6() {
        SwitchToBoss();
        ChangePylfer(DamageType.Fire);
        ChangePylferResist(1.5f, 0.25f, 0.5f, 1f);
        currentBoss.transform.position = new Vector2(0, 0);
        currentBoss.shooting.StartShooting(SummonRingOfFire(startoffset));
        currentBoss.shooting.StartShooting(EnemyPatterns.PulsingBulletsRandomAngle(
            GameManager.gameData.leafBullet2, earthbulletdmg6, currentBoss.transform, earthspeed6, earthpulserate6, earthcount6, GameManager.gameData.shortarrowSFX
            ));
        currentBoss.bosshealth.OnLifeDepleted += EndPhase6;
        }
    void EndPhase6() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase6;
        Instantiate(GameManager.gameData.lifeDrop1000, currentBoss.transform.position, Quaternion.identity);
        EndPhase();
        Invoke("Phase7", endPhaseTransition);
    }
    void Phase7() {
        SwitchToBoss();
        currentBoss.movement.MoveTo(pos7, movespeed);
        ActionTrigger<Movement> trigger = new ActionTrigger<Movement>(movement => !Functions.WithinBounds(movement.transform.position, 4f));
        trigger.OnTriggerEvent += movement =>
        {
            Vector2 pos = movement.transform.position;
            if (pos.y >= 4)
            {


                movement.transform.position = new Vector2(movement.transform.position.x, 3.99f);
                movement.graph = Movement.ReflectPathAboutX(movement.graph);
            }
            else if (pos.y <= -4)
            {
                movement.ResetTriggers();
            }
            else
            {
                movement.graph = Movement.ReflectPathAboutY(movement.graph);
            }
        };
        currentBoss.shooting.StartShooting(Functions.RepeatCustomAction(
            i => Patterns.CustomRing(
                angle =>
                {
                    Bullet bul = Patterns.ShootStraight(GameManager.gameData.ellipseBullet.GetItem(i % 3), bulletdmg7,
                       (Vector2)currentBoss.transform.position + new Vector2(i % 2 == 0 ? -spawnX7 : spawnX7, 0), angle, bulletspeed7, GameManager.gameData.gunSFX);
                    bul.movement.triggers.Add(trigger);
                    return bul;
                }, UnityEngine.Random.Range(0, 360f), bulletcount7
                ), bulletpulserate7));
        currentBoss.shooting.StartShooting(EnemyPatterns.MoveRandomly(currentBoss.movement, pos7, bounds7, movespeed7, delaymin7, delaymax7));
        currentBoss.bosshealth.OnLifeDepleted += EndPhase7;
    }

    public void EndPhase7() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase7;
        EndPhase();
        
        Invoke("StartPhase8", endPhaseTransition);
    }
    public void StartPhase8() {
        bossImage.GetComponent<Movement>().MoveTo(pos8, movespeed);
        SpellCardUI(namesOfSpellCards[3]);
        Invoke("Phase8", spellCardTransition);
    }
    public void Phase8() {
        SwitchToBoss();
        currentBoss.damagetaker.OnDamageTaken += ReflectBullet;
        currentBoss.shooting.StartShooting(Functions.RepeatCustomAction(
            i => 
                currentBoss.shooting.StartShooting(Pattern8((i % 2) == 0)), ballpulserate8
            ));
        currentBoss.shooting.StartShooting(SwitchElement());
        currentBoss.bosshealth.OnLifeDepleted += EndPhase8;
    }
    public void EndPhase8() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase8;
        currentBoss.damagetaker.OnDamageTaken -= ReflectBullet;

        EndPhase();
        Invoke("StartPhase9", endPhaseTransition);

    }
    public void StartPhase9() {
        bossImage.GetComponent<Movement>().MoveTo(pos9, movespeed);
        SpellCardUI(namesOfSpellCards[4]);
        Invoke("Phase9", spellCardTransition);
    }

    public void Phase9() {
        SwitchToBoss();
        currentBoss.shooting.StartShooting(ShootSlowSpiral(true, -90));
        currentBoss.shooting.StartShooting(ShootSlowSpiral(false, -90));
        currentBoss.shooting.StartShooting(Functions.RepeatAction(
            () => RainingLasers((DamageType)UnityEngine.Random.Range(0, 4), UnityEngine.Random.Range(-4f, 4f)), starshotrate9
            )) ;

        currentBoss.bosshealth.OnLifeDepleted += EndPhase9;
    }
    public void EndPhase9() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase9;
        Instantiate(GameManager.gameData.defaultBombDrop, currentBoss.transform.position, Quaternion.identity);
        Instantiate(GameManager.gameData.lifeDrop1000, currentBoss.transform.position, Quaternion.identity);
        EndPhase();
        Invoke("StartPhase10", endPhaseTransition);
    }
    public void StartPhase10() {
        SpellCardUI(namesOfSpellCards[5]);
        Invoke("Phase10", spellCardTransition);
    }
    public void Phase10() {
        SwitchToBoss();
        ChangePylferResist(0.85f, 0.85f, 0.85f, 0.75f);
        currentBoss.shooting.StartShooting(CheckForSubPhase());
        currentBoss.shooting.StartShooting(Functions.RepeatCustomAction(
            i => Patterns.CustomRing(angle => Patterns.ReflectingBullet(GameManager.gameData.pointedBullet.GetItem(i % 4), dmg10rounded, currentBoss.transform.position,
            angle, roundedspeed10, GameManager.gameData.magicPulse1SFX), UnityEngine.Random.Range(0,360f), roundedcount10), roundedpulserate10));

    }
    
    
    public IEnumerator CheckForSubPhase() {
        float bosshp = currentBoss.bosshealth.maxHP;
        while (currentBoss && subphase < 4) {
            if (subphase == 1 && currentBoss.bosshealth.GetCurrentHP() / bosshp < 0.75f) {
                subphase = 2;
                StartSubPattern2();
                ChangePylferResist(0.85f, 0.85f, 0.85f, 0.5f);
                Destroy(Instantiate(pureparticle, currentBoss.transform.position, Quaternion.identity).gameObject, 5f);
            }
            else if (subphase == 2 && currentBoss.bosshealth.GetCurrentHP() / bosshp < 0.5f)
            {
                StartSubPattern3();
                ChangePylferResist(0.85f, 0.85f, 0.85f, 0.35f);
                subphase = 3;
                Destroy(Instantiate(pureparticle, currentBoss.transform.position, Quaternion.identity).gameObject, 5f);
            }
            if (subphase == 3 && currentBoss.bosshealth.GetCurrentHP() / bosshp < 0.25f)
            {
                ChangePylferResist(0.85f, 0.85f, 0.85f, 0.15f);
                StartSubPattern4();
                subphase = 4;
                Destroy(Instantiate(pureparticle, currentBoss.transform.position, Quaternion.identity).gameObject, 5f);
            }
            yield return null;
        }
        currentBoss.bosshealth.OnDeath += EndPhase10;
    }
    public void EndPhase10() {
        if (currentBoss)
        {
            currentBoss.bosshealth.OnDeath -= EndPhase10;
        }
        EndPhase();
        StartCoroutine(DeathAnimation());
    }
    void PlayMultipleParticleEffect(ParticleSystem system, int times, Vector2 location) {
        for (int i = 0; i < times; i++) {
            Instantiate(system, location, Quaternion.identity);
        }
    }
    IEnumerator DeathAnimation() {
        PlayMultipleParticleEffect(waterparticle, 15, bossImage.transform.position);
        yield return new WaitForSeconds(0.2f);
        PlayMultipleParticleEffect(earthparticle, 15, bossImage.transform.position);
        yield return new WaitForSeconds(0.2f);
        PlayMultipleParticleEffect(fireparticle, 15, bossImage.transform.position);
        yield return new WaitForSeconds(0.2f);
        PlayMultipleParticleEffect(deathparticle, 1, bossImage.transform.position);
        yield return new WaitForSeconds(1f);
        StartCoroutine(DialogueManager.StartDialogue(generalendDialogue, ()=>StartCoroutine(TeleportAway())));

    }
    IEnumerator TeleportAway() {
        Destroy(Instantiate(tpAway, bossImage.transform.position, Quaternion.identity), 5f);
        Destroy(Instantiate(tpAwayParticle, bossImage.transform.position, Quaternion.identity), 5f);
        yield return new WaitForSeconds(0.5f);
        AudioManager.current.PlaySFX(tpAwaySFX);
        yield return new WaitForSeconds(2.15f);
        Destroy(bossImage.gameObject);
        yield return new WaitForSeconds(1f);
        StartCoroutine(DialogueManager.StartDialogue(PlayerStats.deathCount > 0 ? badEndDialogue : goodEndDialogue, LoadNext));
    }
    void LoadNext() {
        SceneManager.LoadScene("AfterStage6Scene");
    }
    void StartSubPattern2()
    {
        currentBoss.shooting.StartShooting(Functions.RepeatCustomAction(
                    i => Patterns.RingAroundBossAimAtPlayer(GameManager.gameData.laserBullet.GetItem(i % 4), dmg10laser, currentBoss.transform.position, laserradius10, laserspeed10, lasercount10, GameManager.gameData.gunSFX)
                    , laserpulserate10));
        currentBoss.shooting.StartShooting(Functions.RepeatCustomAction(
            i =>
            {
                Patterns.SpirallingOutwardsRing(GameManager.gameData.ellipseBullet.GetItem(i % 4), ellipsedmg10, (Vector2)currentBoss.transform.position - new Vector2(ellipsespacing10, 0),
                   ellipseradialvel10, ellipseangularvel10, ellipsecount10, 0, GameManager.gameData.magicPulse1SFX);
                Patterns.SpirallingOutwardsRing(GameManager.gameData.ellipseBullet.GetItem(3 - (i % 4)), ellipsedmg10, (Vector2)currentBoss.transform.position + new Vector2(ellipsespacing10, 0),
                   ellipseradialvel10, -ellipseangularvel10, ellipsecount10, 0, null);
            }, ellipsepulserate10
            ));
    }
    void StartSubPattern3() {
        currentBoss.shooting.StartShooting(Functions.RepeatAction(() =>
        Patterns.ShootStraight(GameManager.gameData.icicle, fallingdmg10, new Vector2(UnityEngine.Random.Range(-4f,4f), 4.3f), -90 + UnityEngine.Random.Range(-fallinganglerange10, fallinganglerange10),
        UnityEngine.Random.Range(fallingspeed10min, fallingspeed10max), null), fallingshotrate10));
       
        currentBoss.shooting.StartShooting(Functions.RepeatAction(() =>
        Patterns.ShootStraight(GameManager.gameData.rockBullet, fallingdmg10, new Vector2(UnityEngine.Random.Range(-4f, 4f), -4.3f), 90 + UnityEngine.Random.Range(-fallinganglerange10, fallinganglerange10),
        UnityEngine.Random.Range(fallingspeed10min, fallingspeed10max), null), fallingshotrate10));
        
        currentBoss.shooting.StartShooting(Functions.RepeatAction(
            () =>
            {
                Bullet bul1 = Instantiate(GameManager.gameData.fireBeam2, new Vector2(0, 4.2f), Quaternion.Euler(0, 0, -90));
                bul1.SetDamage(firelaserdm10);
                bul1.orientation.SetFixedOrientation(-90);
                Destroy(bul1.gameObject, firelaserduration10);
                for (float x = firelaserspacing10; x < 4.5f; x += firelaserspacing10)
                {
                    Bullet bul = Instantiate(GameManager.gameData.fireBeam2, new Vector2(x, 4.2f), Quaternion.Euler(0, 0, -90));
                    bul.SetDamage(firelaserdm10);
                    bul.orientation.SetFixedOrientation(-90);
                    Destroy(bul.gameObject, firelaserduration10);
                    Bullet bul2 = Instantiate(GameManager.gameData.fireBeam2, new Vector2(-x, 4.2f), Quaternion.Euler(0, 0, -90));
                    bul2.SetDamage(firelaserdm10);
                    bul2.orientation.SetFixedOrientation(-90);
                    Destroy(bul2.gameObject, firelaserduration10);
                }
            }, firelaserpulserate10));
        currentBoss.shooting.StartShootingAfter(Functions.RepeatAction(
           () => AudioManager.current.PlaySFX(GameManager.gameData.laser1SFX)
          , firelaserpulserate10),1f);

    }
    void StartSubPattern4() {
        currentBoss.shooting.StartShootingAfter(
            Functions.RepeatAction(() => MultiColorBullets(GameManager.gameData.bigBullet, bigcircledmg10, currentBoss.transform.position, phase4speed10,
            UnityEngine.Random.Range(0f, 360f), bigcirclecount10, GameManager.gameData.windboltSFX), phase4pulserate10), bigcircledelay10);
        currentBoss.shooting.StartShootingAfter(
           Functions.RepeatAction(() => MultiColorBullets(GameManager.gameData.smallRoundBullet, smallcircledmg10, currentBoss.transform.position, phase4speed10,
           UnityEngine.Random.Range(0f, 360f), smallcirclecount10, GameManager.gameData.windboltSFX), phase4pulserate10), smallcircledelay10);
        currentBoss.shooting.StartShootingAfter(
           Functions.RepeatAction(() => MultiColorBullets(GameManager.gameData.starBullet, stardmg10, currentBoss.transform.position, phase4speed10,
           UnityEngine.Random.Range(0f, 360f), starcount10, GameManager.gameData.windboltSFX), phase4pulserate10), stardelay10);
        currentBoss.shooting.StartShootingAfter(
           Functions.RepeatAction(() => MultiColorBullets(GameManager.gameData.arrowBullet, arrowsdmg10, currentBoss.transform.position, phase4speed10,
           UnityEngine.Random.Range(0f, 360f), arrowcount10, GameManager.gameData.windboltSFX), phase4pulserate10), arrowdelay10);
    }

    void MultiColorBullets(BulletPack pack, float dmg, Vector2 origin, float speed, float offset, int lines, SFX sfx) {
        float start = offset;
        float diff = 360f / lines;
        for (int i = 0; i < lines; i++) {
            Patterns.ShootStraight(pack.GetItem(i % 4), dmg, origin, start, speed, sfx);
            start += diff;
        }
    }

    public Bullet RainingLasers(DamageType type, float x) {
        Bullet bul = Patterns.ShootStraight(GameManager.gameData.starBullet.GetItem(type), starlaserdmg9, new Vector2(x, 4.1f), -90, starspeed9, null);
        float time2 = UnityEngine.Random.Range(starconvertimemin9, starconvertimemax9);
        ActionTrigger<Movement> trigger = new ActionTrigger<Movement>(movement => movement.time > time2);
        trigger.OnTriggerEvent += movement =>
        {
            float angle = Functions.AimAtPlayer(movement.transform);
            Bullet bul2 = Patterns.ShootCustomBullet(GameManager.gameData.laserBullet.GetItem(type), starlaserdmg9, movement.transform.position,
                t => new Polar(t < laseracctime9 ? laseracc9 : 0, angle).rect, MovementMode.Acceleration, GameManager.gameData.shortarrowSFX);
            bul2.transform.rotation = Quaternion.Euler(0, 0, angle);
            movement.GetComponent<Bullet>().Deactivate();
        };
        bul.movement.triggers.Add(trigger);
        return bul;
    }
    public IEnumerator ShootSlowSpiral(bool clockwise, float offset) {
        float angle = offset;
        int i = 0;
        while (currentBoss) {
            int j = i % 4;
            Patterns.RingOfBullets(GameManager.gameData.smallRoundBullet.GetItem(clockwise ? j : 3 - j), dmgbullet9, currentBoss.transform.position, lines9, angle, bulletspeed9, GameManager.gameData.magicPulse1SFX);
            angle += (clockwise ? -angularvel9 : angularvel9)* shotrate9;
            i++;
            yield return new WaitForSeconds(shotrate9);
        }
    }
    public IEnumerator SwitchElement() {
        int i = 0;
        while (currentBoss) {
            currentMode8 = (DamageType)(i % 3);
            AudioManager.current.PlaySFX(GameManager.gameData.magicPulse1LouderSFX);
            ChangePylfer(currentMode8);
            ChangePylferResist(currentMode8 == DamageType.Fire ? 1.5f : 0, currentMode8 == DamageType.Water ? 1.5f : 0, currentMode8 == DamageType.Earth? 1.5f : 0, 1f);
            i++;
            yield return new WaitForSeconds(UnityEngine.Random.Range(switchingmin8, switchingmax8));
        }
    }
    public IEnumerator Pattern8(bool bl) {
        Patterns.SpirallingOutwardsRing(GameManager.gameData.smallRoundBullet.GetItem(currentMode8), balldmg8, currentBoss.transform.position, ballradialvel8, (bl ? -1 : 1) * ballangularvel8, ballcount8, 0, GameManager.gameData.gunSFX);
        yield return new WaitForSeconds(ballshotrate8);
        Patterns.SpirallingOutwardsRing(GameManager.gameData.smallRoundBullet.GetItem(currentMode8), balldmg8, currentBoss.transform.position, ballradialvel8, (bl ? 1 : -1) * ballangularvel8, ballcount8, 0, GameManager.gameData.gunSFX);
    }
    public void ReflectBullet(DamageDealer dmg) {
        DamageType correctMode = currentMode8 == DamageType.Water ? DamageType.Earth : currentMode8 == DamageType.Earth ? DamageType.Fire : currentMode8 == DamageType.Fire ? DamageType.Water : DamageType.Pure;
        if (correctMode != dmg.damageType&& dmg.damageType != DamageType.Pure) {
            ShootAtPlayer8(dmg.damageType);
        }
    }
    public void ShootAtPlayer8(DamageType type)
    {
        if (cooldown8 <= 0)
        {
            Bullet bul =Patterns.ShootStraight(GameManager.gameData.bigBullet.GetItem(type), bullet8dmg, currentBoss.transform.position,
               Functions.AimAtPlayer(currentBoss.transform) + UnityEngine.Random.Range(-bulletspread8, bulletspread8), bullet8speed, GameManager.gameData.clickSFX);
            bul.transform.localScale *= 0.3f;
            cooldown8 = bullet8cooldown;
                
       }
    }

    IEnumerator SummonRingOfFire(float offset ) {
        float angle = offset;
        float nextarc = offset + angleperarc6;
        while (currentBoss) {
            for (int i = 0; i < bulletpershot6; i++) {
                Patterns.ShootStraight(GameManager.gameData.fireBullet, firebulletdmg6, currentBoss.transform.position, angle, firebulletspeed6, GameManager.gameData.firestreamingSFX);
                    angle += spacing6;
                    if (angle > nextarc)
                {
                    angle += arcspacing6;
                    nextarc = angle + angleperarc6;
                }

            }
            yield return new WaitForSeconds(shotrate6fire);
        }
    }
    IEnumerator WaterPattern2() {
        while (currentBoss)
        {
            float currentx = currentBoss.transform.position.x;
            float x = currentx + (currentx < pos2.x+boundaryX2 - maxstepX2 ? currentx > pos2.x-boundaryX2 + maxstepX2 ? UnityEngine.Random.Range(-maxstepX2, maxstepX2) :
                UnityEngine.Random.Range(0, maxstepX2) : UnityEngine.Random.Range(-maxstepX2, 0));
            float currenty = currentBoss.transform.position.y;
            float y = currenty + (currenty < pos2.y+boundaryY2 - maxstepY2 ? currenty > pos2.y - boundaryY2 + maxstepY2 ? UnityEngine.Random.Range(-maxstepY2, maxstepY2) :
                UnityEngine.Random.Range(0, maxstepY2) : UnityEngine.Random.Range(-maxstepY2, 0));
            float time1 = currentBoss.movement.MoveTo(new Vector2(x, y), movespeed);
            yield return new WaitForSeconds(time1);
            currentBoss.transform.position = new Vector2(x, y);
            if (harder)
            {
                Patterns.ShootMultipleStraightBullet(GameManager.gameData.starBullet.GetItem(DamageType.Pure), harderstardmg2, currentBoss.transform.position, harderstarspeed2,
                  Functions.AimAtPlayer(currentBoss.transform), harderstarspreadangle2, harderstarcount2, GameManager.gameData.gunSFX);
            }
            currentBoss.shooting.StartShooting(WaterSubPattern2(true, true, currentBoss.transform.position));
            currentBoss.shooting.StartShooting(WaterSubPattern2(false, true, currentBoss.transform.position));
            currentBoss.shooting.StartShooting(WaterSubPattern2(true, false, currentBoss.transform.position));
            currentBoss.shooting.StartShooting(WaterSubPattern2(false, false, currentBoss.transform.position));
            yield return new WaitForSeconds(waterpulserate2);
        }
    }
    IEnumerator WaterSubPattern2(bool left, bool up, Vector2 start) {
        Assert.IsTrue(waterstartpos2.Count == waterendpos2.Count);
        for (int i = 0; i <waterstartpos2.Count; i++){
            float startpos = waterstartpos2[i];
            while (startpos < waterendpos2[i]) {
                Patterns.ShootStraight(GameManager.gameData.icicle, dmg2, start + new Vector2((left ? -1 : 1) * startpos, UnityEngine.Random.Range(-waterrandomfactor2, waterrandomfactor2))
                    , up ? 90 : -90, waterspeed2, GameManager.gameData.clickSFX);
                startpos += waterspacing2;
                yield return new WaitForSeconds(watershotrate2);
            }

        }
    }
    IEnumerator StarPattern(float offset, Vector2 origin, DamageType type, float speed, float direction, float timemoving) {
        GameObject centre = new GameObject();
        centre.transform.position = origin;
        centre.AddComponent<Movement>();
        float angle = offset;
        for (int i = 0; i < 5; i++) {
            Vector2 pos = origin + new Polar(starradius5, angle).rect;
            Vector2 increpos = (new Polar(starradius5, angle - 144).rect - new Polar(starradius5, angle).rect) / numberOfStarsperside5;
            for (int j = 0; j < numberOfStarsperside5; j++) {
                Bullet bul =Patterns.ShootStraight(GameManager.gameData.starBullet.GetItem(type), stardmg5, pos, 0, 0, GameManager.gameData.clickSFX);
                
                bul.transform.parent = centre.transform;
                pos += increpos;
                yield return new WaitForSeconds(starshotrate5);
            }
            angle -= 144;
        }
        yield return new WaitForSeconds(delaybeforemoving5);
        AudioManager.current.PlaySFX(GameManager.gameData.kiraSFX);
        if (centre)
        { centre.GetComponent<Movement>().MoveAndStopAfter(new Polar(speed, direction).rect, timemoving); }
        yield return new WaitForSeconds(delaybeforesplitting5);
        foreach (Transform child in centre.transform) {
            child.GetComponent<Movement>().AccelerateTowards(acc5, centre.transform.position, endspeed5);
        }
        Destroy(centre, 60f);
    }
    IEnumerator Pattern4() {
        while (currentBoss) {
            float time1 = currentBoss.movement.MoveTo(locationleft4, movespeed4);
            yield return new WaitForSeconds(time1);
            if (currentBoss)
            {
                EarthLaser4(180 + laserangle4);
                for (int i = 1; i < numberOfsideLasers4; i++) {
                    EarthLaser4(180 + laserangle4 + i* spreadanglelaser4);
                    EarthLaser4(180 + laserangle4 - i * spreadanglelaser4);
                }

            }
            yield return new WaitForSeconds(delayinpulse4);
            if (currentBoss)
            {
                float time2 = currentBoss.movement.MoveTo(locationright4, movespeed4);
                yield return new WaitForSeconds(time2);
            }
            if (currentBoss)
            {
                EarthLaser4(-laserangle4);
                for (int i = 1; i < numberOfsideLasers4; i++)
                {
                    EarthLaser4(-laserangle4 + i * spreadanglelaser4);
                    EarthLaser4(-laserangle4 - i * spreadanglelaser4);
                }

            }
            yield return new WaitForSeconds(delayinpulse4);
            if (currentBoss)
            {
                float time3 = currentBoss.movement.MoveTo(locationtop4, movespeed4);
                yield return new WaitForSeconds(time3);
            }
            if (currentBoss)
            {
                float angle = Functions.AimAtPlayer(currentBoss.transform);
                EarthLaser4(angle);
                for (int i = 1; i < numberOfsideLasers4; i++)
                {
                    EarthLaser4(angle + i * spreadanglelaser4);
                    EarthLaser4(angle - i * spreadanglelaser4);
                }

            }
            yield return new WaitForSeconds(delaybeytweenpulse4);
        }
    }

    Bullet EarthLaser4(float angle) {
        Bullet bul = Patterns.ShootStraight(GameManager.gameData.laserBullet.GetItem(DamageType.Earth), earthlaserdmg4, currentBoss.transform.position, angle, earthspeed4, GameManager.gameData.laser1SFX);
        ActionTrigger<Movement> trigger = new ActionTrigger<Movement>(movement => !Functions.WithinBounds(movement.transform.position, 4f, 5.5f));
        trigger.OnTriggerEvent += movement =>
        {
            currentBoss.shooting.StartCoroutine(GameManager.maincamera.ShakeCamera(0.06f, 0.1f));
            for (int i = 0; i < numberofleafs4; i++)
            {
                EnemyPatterns.FallingBullet(GameManager.gameData.leafBullet2, leafdmg4, movement.transform.position,
                  (movement.transform.position.x > 0 ? 180 : 0) + UnityEngine.Random.Range(-spreadleafangle4, spreadleafangle4), leafacc4, leafacctime4, 
                  UnityEngine.Random.Range(leafinitialspeed4min, leafinitialspeed4max), GameManager.gameData.slamSFX);
            }
            movement.GetComponent<Bullet>().Deactivate();
        };
        bul.movement.triggers.Add(trigger);
        return bul;
    }
    Bullet magicCircle3(DamageType type, float angle) {
        Bullet magicCircle = GameManager.gameData.magicCircles.GetItem(type);
        
        Bullet bul = GameManager.gameData.arrowBullet.GetItem(type);
        Bullet circle = GameManager.bulletpools.SpawnBullet(magicCircle, (Vector2)(currentBoss.transform.position) + new Polar(radius3, angle).rect);
        circle.movement.destroyBoundary = 8f;
        circle.movement.SetPolarPath(t => new Polar(radius3, angle + circleangularvel3 * t));
        circle.transform.parent = currentBoss.transform;
        EnemyPatterns.StartFanningPattern(bul, dmg3, circle.GetComponent<Shooting>(), shotspeed3, -bulletangularvel3, 0, numberoflines3, shotrate3circle, numberperlines3, shotspeeddiff3, GameManager.gameData.shortarrowSFX);
        return circle;
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
        AudioManager.current.PlaySFX(GameManager.gameData.magicPulse1LouderSFX);
        // currentBoss.shooting.StartShooting(FlowerStalks(type4, stalktopspeed, stalkslowspeed, offset));
        // yield return null;

        SingleFlower(type3, fastspeed1 * 3, slowspeed1 * 3, offset);
        yield return null;
        SingleFlower(type2, fastspeed1 * 2, slowspeed1 * 2, offset);
        yield return null;
        SingleFlower(type1, fastspeed1, slowspeed1, offset);
        yield return null;
        currentBoss.shooting.StartShooting(FlowerStalks(type4, stalktopspeed, stalkslowspeed, offset));
    }

    IEnumerator FlowerStalks(DamageType type, float fastspeed, float slowspeed, float offset) {
        Bullet bul = GameManager.gameData.starBullet.GetItem(type);
        float speed = fastspeed;
        float diff = (fastspeed - slowspeed) / (stalkCount - 1);
        //int z = 0;
        float stalkspeed = fastspeed +  stalktopspeeddiff;
        for (int j = 1; j <= stalkTopcount; j++)
        {

            Patterns.RingOfBullets(bul, buldmg1, currentBoss.transform.position, petalCount1, offset + j * stalktopspread, stalkspeed, null);
            Patterns.RingOfBullets(bul, buldmg1, currentBoss.transform.position, petalCount1, offset - j * stalktopspread, stalkspeed, null);
            stalkspeed += stalktopspeeddiff;
            yield return null;
        }
        
        for (int i = 0; i < stalkCount; i++) {
            Patterns.RingOfBullets(bul, buldmg1, currentBoss.transform.position, petalCount1, offset, speed, null);
            speed -= diff;
            yield return null;
            
        }
        
        
    }

    void SingleFlower(DamageType type, float fastspeed, float slowspeed, float offset)
    {

        float incre = 360f / (petalCount1 * bulletperhalfpatel1 * 2);
        float angle = offset;
        float speed = slowspeed;
        float speedincre = (fastspeed - slowspeed) / bulletperhalfpatel1;
        Bullet bul = GameManager.gameData.ellipseBullet.GetItem(type);
      
            for (int j = 0; j < bulletperhalfpatel1; j++)
            {
                Patterns.RingOfBullets(bul, buldmg1, currentBoss.transform.position, petalCount1, angle, speed, null);
   
                angle += incre;
                speed += speedincre;
            }
            for (int j = 0; j < bulletperhalfpatel1; j++)
            {
            Patterns.RingOfBullets(bul, buldmg1, currentBoss.transform.position, petalCount1, angle, speed, null);
            angle += incre;
                speed -= speedincre;
            }

        

    }

    private void Update()
    {
        if (cooldown8 > 0) {
            cooldown8 -= Time.deltaTime;
        }
    }
}


