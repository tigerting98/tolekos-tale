using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Policy;
using UnityEditor.Experimental.Rendering;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Stage5EndBoss : EnemyBossWave
{
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
        float time = waterimage.MoveTo(waterspawnlocation, movespeed);
        yield return new WaitForSeconds(time);
        StartCoroutine(DialogueManager.StartDialogue(preFightDialogue1, ()=> StartCoroutine(preFight2())));

    }
    public IEnumerator preFight2()
    {
        earthimage = Instantiate(earthimageprefab, new Vector2(0, 4.1f), Quaternion.identity);
        float time1 = earthimage.MoveTo(earthspawnlocation, movespeed);
        fireimage = Instantiate(fireimageprefab, new Vector2(4.1f, 4.1f), Quaternion.identity);
        float time2 = fireimage.MoveTo(firespawnlocation, movespeed);
        yield return new WaitForSeconds(Math.Max(time1, time2));
        StartCoroutine(DialogueManager.StartDialogue(preFightDialogue2, Phase1));

    }
    public void Phase1() {
        waterBoss = Instantiate(waterprefab, waterimage.transform.position, Quaternion.identity);
        waterBoss.SetUpBossHealthbar(bossUI.waterHealthBar);
        SwitchToBoss(DamageType.Water);
        float timeearth = earthimage.MoveTo(new Vector2(-3.5f, 3.5f), movespeed);
        float timefire = fireimage.MoveTo(new Vector2(3.5f, 3.5f), movespeed);
        waterBoss.shooting.StartShootingAfter(Functions.RepeatAction(
            ()=>Patterns.ShootStraight(GameManager.gameData.fireBall, firedmg1, Functions.RandomLocation(fireimage.transform.position, fireradius1),
            Functions.AimAt(fireimage.transform.position, GameManager.playerPosition) + UnityEngine.Random.Range(-fireanglespread1, fireanglespread1), fireSpeed1),
            fireshotRate1
            ),timefire );
        waterBoss.shooting.StartShootingAfter(
            EnemyPatterns.PulsingBullets(
                GameManager.gameData.leafBullet2, earthdmg1, earthimage.transform, earthspeed1, earthshotRate1, earthnumber1), timeearth);
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
        SpellCardUI(namesOfSpellCards[0]);
        Invoke("Phase2", spellCardTransition);
    }
    public void Phase2() {
        SwitchToBoss(DamageType.Water);
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
        float time = circle.movement.MoveTo((Vector2)boss.transform.position + location, speed);
        circle.transform.parent = boss.transform;
        yield return new WaitForSeconds(time);
        if (circle) {
            EnemyPatterns.StartFanningPattern(GameManager.gameData.ellipseBullet.GetItem(DamageType.Water), waterdmg1, circle.GetComponent<Shooting>(),
                waterspeed1, (left ? -1 : 1) * waterangularvel1, left ? 0 : 180, 1, watershotrate1, waternumber1, waterspeeddiff);
                
        
        }
        
    }
    public void SetUp() {
        GameObject obj = GameObject.Find("Canvas");
        bossUI = Instantiate(ui, obj.transform);
        bossUI.transform.localPosition = new Vector2(622, 294);
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
