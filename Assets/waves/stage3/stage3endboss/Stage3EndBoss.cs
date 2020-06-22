using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Stage3EndBoss : EnemyBossWave
{
    // Start is called before the first frame update
    [SerializeField] GameObject earthCircle, key;
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
    IEnumerator PreFight1()
    {
        try
        {
            GameObject.Find("stage3quad").GetComponent<Animator>().SetTrigger("StopMoving");
        }
        catch (Exception ex) { Debug.Log(ex.ToString()); }
        yield return new WaitForSeconds(1f);
        Instantiate(key, new Vector2(0, 3.5f), Quaternion.identity);
        StartCoroutine(DialogueManager.StartDialogue(preFightDialogue1, () => StartCoroutine(PreFight2())));


    }
    IEnumerator PreFight2() {
        yield return new WaitForSeconds(1f);
        bossImage = Instantiate(image, new Vector2(-4.1f,4.1f), Quaternion.identity);
        float time = bossImage.GetComponent<Movement>().MoveTo(new Vector2(0, spawnLocationY), initialMoveSpeed);
        yield return new WaitForSeconds(time);
        Instantiate(slamEffect, new Vector2(0, spawnLocationY), Quaternion.identity);
        StartCoroutine(DialogueManager.StartDialogue(preFightDialogue2, Phase1));

    }

    void Phase1() {
        currentBoss = Instantiate(boss, new Vector2(0, spawnLocationY), Quaternion.identity);
        GameManager.currentBoss = currentBoss;
        ReflectingBullet(GameManager.gameData.leafBullet1, 10, new Vector2(0, spawnLocationY), 30, 2);
        SwitchToBoss();
    }

    void EndPhase1() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase1;
        EndPhase();
        Invoke("Phase2", 1f);
    }

    IEnumerator Pattern1() {
        float time = currentBoss.movement.MoveTo(new Vector2(-x1, y1), initialMoveSpeed);
        yield return new WaitForSeconds(time);
      

    }

    void Phase2() {
        SpellCardUI(namesOfSpellCards[0]);
        Invoke("StartPattern2", 2f);
        currentBoss.bosshealth.OnDeath += End;

    }

    void StartPattern2() {
        SwitchToBoss();
        float time = currentBoss.movement.MoveTo(new Vector2(0, y2), initialMoveSpeed);
    

    }
    void End() {
        EndPhase();
        Destroy(bossImage);
        OnDefeat?.Invoke();
        DestroyAfter(5);
    }


    Bullet ReflectingBullet(Bullet bul, float dmg, Vector2 origin, float initialAngle, float initialSpeed) {
        ActionTrigger<Movement> reflectOnBound = new ActionTrigger<Movement>(
        movement => !Functions.WithinBounds(movement.transform.position, 4f) && movement.transform.position.y > -4);
        reflectOnBound.OnTriggerEvent += movement =>
        {
            Vector2 pos = movement.transform.position;
            if (pos.y >= 4)
            {

                
                movement.transform.position = new Vector2(movement.transform.position.x, 3.99f);
                movement.graph = Movement.ReflectPathAboutX(movement.graph);
            }
            else
            {
                

                movement.graph = Movement.ReflectPathAboutY(movement.graph);
                movement.ResetTriggers();

            }
        };
        Bullet bullet = Patterns.ShootStraight(bul, dmg, origin, initialAngle, initialSpeed);
        bullet.movement.triggers.Add(reflectOnBound);
        return bullet;

    }

}
