using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5Midboss : EnemyBossWave
{
    [SerializeField] ParticleSystem spawnEffect;
    [SerializeField] GameObject image;
    [SerializeField] Vector2 spawnLocation;
    [SerializeField] Dialogue prefightDialogue;
    [Header("Pattern1")]
    [SerializeField] Bullet fanBullet;
    [SerializeField] float bulletSpawnerOffset;
    [SerializeField] float minStationaryTime = 1f, maxStationaryTime = 2f;
    [SerializeField] List<Vector2> trianglePath;
    [SerializeField] float moveSpeed1 = 3.5f;

    [Header("Pattern2")]
    [SerializeField] Bullet bookBullet;
    [SerializeField] Bullet pageBullet;

    [Header("Pattern3")]
    [SerializeField] Bullet groundLaser;


    IEnumerator PreFight() {
        // Destroy(Instantiate(spawnEffect, spawnLocation - new Vector2(0, 0.5f), Quaternion.Euler(-90,0,0)).gameObject, 5f);
        yield return new WaitForSeconds(0.5f);
        bossImage = Instantiate(image, spawnLocation, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        StartCoroutine(DialogueManager.StartDialogue(prefightDialogue, StartPhase1));

    }

    void StartPhase1() {
        currentBoss = Instantiate(boss, spawnLocation, Quaternion.identity);
        GameManager.currentBoss = currentBoss;
        SwitchToBoss();
        currentBoss.shooting.StartShooting(Pattern1());
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
        currentBoss.bosshealth.OnLifeDepleted  += EndPhase2;
    }

    void EndPhase2() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase2;
        EndPhase();
        Invoke("StartPhase3", spellCardTransition);
    }

    void StartPhase3() {
        SpellCardUI(namesOfSpellCards[1]);
        Invoke("Phase3", spellCardTransition);
    }

    void Phase3() {
        SwitchToBoss();
        currentBoss.GetComponent<BulletOrientation>().Reset();
        currentBoss.transform.rotation = Quaternion.identity;
        currentBoss.shooting.StartCoroutine(Pattern2());
        currentBoss.bosshealth.OnLifeDepleted  += EndPhase2;
    }

    IEnumerator Pattern1()
    {
        yield return new WaitForSeconds(1f);
    }

    IEnumerator Pattern2()
    {
        yield return new WaitForSeconds(1f);
    }

    IEnumerator Pattern3()
    {
        yield return new WaitForSeconds(1f);
    }

    IEnumerator MoveInTriangle() 
    {
        int index = 0;
        while (true) 
        {
            float stationaryTime = Random.Range(minStationaryTime, maxStationaryTime);
            yield return new WaitForSeconds(stationaryTime);
            float timeToMove = currentBoss.movement.MoveTo(trianglePath[index], moveSpeed1);
            yield return new WaitForSeconds(timeToMove);
            if (index == trianglePath.Count - 1) {
                index = 0;
            } else {
                index ++;
            }
        }
    }

    void End()
    {
        EndPhase();
        Destroy(bossImage);
        OnDefeat?.Invoke();
        DestroyAfter(5);
    }
}
