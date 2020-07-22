
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class Stage1EndBoss : EnemyBossWave
{
    [SerializeField] Movement background;
    [Header("Dialogues")]
    [SerializeField] Dialogue preBossFight;
    [SerializeField] Dialogue midFightDialogue;
    [SerializeField] Dialogue endFightDialogue;
    [SerializeField] Dialogue preBossFight2;
    [SerializeField] bool harder = false;
    [Header("Pattern1")]
    [SerializeField] int number1;
    [SerializeField] float speed1, shotrate1, dmg1ball;
    [SerializeField]
    int pattern1Number = 5, numberOfCone = 5;
    [SerializeField] float pattern1PulseRate = 1f, pattern1SpawnRate = 0.05f, pattern1Spacing = 0.05f, pattern1Speed = 3f, dmg1 =100;


    [Header("Pattern2")]
    [SerializeField] float arrowSpawnTimeMax = 0.05f, arrowSpawnTimeMin = 0.01f;
    [SerializeField] float maxSpeed = 5f, minSpeed = 2f;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float maxY = 3.5f, minY = 0.5f;
    [SerializeField] float dmg2 = 100f;
    [Header("Pattern2 Homing Behavior")]
    [SerializeField] float arrowSpeed = 1.5f;
    [SerializeField] float shotRate = 1f;

    [Header("Pattern3 Bullet Behavior")]
    [SerializeField] Bullet explodingBullet;
    [SerializeField] int numberOfBulletsPattern3 = 30;
    [SerializeField] float initialSpeedPattern3 = 10f, maxRadiusPattern3 = 1f, shortDelayPattern3 = 0.2f, finalSpeedPattern3 = 2f, dmg3 = 100f;
    [SerializeField] float pausetime = 2f;
    [SerializeField] Bullet explosion, punch;
    [SerializeField] float punchSpeed = 2f;
    [SerializeField] float punchdmg, explosiondmg;
    [SerializeField] int number3 = 20;
    [SerializeField] float speed3harder = 2, dmg3star = 100;





    public override void SpawnWave() {

        StartCoroutine(PreFight());
    }

    IEnumerator PreFight() {
        Movement bg = Instantiate(background, new Vector3(0, 8, 0.9f), Quaternion.identity);
        bossImage = bg.transform.Find("bossImage").gameObject;
        bg.destroyBoundary = 10f;
        bg.SetSpeed(new Vector2(0, -0.8f));
        bg.StopMovingAfter(10);
        yield return new WaitForSeconds(12);
        StartCoroutine(DialogueManager.StartDialogue(preBossFight, PreFight2));
    }
    void PreFight2() {
        GameManager.PlayEndBossMusic();
        StartCoroutine(DialogueManager.StartDialogue(preBossFight2, StartBossFight));
    }

   
    void StartBossFight() {
        Vector2 initialPosition = new Vector2(bossImage.transform.position.x, bossImage.transform.position.y);

        currentBoss = Instantiate(boss, initialPosition, Quaternion.identity);
        currentBoss.movement.destroyBoundary = 10f;
        GameManager.currentBoss = currentBoss;
        SwitchToBoss();
        currentBoss.shooting.StartShooting(Pattern1(currentBoss));
        if (harder) {
            currentBoss.shooting.StartShooting(EnemyPatterns.PulsingBullets(GameManager.gameData.smallRoundBullet.GetItem(DamageType.Pure), dmg1ball, currentBoss.transform,
                speed1, shotrate1, number1, GameManager.gameData.magicPulse1SFX));
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
        Invoke("StartPattern2", spellCardTransition);
    }
    void StartPattern2() {
        
        SwitchToBoss();
        currentBoss.bosshealth.OnLifeDepleted += EndPhase2;
        currentBoss.shooting.StartShootingAfter(RainOfArrows(), 0.3f);
        currentBoss.shooting.StartShootingAfter(MoveLeftAndRight(), 0.3f);
        currentBoss.shooting.StartShootingAfter(EnemyPatterns.ShootAtPlayer(GameManager.gameData.stage1arrowBullet, dmg2, currentBoss.transform, arrowSpeed, shotRate, GameManager.gameData.longarrowSFX), 0.3f);
  
    }

    void EndPhase2() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase2;
        EndPhase();
        StartCoroutine(DialogueManager.StartDialogue(midFightDialogue, StartPhase3));
        
    }

    void StartPhase3() {
        SpellCardUI(namesOfSpellCards[1]);
        Invoke("StartPattern3", spellCardTransition);
    }

    void StartPattern3() {
        SwitchToBoss();
        currentBoss.shooting.StartShooting(Pattern3());
        currentBoss.bosshealth.OnDeath += EndStage;
    }

    void EndStage() {
        if (currentBoss)
        {
            currentBoss.bosshealth.OnDeath -= EndStage;
        }
        try
        {

            EndPhase();
            Invoke("Collect", 0.1f);
        }
        catch (Exception ex) {
            Debug.Log(ex);
        }
        StartCoroutine(DialogueManager.StartDialogue(endFightDialogue, NextStage));
    
    }

    

    IEnumerator Pattern3() {
        Animator animator = currentBoss.gameObject.GetComponent<Animator>();
        while (animator) {
            bool up = true;
                animator.SetTrigger("Disappear");
                if (harder)
                {
                    Patterns.RingOfBullets(GameManager.gameData.starBullet.GetItem(UnityEngine.Random.Range(0, 3)),
                        dmg3star, currentBoss.transform.position, number3, Functions.AimAtPlayer(currentBoss.transform), speed3harder, null);
                }
                AudioManager.current.PlaySFX(GameManager.gameData.tpSFX);
                yield return new WaitForSeconds(1f);
                if (animator)
                {
                if (GameManager.playerPosition.y > 0) {
                    up = false;
                }
                    currentBoss.transform.position = GameManager.playerPosition + new Vector2(0, up?1:-1);
                    animator.SetTrigger("Appear");
                }
                if (animator)
                {
                    yield return new WaitForSeconds(0.8f);
                    if (currentBoss)
                    {
                        Bullet punchbul = GameManager.bulletpools.SpawnBullet(punch, currentBoss.transform.position);
                        punchbul.movement.SetSpeed(new Vector2(0, up? -punchSpeed: punchSpeed));
                        punchbul.movement.destroyBoundary = 6f;
                        ActionTrigger<Movement> trigger = new ActionTrigger<Movement>(movement => movement.time > 1 / punchSpeed);
                        trigger.OnTriggerEvent += movement =>
                        {
                            Bullet bul = Instantiate(GameManager.gameData.explosionBullet, movement.transform.position, Quaternion.identity);
                            bul.SetDamage(explosiondmg);
                            Destroy(bul, 1.2f);
                            ExplodingAndBack(movement.transform.position);
                            movement.GetComponent<Bullet>().Deactivate();
                            currentBoss.shooting.StartShooting(GameManager.maincamera.ShakeCamera(0.12f, 0.2f));
                        };
                        punchbul.movement.triggers.Add(trigger);


                    }

                }
                yield return new WaitForSeconds(pausetime + 1/punchSpeed);
            }
          
        

    }

 


    IEnumerator Pattern1(Enemy enemy) {
  

        return Functions.RepeatAction(() =>
            {
                float offset = UnityEngine.Random.Range(0f, 360f);
                Patterns.CustomRing((angle) => enemy.shooting.StartCoroutine(EnemyPatterns.ConePattern(
                    GameManager.gameData.ellipseBullet.GetItem(DamageType.Pure), dmg1, 
                    enemy.transform, angle, pattern1Speed, pattern1SpawnRate, pattern1Number, pattern1Spacing, GameManager.gameData.shortarrowSFX)), offset, numberOfCone);
            }, pattern1PulseRate);
     
        
        }
    
    
    IEnumerator MoveLeftAndRight() {
        bool left = true;
        while (true) {
            float toX = left ? -3.5f : 3.5f;
            float toY = UnityEngine.Random.Range(minY, maxY);
            float time = currentBoss.movement.MoveTo(new Vector2(toX, toY), moveSpeed);
            left = !left;
            yield return new WaitForSeconds(time);
        
        }
       
    
    }

    List<Bullet> ExplodingAndBack(Vector2 pos) {
       
        return Patterns.CustomRing(
            angle => {
            Bullet bul = Patterns.ShootCustomBullet(GameManager.gameData.smallRoundBullet.GetItem(DamageType.Pure), dmg3, pos, Movement.RotatePath(angle,
                    t =>
                    new Vector2(0, t < maxRadiusPattern3 / initialSpeedPattern3 ?
                    initialSpeedPattern3 : t < maxRadiusPattern3 / initialSpeedPattern3 + shortDelayPattern3 ?
                    0 : -finalSpeedPattern3)), MovementMode.Velocity, GameManager.gameData.explosionSFX);
                bul.movement.destroyBoundary = 6f;
                return bul;
            }, 0, numberOfBulletsPattern3); 

    }

    IEnumerator RainOfArrows() {
        return Functions.RepeatCustomActionCustomTime(i =>
        {
            Patterns.ShootStraight(GameManager.gameData.stage1arrowBullet,
               dmg2, new Vector2(UnityEngine.Random.Range(-4f, 4f), 4.4f), -90, UnityEngine.Random.Range(minSpeed, maxSpeed), null);
          
        }, i => UnityEngine.Random.Range(arrowSpawnTimeMin, arrowSpawnTimeMax));
            
    
    }
 

 



  
    
}
