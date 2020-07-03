using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
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
    Stage5EndBossUI bossUI;
    public override void SpawnWave() {
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
        earthimage.MoveTo(new Vector2(-3.5f, 3.5f), movespeed);
        fireimage.MoveTo(new Vector2(3.5f, 3.5f), movespeed);
        //waterBoss.start
    
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
            if (boss.isActiveAndEnabled)
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
            actual.gameObject.SetActive(false);
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
            actual.gameObject.SetActive(true);
            actual.transform.position = image.transform.position;
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }

    }

}
