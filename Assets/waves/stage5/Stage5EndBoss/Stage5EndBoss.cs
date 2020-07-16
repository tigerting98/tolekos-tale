using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Stage5EndBoss : EnemyBossWave
{
    [SerializeField] Sprite waterportrait, earthportrait, fireportrait, combineportrait;
    [SerializeField] Stage5EndBossUI ui;
    [SerializeField] Stage5Boss waterprefab, earthprefab, fireprefab;
    Stage5Boss waterBoss, earthBoss, fireBoss;
    [SerializeField] Movement waterimageprefab, earthimageprefab, fireimageprefab;
    Movement waterimage, earthimage, fireimage;
    [SerializeField] float movespeed = 10f;
    [SerializeField] Vector2 waterspawnlocation, earthspawnlocation, firespawnlocation;
    [SerializeField] Dialogue preFightDialogue1, preFightDialogue2;
    Bullet waterCircle, earthCircle, fireCircle;
    Stage5EndBossUI bossUI;
    [SerializeField] Color blue, green, red;
    [SerializeField] Stage5BossPointer pointer;
    [SerializeField] bool harder;
    [Header("Pattern1")]
    [SerializeField] float firedmg1;
    [SerializeField] float fireshotRate1 = 0.1f, fireSpeed1 = 3f, fireanglespread1 = 15f, fireradius1 = 0.3f;
    [SerializeField] float earthdmg1 = 400, earthshotRate1 = 1f, earthspeed1 = 3f, earthspread1 = 20f;
    [SerializeField] int earthnumber1 = 30;
    [SerializeField] float waterdmg1 = 365, watershotrate1 = 0.15f, waterangularvel1 = 58f, waterspeed1 = 4f, circledist1 = 1f, circlespeed1 = 3f, waterspeeddiff = 0.2f;
    [SerializeField] int waternumber1 = 5;
    [SerializeField] float movespeed1 = 3f, radiusmovement1 = 1f;
    [SerializeField] Vector2 waterorigin1;
    [Header("Pattern2")]
    [SerializeField] Vector2 startingPoint2;
    [SerializeField] float snowflakedmg2 = 415, snowflakespinningVel2 = 187, snowflakerandomfactor2 = 5, snowspeedmin = 3, snowspeedmax = 4, spawnRatesnow = 0.05f;
    [SerializeField] float icicledmg2 = 450f, spreadrange2 = 15f, anglepershot2 = 8.5f, shotrateicicle2 = 0.05f, iciclespeed2 = 2.8f, spawnradius2= 0.5f;
    [SerializeField] float harderpulserate2, harderspeed2, harderdmg2;
    [SerializeField] int hardercount2;
    [Header("Pattern3")]
    [SerializeField] Vector2 startingPoint3;
    [SerializeField] float y3, radius3, bulletspeed3nonmain, bulletdmg3nonmain, bulletpulserate3, bossmovespeed3,x3;
    [SerializeField] int ringnumber3;
    [SerializeField] float angularvel3, shotrate3, pulseduration3, bulletspeed3main, bulletspeeddiff3, bulletdmg3main, delay3, bigdmg3 = 900, bigspeed3 = 3f, bigduration3 = 0.5f;
    [SerializeField] int number3main;
    [Header("Pattern4")]
    [SerializeField] float treerockspeed4;
    [SerializeField] float treerockdmg4;
    [SerializeField] float shotRate4, unitspercircle4, acceleration4, time4, delay4, dmg4star, size4 = 0.65f;
    [SerializeField] float spacing4 = 1.3f, randomfactor4 = 0.5f, pulseRate4 = 7f;
    [Header("Pattern5")]
    [SerializeField] Vector2 startingPoint5;
    [SerializeField] float y5, radius5, bulletspeed5nonmain, bulletdmg5nonmain, bossmovespeed5,x5, angularvel5, shotrate5nonmain;
    [SerializeField] int magiccirclenumber5, bulletnumber5nonmain;
    [SerializeField] float shotrate5, bulletspeed5main, bulletdmg5main, bigdmg5 = 900, bigspeed5 = 3f, bigduration5 = 0.5f, radiusmain5;
    [SerializeField] float boundaryx, maxstepx, movementpulseduration5;
    [Header("Pattern6")]
    [SerializeField] Vector2 startingPoint6;
    [SerializeField] float wheelspeed6 = 2f, angularvel6 = 40f, shotrate6wheel = 2f, dmg6wheel = 2500f, movespeed6=10, spreadangle6 = 43f,wheelsize = 1.5f;
    [SerializeField] int wheellines6 = 2;
    [SerializeField] Vector2 left6, right6;
    [SerializeField] float firespeed6 = 3f, firedmg6 = 410, firepulserate6 = 3f, fireshotrate6 = 0.3f, bigdmg6 = 500, delay6;
    [SerializeField] int firenumbersperpulse6 = 5, numberoffirebullets = 30, bignumber = 10;
    [SerializeField] float  harderangularvel6;
    [Header("Pattern7")]
    [SerializeField] Vector2 startingPoint7 = new Vector2(0,2);
    [SerializeField] float movementradius7 = 1f, freq7 = 2f;
    [SerializeField] float earthPillarspacingY = 0.5f, earthpillarspacingX = 0.5f, earthpillarspawnmin = 4.5f, earthpillarspawnmax = 3.5f;
    [SerializeField] float earthPillarmovementX = 3f, earthPillarmovementY = 0.2f, earthpillarpulseRate = 5f, earthpillardmg = 2000f;
    [SerializeField] int numberOfEarthpillars = 10;
    [SerializeField] float firepillarspacingX = 0.5f, firepillarspacingY = 0.5f, firepillarspawnmin = 4.2f, firepillarspawnmax = 3.9f;
    [SerializeField] float firePillarmovementX = 0.3f, firePillarmovementY = 3f, firepillarpulseRate = 5f, firepillardmg = 2000f;
    [SerializeField] int numberOffirepillars = 10;
    [SerializeField] int numberofsnowflakes7;
    [SerializeField] float snowflakeshotrate7, snowflakedmg7, snowflakeangularvel7, snowflakeradialvel7;
    [Header("Pattern8")]
    [SerializeField] Vector2 startingPoint8 = new Vector2(0, 2);
    [SerializeField] float radius8, angularvel8, laserdmg8, thickness8 = 0.5f;
    [SerializeField] float shotRate8, shotspeed8, shot8dmg;
    [SerializeField] float firedmg8, firespread8, firespeed8;
    [SerializeField] int firenumber8;
    [SerializeField] float snowdmg8, snowradialvel8, snowangularvel8;
    [SerializeField] int snownumber8;
    [SerializeField] float leafdmg8= 400, minspeed8leaf = 1.5f, speeddiff8leaf = 0.2f, leafspreadangle8 = 30f, leafsize8 = 0.8f;
    [SerializeField] int leaflines8 = 3, leafperline8 = 5;
    [SerializeField] Dialogue endDialogue;
    bool[] donebools7 = new bool[3] { false, false, false};
    bool[] donebools8 = new bool[3] { false, false, false };
    public override void SpawnWave() {
        waterCircle = GameManager.gameData.waterCircle;
        earthCircle = GameManager.gameData.earthCircle;
        fireCircle = GameManager.gameData.fireCircle;
        try
        {

            GameObject.Find("stage5quad").GetComponent<Animator>().SetTrigger("StopMoving");
        }
        catch (Exception ex) {
            Debug.Log(ex);
        }
        StartCoroutine(preFight1());
        
        

    }
    public IEnumerator preFight1() {
        SetUp();
        waterimage = Instantiate(waterimageprefab, new Vector2(-4.1f, 4.1f), Quaternion.identity);
        waterimage.destroyBoundary = 10f;
        float time = waterimage.MoveTo(waterspawnlocation, movespeed);
        yield return new WaitForSeconds(time);
        StartCoroutine(DialogueManager.StartDialogue(preFightDialogue1, ()=> StartCoroutine(preFight2())));

    }
    public IEnumerator preFight2()
    {
        earthimage = Instantiate(earthimageprefab, new Vector2(0, 4.1f), Quaternion.identity);
        earthimage.destroyBoundary = 10f;
        float time1 = earthimage.MoveTo(earthspawnlocation, movespeed);
        fireimage = Instantiate(fireimageprefab, new Vector2(4.1f, 4.1f), Quaternion.identity);
        fireimage.destroyBoundary = 10f;
        float time2 = fireimage.MoveTo(firespawnlocation, movespeed);
        yield return new WaitForSeconds(Math.Max(time1, time2));
        StartCoroutine(DialogueManager.StartDialogue(preFightDialogue2, Phase1));

    }
    public void Phase1() {
        waterBoss = Instantiate(waterprefab, waterimage.transform.position, Quaternion.identity);
        Stage5BossPointer waterPointer = Instantiate(pointer, new Vector2(0, -4.2f), Quaternion.identity);
        waterPointer.trackingBoss = waterBoss;
        waterPointer.sprite.color = blue;
        waterBoss.SetUpBossHealthbar(bossUI.waterHealthBar);
        SwitchToBoss(DamageType.Water);
        float timeearth = earthimage.MoveTo(new Vector2(-3.5f, 3.5f), movespeed);
        float timefire = fireimage.MoveTo(new Vector2(3.5f, 3.5f), movespeed);
        waterBoss.shooting.StartShootingAfter(Functions.RepeatAction(
            ()=>Patterns.ShootStraight(GameManager.gameData.fireBall, firedmg1, Functions.RandomLocation(fireimage.transform.position, fireradius1),
            Functions.AimAtPlayer(fireimage.transform) + UnityEngine.Random.Range(-fireanglespread1, fireanglespread1), fireSpeed1,null),
            fireshotRate1
            ),timefire );
        waterBoss.shooting.StartShootingAfter(
            EnemyPatterns.PulsingBullets(
                GameManager.gameData.leafBullet2, earthdmg1, earthimage.transform, earthspeed1, earthshotRate1, earthnumber1,null), timeearth);
        waterBoss.shooting.StartShooting(WaterCirclePattern1(new Vector2(circledist1, 0), waterBoss, circlespeed1, true));
        waterBoss.shooting.StartShooting(WaterCirclePattern1(new Vector2(-circledist1, 0), waterBoss, circlespeed1, false));
        waterBoss.shooting.StartShooting(MoveInCircle());
        waterBoss.bosshealth.OnLifeDepleted += EndPhase1;
       
    }
    public void EndPhase1() {
        waterBoss.bosshealth.OnLifeDepleted -= EndPhase1;
        EndPhase();
        SwitchToImage(DamageType.Water);
        Invoke("StartPhase2", endPhaseTransition);
    }

    public void StartPhase2()
    {

        fireimage.MoveTo(new Vector2(4.5f, 4.5f), movespeed);
        earthimage.MoveTo(new Vector2(-4.5f, 4.5f), movespeed);
        waterimage.MoveTo(startingPoint2, movespeed);
        SpellCardUI(namesOfSpellCards[0], waterportrait);
        Invoke("Phase2", spellCardTransition);
    }
    public void Phase2() {
        SwitchToBoss(DamageType.Water);
        waterBoss.shooting.StartShooting(EnemyPatterns.PulsingBulletsRandomAngle(
            GameManager.gameData.bigBullet.GetItem(DamageType.Water), harderdmg2, waterBoss.transform, harderspeed2, harderpulserate2, hardercount2, null)
            );
        waterBoss.shooting.StartShooting(Functions.RepeatAction(          
            ()=> {
                float x = UnityEngine.Random.Range(-4.1f, 4.1f);
                float speed = UnityEngine.Random.Range(snowspeedmin, snowspeedmax);
                SnowflakeDroppingBullet(x, speed, snowflakerandomfactor2);               
                }, spawnRatesnow));
        waterBoss.shooting.StartShooting(Functions.RepeatCustomAction(
            i =>
            {
                float angle = i * -anglepershot2 + UnityEngine.Random.Range(-spreadrange2, spreadrange2)-90;
                warpingIcicle(iciclespeed2, angle, Functions.RandomLocation(waterBoss.transform.position, spawnradius2));
            }, shotrateicicle2));
        waterBoss.bosshealth.OnLifeDepleted += EndPhase2;
    }

    public void EndPhase2() {
        waterBoss.bosshealth.OnLifeDepleted -= EndPhase2;
        SwitchToImage(DamageType.Water);
        EndPhase();
        float time1 = fireimage.MoveTo(new Vector2(x3, y3), movespeed/3);
        float time2=earthimage.MoveTo(startingPoint3, movespeed/1.5f);
        float time3= waterimage.MoveTo(new Vector2(-x3, y3), movespeed/3);
        Invoke("Phase3", Math.Max( time2, time3));
    }
    public void Phase3() {
        earthBoss = Instantiate(earthprefab, earthimage.transform.position, Quaternion.identity);
        earthBoss.SetUpBossHealthbar(bossUI.earthHealthBar);
        Stage5BossPointer earthPointer = Instantiate(pointer, new Vector2(0, -4.2f), Quaternion.identity);
        earthPointer.trackingBoss = earthBoss;
        earthPointer.sprite.color = green;
        SwitchToBoss(DamageType.Earth);
        earthBoss.shooting.StartShooting(Functions.RepeatAction(
            () => Patterns.RingAroundBossAimAtPlayer(GameManager.gameData.icicle, bulletdmg3nonmain, waterimage.transform.position, radius3, bulletspeed3nonmain, ringnumber3,null),
            bulletpulserate3));
        earthBoss.shooting.StartShootingAfter(Functions.RepeatAction(
            () => Patterns.RingAroundBossAimAtPlayer(GameManager.gameData.fireBall, bulletdmg3nonmain, fireimage.transform.position, radius3, bulletspeed3nonmain, ringnumber3,null),
            bulletpulserate3),bulletpulserate3/2);
        earthBoss.shooting.StartShooting(MoveLeftAndRight(fireimage, true, x3, y3, bossmovespeed3));
        earthBoss.shooting.StartShooting(MoveLeftAndRight(waterimage, false, x3, y3, bossmovespeed3));
        earthBoss.shooting.StartShooting(Pattern3Main());
        earthBoss.bosshealth.OnLifeDepleted += EndPhase3;
    }
    public void EndPhase3() {
        earthBoss.bosshealth.OnLifeDepleted -= EndPhase3;
        SwitchToImage(DamageType.Earth);
        EndPhase();
       
        Invoke("StartPhase4", endPhaseTransition);

    }
    public void StartPhase4() {
        float time1 = fireimage.MoveTo(new Vector2(4.5f, 4.5f), movespeed);
        float time3 = waterimage.MoveTo(new Vector2(-4.5f, 4.5f), movespeed);
        SpellCardUI(namesOfSpellCards[1], earthportrait);
        Invoke("Phase4", spellCardTransition);

    }
    public void Phase4() {
        SwitchToBoss(DamageType.Earth);
        earthBoss.shooting.StartShooting(Functions.RepeatCustomAction(
            i => {
                int r = i % 4;
                SummonRocks(r == 1 || r == 2, r == 2 || r == 3);
            }, pulseRate4));
        earthBoss.bosshealth.OnLifeDepleted += EndPhase4;
    }
    public void EndPhase4() {
        earthBoss.bosshealth.OnLifeDepleted -= EndPhase4;
        EndPhase();
        SwitchToImage(DamageType.Earth);
        float time1 = fireimage.MoveTo(startingPoint5, movespeed / 3);
        float time2 = earthimage.MoveTo(new Vector2(x5,y5), movespeed / 3);
        float time3 = waterimage.MoveTo(new Vector2(-x5, y5), movespeed / 1.5f);
        Invoke("Phase5", Math.Max(time1, time2));

    }
    public void Phase5() {
        fireBoss = Instantiate(fireprefab, fireimage.transform.position, Quaternion.identity);
        fireBoss.SetUpBossHealthbar(bossUI.fireHealthBar);
        Stage5BossPointer firePointer = Instantiate(pointer, new Vector2(0, -4.2f), Quaternion.identity);
        firePointer.trackingBoss = fireBoss;
        firePointer.sprite.color = red;
        SwitchToBoss(DamageType.Fire);

        SpinningMagicCircles(waterCircle, waterimage.transform, false, 0, GameManager.gameData.snowflake);
        SpinningMagicCircles(earthCircle, earthimage.transform, true, shotrate5nonmain/2, GameManager.gameData.leafBullet2);
        fireBoss.shooting.StartShooting(Functions.RepeatAction(
            ()=> {
                Vector2 pos = Functions.RandomLocation(fireBoss.transform.position, radiusmain5);
                ShootChangingBullet2(pos, bigduration5, UnityEngine.Random.Range(0f, 360f));

            },shotrate5));
        fireBoss.shooting.StartShooting(Functions.RepeatAction(
            () =>
            {
                float currentx = fireBoss.transform.position.x;
                float x = currentx + (currentx < boundaryx - maxstepx ? currentx > -boundaryx + maxstepx ? UnityEngine.Random.Range(-maxstepx, maxstepx) :
                UnityEngine.Random.Range(0, maxstepx) : UnityEngine.Random.Range(-maxstepx, 0));
                fireBoss.movement.MoveTo(new Vector2(x, fireBoss.transform.position.y), bossmovespeed5);
            }, movementpulseduration5));
        fireBoss.bosshealth.OnLifeDepleted += EndPhase5;
    }
    public void EndPhase5() {
        fireBoss.bosshealth.OnLifeDepleted -= EndPhase5;
        EndPhase();
        SwitchToImage(DamageType.Fire);
        Invoke("StartPhase6", endPhaseTransition);
    }
    public void StartPhase6() {
        SpellCardUI(namesOfSpellCards[2],fireportrait);
        waterimage.MoveTo(new Vector2(4.5f, 4.5f), movespeed);
        earthimage.MoveTo(new Vector2(-4.5f, 4.5f), movespeed);
        fireimage.MoveTo(startingPoint6, movespeed);
        Invoke("Phase6", spellCardTransition);
           
    }
    public void Phase6() {
        SwitchToBoss(DamageType.Fire);
        Bullet wheel = GameManager.gameData.fireWheel;
        Action<bool, float> Shoot = (clockwise, angle) =>
        {
            Bullet bul = Patterns.ShootStraight(wheel, dmg6wheel, fireBoss.transform.position, angle, wheelspeed6,null);
            bul.orientation.StartRotating(clockwise ? -angularvel6 : angularvel6);
            bul.movement.destroyBoundary = 6f;
            bul.transform.localScale *= wheelsize;
        };
        float time1 = fireBoss.movement.MoveTo(right6, movespeed6);
        fireBoss.shooting.StartShootingAfter(Functions.RepeatCustomAction(
            i => {
                float angle = Functions.AimAtPlayer(fireBoss.transform);
                bool clockwise = i % 2 == 0;

                Shoot(clockwise, angle);
                clockwise = !clockwise;
                for (int j = 1; j <= wheellines6; j++) {
                    Shoot(clockwise, angle + j * spreadangle6);
                    Shoot(clockwise, angle - j * spreadangle6);
                    clockwise =!clockwise;
                }
                fireBoss.movement.MoveTo(i % 2 == 0 ? left6 : right6, movespeed6);
            }, shotrate6wheel),time1);
        
        fireBoss.shooting.StartShootingAfter(SubPattern6(), time1 + shotrate6wheel/3);
        fireBoss.bosshealth.OnLifeDepleted += EndPhase6;
    }
    public void EndPhase6() {
        Instantiate(GameManager.gameData.defaultBombDrop, fireBoss.transform.position, Quaternion.identity);
        fireBoss.bosshealth.OnLifeDepleted -= EndPhase6;
        EndPhase();
        SwitchToImage(DamageType.Fire);
        fireimage.MoveTo(startingPoint7 + new Vector2(movementradius7, 0), movespeed/1.5f);
        waterimage.MoveTo(startingPoint7 - new Vector2(movementradius7, 0), movespeed/1.5f);
        earthimage.MoveTo(startingPoint7, movespeed);
        Invoke("StartPhase7", endPhaseTransition);
    }

    public void StartPhase7() {
        SpellCardUI(namesOfSpellCards[3], combineportrait);
        Invoke("Phase7", spellCardTransition);
    }
    public void Phase7() {
        
        SwitchToBoss(DamageType.Water);
        SwitchToBoss(DamageType.Earth);
        SwitchToBoss(DamageType.Fire);
        waterBoss.transform.position = startingPoint7 - new Vector2(movementradius7, 0);
        earthimage.transform.position = startingPoint7;
        fireimage.transform.position = startingPoint7 + new Vector2(movementradius7, 0);
        MoveInCircle(waterBoss.movement, -90);
        MoveInCircle(earthBoss.movement, 0);
        MoveInCircle(fireBoss.movement, 90);
        waterBoss.bosshealth.OnLifeDepleted += OneDownWater;
        earthBoss.shooting.StartShooting(EarthPillar());
        fireBoss.shooting.StartShooting(FirePillar());
        earthBoss.bosshealth.OnLifeDepleted += OneDownEarth;
        fireBoss.bosshealth.OnLifeDepleted += OneDownFire;
        waterBoss.shooting.StartShooting(Functions.RepeatCustomAction(
            i =>
            {
                Patterns.SpirallingOutwardsRing(GameManager.gameData.snowflake, snowflakedmg7, waterBoss.transform.position, snowflakeradialvel7,
                   (i % 2 == 0 ? -1 : 1) * snowflakeangularvel7, numberofsnowflakes7, 0,null);
               
            }, snowflakeshotrate7));
    }
    public void OneDownWater() {
        donebools7[0] = true;
        waterBoss.shooting.StopAllCoroutines();
        SwitchToImage(DamageType.Water);
        waterimage.GetComponent<Movement>().MoveTo(new Vector2(0, 5f), movespeed / 3);
        waterBoss.bosshealth.OnLifeDepleted -= OneDownWater;
        GoNext();
        
    }
    public void OneDownEarth()
    {
        donebools7[1] = true;
        earthBoss.shooting.StopAllCoroutines();
        SwitchToImage(DamageType.Earth);
        earthimage.GetComponent<Movement>().MoveTo(new Vector2(0, 5f), movespeed / 3);
        earthBoss.bosshealth.OnLifeDepleted -= OneDownEarth;
        GoNext();
    }
    public void OneDownFire()
    {
        donebools7[2] = true;
        fireBoss.shooting.StopAllCoroutines();
        SwitchToImage(DamageType.Fire);
        fireimage.GetComponent<Movement>().MoveTo(new Vector2(0, 5f), movespeed / 3);
        fireBoss.bosshealth.OnLifeDepleted -= OneDownFire;
        GoNext();
    }
    public void GoNext() {
        PlayLifeDepletedSound();
        foreach (bool bl in donebools7)
        {
            if (!bl)
            {
                return;
            }
        }
        EndPhase7();
    }
    public void EndPhase7() {
        EndPhase();
        waterimage.MoveTo(startingPoint8 + new Polar(radius8, 90).rect, movespeed/1.5f);
        earthimage.MoveTo(startingPoint8 + new Polar(radius8, -30).rect, movespeed/1.5f);
        fireimage.MoveTo(startingPoint8 + new Polar(radius8, -150).rect, movespeed/1.5f);
        Invoke("StartPhase8", endPhaseTransition);
    }
    public void StartPhase8() {
        SpellCardUI(namesOfSpellCards[4], combineportrait);
        Invoke("Phase8", spellCardTransition);
    }
    public void Phase8() {
        SwitchToBoss(DamageType.Water);
        SwitchToBoss(DamageType.Earth);
        SwitchToBoss(DamageType.Fire);
        Pattern8Movement(waterBoss.movement, DamageType.Water, 90);
        Pattern8Movement(earthBoss.movement, DamageType.Earth, -30);
        Pattern8Movement(fireBoss.movement, DamageType.Fire, -150);
        waterBoss.bosshealth.OnDeath += OneDownWater8;
        earthBoss.bosshealth.OnDeath += OneDownEarth8;
        fireBoss.bosshealth.OnDeath += OneDownFire8;
        fireBoss.shooting.StartShooting(Functions.RepeatAction(()=> ShootFire8(fireBoss.transform), shotRate8));
        waterBoss.shooting.StartShooting(Functions.RepeatCustomAction(
            i => ShootWater8(waterBoss.transform, i % 2 == 0), shotRate8));
        earthBoss.shooting.StartShooting(Functions.RepeatAction(() => ShootEarth8(earthBoss.transform), shotRate8));
    }
    public void OneDownWater8()
    {
        donebools8[0] = true;
        waterBoss.shooting.StopAllCoroutines();
        SwitchToImage(DamageType.Water);
        waterimage.GetComponent<Movement>().MoveTo(new Vector2(0, 5f), movespeed / 3);
        waterBoss.bosshealth.OnLifeDepleted -= OneDownWater8;
        GoNext8();

    }
    public void OneDownEarth8()
    {
        donebools8[1] = true;
        earthBoss.shooting.StopAllCoroutines();
        SwitchToImage(DamageType.Earth);
        earthimage.GetComponent<Movement>().MoveTo(new Vector2(0, 5f), movespeed / 3);
        earthBoss.bosshealth.OnLifeDepleted -= OneDownEarth8;
        GoNext8();
    }
    public void OneDownFire8()
    {
        donebools8[2] = true;
        fireBoss.shooting.StopAllCoroutines();
        SwitchToImage(DamageType.Fire);
        fireimage.GetComponent<Movement>().MoveTo(new Vector2(0, 5f), movespeed / 3);
        fireBoss.bosshealth.OnLifeDepleted -= OneDownFire8;
        GoNext8();
    }
    public void GoNext8()
    {
        foreach (bool bl in donebools8)
        {
            if (!bl)
            {
                return;
            }
        }
        EndPhase8();
    }
    void EndPhase8() {
        EndPhase();
        GameManager.CollectEverything();
        Invoke("StartDialogue", 1f);
    }
    void StartDialogue() {
        StartCoroutine(DialogueManager.StartDialogue(endDialogue, NextStage));
    }
    public Bullet ShootWater8(Transform origin, bool clockwise) {
        float angle = Functions.AimAt(origin.position, startingPoint8);
        float time = radius8 / shotspeed8;
        Bullet water = Patterns.ShootStraight(GameManager.gameData.icicle, shot8dmg, origin.position, angle, shotspeed8,null);

        ActionTrigger<Movement> trigger = new ActionTrigger<Movement>(movement => movement.time > time);
        trigger.OnTriggerEvent += movement => 
        {
            Patterns.SpirallingOutwardsRing(GameManager.gameData.snowflake, snowdmg8, movement.transform.position, snowradialvel8, (clockwise ? -1 : 1) * snowangularvel8,
                snownumber8, angle,null);
            movement.GetComponent<Bullet>().Deactivate();
        };
       water.movement.triggers.Add(trigger);
        return water;
    }
    public Bullet ShootFire8(Transform origin) {
        float angle = Functions.AimAt(origin.position, startingPoint8);
        float time = radius8 / shotspeed8;
        Bullet fire = Patterns.ShootStraight(GameManager.gameData.fireBall, shot8dmg, origin.position, angle, shotspeed8,null);

        ActionTrigger<Movement> trigger = new ActionTrigger<Movement>(movement => movement.time > time);
        trigger.OnTriggerEvent += movement =>
        {
            Patterns.ShootMultipleStraightBullet(GameManager.gameData.fireShortLaser, firedmg8,
                movement.transform.position, firespeed8, angle, firespread8, firenumber8,null);
            movement.GetComponent<Bullet>().Deactivate();
        };
        fire.movement.triggers.Add(trigger);
        return fire;
    }
    public Bullet ShootEarth8(Transform origin)
    {
        float angle = Functions.AimAt(origin.position, startingPoint8);
        float time = radius8 / shotspeed8;
        Bullet earth = Patterns.ShootStraight(GameManager.gameData.rockBullet, shot8dmg, origin.position, angle, shotspeed8,null);

        ActionTrigger<Movement> trigger = new ActionTrigger<Movement>(movement => movement.time > time);
        Bullet leaf = GameManager.gameData.leafBullet2;
        trigger.OnTriggerEvent += movement =>
        {
            for (int i = 0; i < leaflines8; i++) {
                List<Bullet> buls = Patterns.ShootMultipleStraightBullet(leaf, leafdmg8, movement.transform.position, 
                    minspeed8leaf + i * speeddiff8leaf, angle, leafspreadangle8, leaflines8,null);
                Functions.Scale(buls, leafsize8);
            }
            movement.GetComponent<Bullet>().Deactivate();
        };
        earth.movement.triggers.Add(trigger);
        return earth;
    }
    public void Pattern8Movement(Movement move, DamageType type, float angle) {
        Vector2 start = startingPoint8 + new Polar(radius8, angle).rect;
        move.transform.position = start;
        move.ResetTimer();
        move.SetPolarPath(t => new Polar(radius8, angle + angularvel8 * -t));
        Bullet bul = Instantiate(GameManager.gameData.stage5lines.GetItem(type), start,Quaternion.Euler(0,0, angle - 150));
        bul.SetDamage(laserdmg8);
        bul.transform.localScale = new Vector2(Mathf.Sqrt(3)*radius8 / 3f, thickness8);
        bul.orientation.angle = angle - 150;
        bul.transform.parent = move.transform;
        bul.orientation.StartRotating(-angularvel8);
        
    }

    IEnumerator EarthPillar() {
        while (earthBoss) {
            float y = UnityEngine.Random.Range(earthpillarspawnmin, earthpillarspawnmax);
            float x = -4f;
            for (int i = 0; i < numberOfEarthpillars; i++) {
                Bullet pillar = Patterns.ShootCustomBullet(GameManager.gameData.earthPillar, earthpillardmg, new Vector2(x, y)
                    , t => new Vector2(earthPillarmovementX, -earthPillarmovementY), MovementMode.Velocity,null);
                y -= earthPillarspacingY;
                x -= earthpillarspacingX;
                pillar.orientation.SetFixedOrientation(0);
                pillar.transform.localScale = new Vector2(1.3f, 0.6f);
                pillar.movement.destroyBoundary = 20f;
            }
            yield return new WaitForSeconds(earthpillarpulseRate);
        }
    }
    IEnumerator FirePillar()
    {
        while (fireBoss)
        {
            float x = UnityEngine.Random.Range(firepillarspawnmin, firepillarspawnmax);
            float y = 4f;
            for (int i = 0; i < numberOffirepillars; i++)
            {
                Bullet pillar = Patterns.ShootCustomBullet(GameManager.gameData.firePillar, firepillardmg, new Vector2(x, y)
                    , t => new Vector2(firePillarmovementX, -firePillarmovementY), MovementMode.Velocity,null);
                y += firepillarspacingY;
                x += firepillarspacingX;
                pillar.orientation.SetFixedOrientation(-90);
                pillar.transform.localScale = new Vector2(1.5f, 0.3f);
                pillar.movement.destroyBoundary = 20f;
            }
            yield return new WaitForSeconds(firepillarpulseRate);
        }
    }
    void MoveInCircle(Movement movement, float originAngle) {
        movement.ResetTimer();
        movement.SetCustomPath(t => new Vector2(movementradius7 * Mathf.Sin(2*Mathf.PI*freq7 * t + Mathf.Deg2Rad * originAngle)
            , movementradius7 * Mathf.Sin(2 * Mathf.PI * freq7 * t + Mathf.Deg2Rad * originAngle) * Mathf.Cos(2 * Mathf.PI * freq7 * t + Mathf.Deg2Rad * originAngle)));
        
    }
    IEnumerator SubPattern6() {
        Action<Bullet,int, float> Shoot = (bul,number, dmg) => Patterns.RingOfBullets(bul, dmg, fireBoss.transform.position, number, 0, firespeed6,null);
        while (fireBoss&fireBoss.enabled) {
            if (!harder)
            {
                Shoot(GameManager.gameData.bigBullet.GetItem(DamageType.Fire), bignumber, bigdmg6);
            }
            else {
                Patterns.SpirallingOutwardsRing(GameManager.gameData.bigBullet.GetItem(DamageType.Fire), bigdmg6, fireBoss.transform.position, firespeed6, harderangularvel6, bignumber,
                    0, null);
                yield return new WaitForSeconds(fireshotrate6);
                Patterns.SpirallingOutwardsRing(GameManager.gameData.bigBullet.GetItem(DamageType.Fire), bigdmg6, fireBoss.transform.position, firespeed6, -harderangularvel6, bignumber,
                    0, null);
            }
            yield return new WaitForSeconds(fireshotrate6);
            for (int i = 0; i < firenumbersperpulse6; i++) {
                Shoot(GameManager.gameData.fireBullet, numberoffirebullets, firedmg6);
                yield return new WaitForSeconds(fireshotrate6);
            }
           
             yield return new WaitForSeconds(firepulserate6 - fireshotrate6 * ((harder?2:1) + firenumbersperpulse6));
        }
    }
    
    List<Bullet> SummonRocks(bool horizontal, bool up) {

        List<Bullet> buls = new List<Bullet>();
        for (float i = -3.9f + UnityEngine.Random.Range(0, randomfactor4); i < 3.9f; i += spacing4) {
            if (horizontal)
            {
                buls.Add(SummonRockHorizontal(i, up));
            }
            else {
                buls.Add(SummonRockVertical(i, up));
            }
        }
        return buls;
    }
    Bullet SummonRockVertical(float x, bool up) {
        Bullet star = GameManager.gameData.starBullet.GetItem(DamageType.Earth);
        Bullet treerock = Patterns.ShootStraight(GameManager.gameData.treeRockBullet, treerockdmg4, new Vector2(x, up? -4.2f:4.2f), up?90:-90, treerockspeed4,null);
        treerock.GetComponent<Shooting>().StartShooting(Functions.RepeatCustomAction(
            i => {
                Vector2 pos = treerock.transform.position;
                float angle = (up?(pos.y+4f):(4f - pos.y)) / unitspercircle4 * 360f;
                Bullet bul = Patterns.ShootCustomBullet(star, dmg4star, pos,
             t => t < delay4 - i * shotRate4 + time4 && t > delay4 - i * shotRate4 ? new Polar(acceleration4, angle).rect : new Vector2(0, 0), MovementMode.Acceleration,null);
                bul.transform.localScale *= size4;
                bul.orientation.SetFixedOrientation(0);
           } , shotRate4));
        return treerock;
    }
    Bullet SummonRockHorizontal(float y, bool left) {
        Bullet star = GameManager.gameData.starBullet.GetItem(DamageType.Earth);
        Bullet treerock = Patterns.ShootStraight(GameManager.gameData.treeRockBullet, treerockdmg4, new Vector2(left ? -4.2f : 4.2f, y), left ? 0 : 180, treerockspeed4,null);
        treerock.GetComponent<Shooting>().StartShooting(Functions.RepeatCustomAction(
            i => {
                Vector2 pos = treerock.transform.position;
                float angle = (left ? (pos.x + 4f) : (4f - pos.x)) / unitspercircle4 * 360f;
                Bullet bul = Patterns.ShootCustomBullet(star, dmg4star, pos,
             t => t < delay4 - i * shotRate4 + time4 && t > delay4 - i * shotRate4 ? new Polar(acceleration4, angle).rect : new Vector2(0, 0), MovementMode.Acceleration,null);
                bul.transform.localScale *= size4;
                bul.orientation.SetFixedOrientation(0);
            }, shotRate4));
        return treerock;

    }
    IEnumerator Pattern3Main() {
        while (earthBoss) {
            for (int i = 0; earthBoss && i < pulseduration3 / shotrate3 + 1; i++)
            {
                ShootChangingBullet1(earthBoss.transform.position, bigduration3, -angularvel3 * shotrate3 * i);
                yield return new WaitForSeconds(shotrate3);
            }
                yield return new WaitForSeconds(delay3);
            for (int i = 0; earthBoss && i < pulseduration3 / shotrate3 + 1; i++)
            {
                ShootChangingBullet1(earthBoss.transform.position, bigduration3, 180 + angularvel3 * shotrate3 * i);
                yield return new WaitForSeconds(shotrate3);
            }
            yield return new WaitForSeconds(delay3);
        }
    }

    List<Bullet> SpinningMagicCircles(Bullet magicCircle, Transform origin, bool clockwise, float delay, Bullet bullet)
    {
        List<Bullet> buls = new List<Bullet>();
        float circleangularvelocity = clockwise ? -angularvel5 : angularvel5;
        for (int i = 0; i < magiccirclenumber5; i++)
        {
            float angle = i * 360 / magiccirclenumber5;
            Bullet circle = GameManager.bulletpools.SpawnBullet(magicCircle, (Vector2)(origin.position) + new Polar(radius5, angle).rect);
            circle.movement.SetPolarPath(t => new Polar(radius5, angle + circleangularvelocity * t));
            circle.transform.localScale *= 0.5f;
            circle.GetComponent<Shooting>().StartShootingAfter(Functions.RepeatAction(
                () => Patterns.RingOfBullets(bullet, bulletdmg5nonmain, circle.transform.position, bulletnumber5nonmain,
                Functions.AimAt(circle.transform.position, origin.position), bulletspeed5nonmain,null), shotrate5nonmain), delay);
            buls.Add(circle);

        }
        return buls;
    }
    Bullet ShootChangingBullet2(Vector2 origin, float duration, float angle)
    {
        Bullet bul = Patterns.BurstShoot(GameManager.gameData.starBullet.GetItem(DamageType.Fire), bigdmg5, origin, angle, bigspeed5 * 5, bigspeed5, 0.5f,null);
        ActionTrigger<Movement> trigger = new ActionTrigger<Movement>(movement => movement.time > duration);
        trigger.OnTriggerEvent += movement =>
        {
              Bullet bullet = Patterns.ShootStraight(GameManager.gameData.fireShortLaser, bulletdmg5main, movement.transform.position, 
                Functions.AimAtPlayer(movement.transform), bulletspeed5main,null);
            bullet.transform.localScale *= 0.7f;
            movement.GetComponent<Bullet>().Deactivate();
        };
        bul.movement.triggers.Add(trigger);

        return bul;

    }

    Bullet ShootChangingBullet1(Vector2 origin, float duration, float angle) {
        Bullet bul = Patterns.BurstShoot(GameManager.gameData.rockBullet, bigdmg3, origin, angle,bigspeed3*5, bigspeed3, 0.3f,null);
        bul.transform.localScale *= 1.5f;
        ActionTrigger<Movement> trigger = new ActionTrigger<Movement>(movement => movement.time > duration);
        trigger.OnTriggerEvent += movement =>
        {
            List<Bullet> buls = Patterns.BulletSpreadingOut(GameManager.gameData.leafBullet3, bulletdmg3main, movement.transform.position, bulletspeed3main, bulletspeeddiff3
                , angle, number3main,null);
            Functions.Scale(buls, 0.8f);
            movement.GetComponent<Bullet>().Deactivate();
        };
        bul.movement.triggers.Add(trigger);

        return bul;    
        
    }
    IEnumerator MoveLeftAndRight(Movement move, bool left, float x, float y, float speed) {
        bool movetoleft = left;
        
        while (move) {
            float time = move.MoveTo(new Vector2(movetoleft ? -x : x, y), speed);
            movetoleft = !movetoleft;
            yield return new WaitForSeconds(time);
        }
    }
    IEnumerator MoveInCircle() {
        float delay = waterBoss.movement.MoveTo(waterorigin1 - new Vector2(0, radiusmovement1), movespeed);
        yield return new WaitForSeconds(delay + 0.2f);
        Debug.Log(waterBoss.gameObject.activeInHierarchy);
        if (waterBoss.gameObject.activeInHierarchy)
        {

            waterBoss.movement.ResetTimer();
            waterBoss.movement.StartMoving();
            waterBoss.movement.SetPolarPath(t => new Polar(radiusmovement1, movespeed1 / radiusmovement1 * t * Mathf.Rad2Deg -90));
        }

    }
    IEnumerator WaterCirclePattern1(Vector2 location, Stage5Boss boss, float speed, bool left) {
        Bullet circle = GameManager.bulletpools.SpawnBullet(waterCircle, boss.transform.position);
        circle.orientation.StartRotating(left ? 180 : -180);
        float time = circle.movement.MoveTo((Vector2)boss.transform.position + location, speed);
        circle.transform.parent = boss.transform;
        yield return new WaitForSeconds(time);
        if (circle&&circle.gameObject.activeInHierarchy) {
            EnemyPatterns.StartFanningPattern(GameManager.gameData.ellipseBullet.GetItem(DamageType.Water), waterdmg1, circle.GetComponent<Shooting>(),
                waterspeed1, (left ? -1 : 1) * waterangularvel1, left ? 0 : 180, 1, watershotrate1, waternumber1, waterspeeddiff,null);
                
        
        }

    }
    Bullet SnowflakeDroppingBullet(float x, float speed, float accelerationFactor)
    {
        Bullet bul = GameManager.bulletpools.SpawnBullet(GameManager.gameData.snowflake, new Vector2(x, 4.1f));
        bul.SetDamage(snowflakedmg2);
        bul.movement.SetAcceleration(new Vector2(0, -speed), t => new Vector2(UnityEngine.Random.Range(-accelerationFactor, accelerationFactor), 0));
        bul.orientation.StartRotating(snowflakespinningVel2, 0);
        return bul;
    }
    Bullet warpingIcicle(float speed, float angle, Vector2 origin) {
        Bullet bul = Patterns.ShootStraight(GameManager.gameData.icicle, icicledmg2, origin, angle, speed,null);
        ActionTrigger<Movement> trigger = new ActionTrigger<Movement>(movement => !Functions.WithinBounds(movement.transform.position, 4.1f));
        trigger.OnTriggerEvent += movement =>
        {
            Vector2 pos = movement.transform.position;
            if (pos.x >= 4.1f || pos.x <= -4.1f)
            {
                movement.transform.position = new Vector2(-pos.x, pos.y);
            }
            else
            {
                movement.transform.position = new Vector2(pos.x, -pos.y);

            }
            movement.ResetTriggers();
        };
        bul.movement.triggers.Add(trigger);
  
        return bul;

    }
    public void SetUp() {
        GameObject obj = GameObject.Find("gamecanvas/stats UI");
        bossUI = Instantiate(ui, obj.transform);
        bossUI.transform.localPosition = new Vector2(0,273);
    }
    public override void EndPhase()
    {
        Action<Stage5Boss> StopAll = boss =>
        {
            if (boss&&boss.isActiveAndEnabled)
            {
                boss.shooting.StopAllCoroutines();
                boss.movement.StopMoving();
                boss.enemyAudio.StopAllCoroutines();
            }
        };
        try
        {
            PlayLifeDepletedSound();
            StopAll(waterBoss);
            StopAll(earthBoss);
            StopAll(fireBoss);
            GameManager.DestoryAllEnemyBullets();
            GameManager.DestroyAllNonBossEnemy(true);
            if (currentUI)
            { Destroy(currentUI.gameObject); }
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }

    }

    public virtual void SpellCardUI(string name, Sprite image)
    {
        currentUI = Instantiate(GameManager.gameData.spellcardUI);
        
        currentUI.SetImage(image);
        currentUI.PlaySFX();
        currentUI.SetText(name.Replace("\\n", "\n"));

    }
    protected virtual void SwitchToImage(DamageType type)
    {
        GameObject image = (type == DamageType.Water ? waterimage : type==DamageType.Earth ? earthimage : fireimage).gameObject;
        GameObject actual = (type == DamageType.Water ? waterBoss : type == DamageType.Earth ? earthBoss : fireBoss).gameObject;

        image.GetComponent<SpriteRenderer>().enabled = true;
        try
        {
            image.transform.position = actual.transform.position;
            actual.SetActive(false);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }


    }

    protected virtual void SwitchToBoss(DamageType type)
    {
        GameObject image = (type == DamageType.Water ? waterimage : type == DamageType.Earth ? earthimage : fireimage).gameObject;
        GameObject actual = (type == DamageType.Water ? waterBoss : type == DamageType.Earth ? earthBoss : fireBoss).gameObject;

        image.GetComponent<SpriteRenderer>().enabled = false;
        try
        {
            actual.SetActive(true);
            actual.transform.position = image.transform.position;
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }

    }

}
