using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage6EndBoss : EnemyBossWave
{
    [SerializeField] GameObject background;
    [SerializeField] GameObject imageofboss;
    [SerializeField] Dialogue dialogue1, dialogue2;
    [SerializeField] ParticleSystem startparticle, spawnParticle;
    [Header("Pattern1")]
    [SerializeField] float y1, movespeed, buldmg1;
    [SerializeField] int petalCount1, bulletperpatel1;

    public override void SpawnWave()
    {
        Destroy(Instantiate(startparticle, new Vector2(0, 0), Quaternion.identity), 5f);
        Invoke("StartAnimation", 0.8f); 
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
    }

    /*
    List<Bullet> FlowerPattern(DamageType type1, DamageType type2, DamageType type3) { 
        
    }

    List<Bullet> SingleFlower(DamageType type, float fastspeed, float slowspeed, float offset) {
        Bullet bul = GameManager.gameData.ellipseBullet.GetItem(type);
        float incre = 360f/(petalCount1*
        List<Bullet> buls = new List<Bullet>();
        for (int ) { 
        
        }
    }*/
}


