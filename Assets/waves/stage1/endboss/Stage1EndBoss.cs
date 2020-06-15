using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class Stage1EndBoss : EnemyBossWave
{
    [SerializeField] Movement background;
    [Header("Dialogues")]
    [SerializeField] Dialogue preBossFight;
    [SerializeField] Dialogue midFightDialogue;
    [SerializeField] Dialogue endFightDialogue;

    [Header("Audio")]
    [SerializeField] SFX pattern1SFX;
    [SerializeField] SFX pattern2SFX;
    [SerializeField] SFX pattern3smashSFX;
    [SerializeField] SFX pattern3tpSFX;
   
    [Header("Pattern1")]
    [SerializeField]
    BulletPack pattern1ConePack;
    [SerializeField]
    int pattern1Number = 5, numberOfCone = 5;
    [SerializeField] float pattern1PulseRate = 1f, pattern1SpawnRate = 0.05f, pattern1Spacing = 0.05f, pattern1Speed = 3f;


    [Header("Pattern2")]
    [SerializeField] float arrowSpawnTimeMax = 0.05f, arrowSpawnTimeMin = 0.01f;
    [SerializeField] float maxSpeed = 5f, minSpeed = 2f;
    [SerializeField] Bullet arrow;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float maxY = 3.5f, minY = 0.5f;
    [Header("Pattern2 Homing Behavior")]
    [SerializeField] float arrowSpeed = 1.5f;
    [SerializeField] float shotRate = 1f;

    [Header("Pattern3 Bullet Behavior")]
    [SerializeField] Bullet explodingBullet;
    [SerializeField] int numberOfBulletsPattern3 = 30;
    [SerializeField] float initialSpeedPattern3 = 10f, maxRadiusPattern3 = 1f, shortDelayPattern3 = 0.2f, finalSpeedPattern3 = 2f;
    [SerializeField] float pausetime = 2f;
    [SerializeField] Bullet explosion, punch;
    [SerializeField] float punchSpeed = 2f;
    [SerializeField] float explosionTime;





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
        StartCoroutine(DialogueManager.StartDialogue(preBossFight, StartBossFight));
    }
   

   
    void StartBossFight() {
        Vector2 initialPosition = new Vector2(bossImage.transform.position.x, bossImage.transform.position.y);

        currentBoss = Instantiate(boss, initialPosition, Quaternion.identity);
        GameManager.currentBoss = currentBoss;
        SwitchToBoss();
        currentBoss.shooting.StartShooting(Pattern1(currentBoss));
        currentBoss.bosshealth.OnLifeDepleted += EndPhase1;
        
    }

    void EndPhase1() {
      
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase1;
        EndPhase();
        
        Invoke("StartPhase2", 1f);
    
    }

    void StartPhase2() {
        SpellCardUI(namesOfSpellCards[0]);
        Invoke("StartPattern2", 3f);
    }
    void StartPattern2() {
        
        SwitchToBoss();
        currentBoss.bosshealth.OnLifeDepleted += EndPhase2;
        currentBoss.shooting.StartShootingAfter(RainOfArrows(), 0.3f);
        currentBoss.shooting.StartShootingAfter(MoveLeftAndRight(), 0.3f);
        currentBoss.shooting.StartShootingAfter(EnemyPatterns.ShootAtPlayer(arrow, currentBoss.transform, arrowSpeed, shotRate), 0.3f);
        currentBoss.enemyAudio.PlayAudio(pattern2SFX, shotRate, 0.3f);
  
    }

    void EndPhase2() {
        currentBoss.bosshealth.OnLifeDepleted -= EndPhase2;
        EndPhase();
        StartCoroutine(DialogueManager.StartDialogue(midFightDialogue, StartPhase3));
        
    }

    void StartPhase3() {
        SpellCardUI(namesOfSpellCards[1]);
        Invoke("StartPattern3", 3f);
    }

    void StartPattern3() {
        SwitchToBoss();
        Coroutine pattern3 = StartCoroutine(Pattern3());
        currentBoss.bosshealth.OnDeath += EndStage;
    }

    void EndStage() {
        EndPhase();
        Invoke("Collect", 0.1f);
        bossImage.transform.position = new Vector2(0, 0);
        bossImage.SetActive(true);
        currentBoss.bosshealth.OnDeath -= EndStage;
        StartCoroutine(DialogueManager.StartDialogue(endFightDialogue, NextStage));
    
    }

    

    IEnumerator Pattern3() {
        Animator animator = currentBoss.gameObject.GetComponent<Animator>();
        while (animator) {
            animator.SetTrigger("Disappear");
            pattern3tpSFX.PlayClip();
            yield return new WaitForSeconds(1f);
            if (animator)
            {
                currentBoss.transform.position = GameManager.playerPosition + new Vector2(0, 1f);
                animator.SetTrigger("Appear");
            }
            if (animator)
            {
                yield return new WaitForSeconds(0.8f);
                if (currentBoss)
                {
                    Bullet punchbul = Instantiate(punch, currentBoss.transform.position, Quaternion.identity);
                    punchbul.movement.SetSpeed(new Vector2(0, -punchSpeed));
                    punchbul.movement.destroyBoundary = 6f;
                    yield return new WaitForSeconds(1 / punchSpeed);


                    if (punchbul)
                    {
                        Vector2 pos = punchbul.transform.position;
                        Bullet explode = Instantiate(explosion, pos, Quaternion.identity);
                        ExplodingAndBack(pos);
                        Destroy(punchbul.gameObject);
                        pattern3smashSFX.PlayClip();


                    }
                }

            }
            yield return new WaitForSeconds(pausetime);
        }

    }

 


    IEnumerator Pattern1(Enemy enemy) {
  

        return Functions.RepeatAction(() =>
            {
                float offset = UnityEngine.Random.Range(0f, 360f);
                enemy.enemyAudio.PlayAudioTimes(pattern1SFX, pattern1SpawnRate, pattern1Number);
                Patterns.CustomRing((angle) => enemy.shooting.StartCoroutine(EnemyPatterns.ConePattern(pattern1ConePack.GetBullet(DamageType.Pure), enemy.transform, angle, pattern1Speed, pattern1SpawnRate, pattern1Number, pattern1Spacing)), offset, numberOfCone);
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
            Bullet bul = Patterns.ShootCustomBullet(explodingBullet, pos, Movement.RotatePath(angle,
                    t =>
                    new Vector2(0, t < maxRadiusPattern3 / initialSpeedPattern3 ?
                    initialSpeedPattern3 : t < maxRadiusPattern3 / initialSpeedPattern3 + shortDelayPattern3 ?
                    0 : -finalSpeedPattern3)), MovementMode.Velocity);
                bul.movement.destroyBoundary = 6f;
                return bul;
            }, 0, numberOfBulletsPattern3); 

    }

    IEnumerator RainOfArrows() {
        return Functions.RepeatCustomActionCustomTime(i =>
        {

            Bullet bul = Instantiate(arrow, new Vector2(UnityEngine.Random.Range(-4f, 4f), 4.4f), Quaternion.identity);
            bul.movement.SetSpeed(new Vector2(0, -UnityEngine.Random.Range(minSpeed, maxSpeed)));
        }, i => UnityEngine.Random.Range(arrowSpawnTimeMin, arrowSpawnTimeMax));
            
    
    }
 

 



  
    
}
