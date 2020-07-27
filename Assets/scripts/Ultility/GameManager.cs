
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
//This global class controls the game system and allows for classes to interact with each other
public enum Difficulty { VeryEasy, Easy, Normal, Hard, Nightmare}
public class GameManager : MonoBehaviour
{
    public static PauseMenu pauseMenu;
    public static DeathScene deathMenu;
    public static Difficulty difficultyLevel = Difficulty.Normal;
    public static bool death = false;
    public static Player player;
    public static MainCamera maincamera;
    public static SceneLoader sceneLoader;
    public static BossHealthBar bossHealthBar;
    public static Vector2 playerPosition;
    public static GameData gameData;
    public static Hashtable enemies = new Hashtable();
    public static Hashtable enemyBullets = new Hashtable();
    public static Hashtable collectibles = new Hashtable();
    public static BulletPools bulletpools;
    public static float SFXVolume = 1f;
    public static float musicVolume = 1f;
    public static bool practiceMode = false;
    public static event Action<bool> OnGameover;
    public static event Action OnPlayBossTheme;
    public static event Action OnSummonEndBoss;
    public static event Action OnSummonMidBoss;
    public static GamePlayerInput gameInput;
    public static DialogueUI dialogueUI;
    public const float WeakMultiplier = 0.5f, StrongMultiplier = 1.5f;
    public static int randomCounter = 0;
    public static float[] randomvalues = Enumerable.Repeat(-1f, 10000).ToArray();
    public static LevelDescription levelDescription = null;

    public static Boss currentBoss = null;

    public static void Reset() {
        randomCounter = 0;
        randomvalues = Enumerable.Repeat(-1f, 10000).ToArray();
        practiceMode = false;
        difficultyLevel = Difficulty.Normal;
        enemies = new Hashtable();
        enemyBullets = new Hashtable();
        collectibles = new Hashtable();
        ResetBosses();
        levelDescription = null;
        currentBoss = null;
    }

    public static float SupplyRandomFloat() {
        if (randomCounter >= 10000) {
            randomCounter = 0;
        }   

        float z= randomvalues[randomCounter];
        randomCounter++;
        return z<0? UnityEngine.Random.Range(0,1f): z;
    }
    public static int SupplyRandomInt(int start, int end) {
        return (int)(SupplyRandomFloat() * (end - start)) + start;
        
    }
    public static float SupplyRandomFloat(float start, float end) { 
        return SupplyRandomFloat() * (end - start) +start;
    }


    public static void CollectEverything() {
        foreach (GameObject collectible in collectibles.Values) {
            collectible.GetComponent<Movement>().Homing(player.gameObject, 4.8f);
        }

    }

    public static bool DestroyAllNonBossEnemy(bool dropLoot) {
        List<Enemy> toDestroy= new List<Enemy>();
        foreach (GameObject obj in enemies.Values) {
            if (obj.GetComponent<Boss>() == null)
            { toDestroy.Add(obj.GetComponent<Enemy>()); }
        }
       for (int i = 0; i < toDestroy.Count; i ++) {

             if (dropLoot)
              {
                    toDestroy[i].deathEffects.Die();
              }
                else {
                  Destroy(toDestroy[i].gameObject);
                }
        }
        return toDestroy.Count > 0;
        
    }

    public static void DestroyNonDPSEnemyBulletsInRadius(float radius) {
        List<Bullet> toDestroy = new List<Bullet>();
        foreach (GameObject obj in enemyBullets.Values)
        {
            if (obj.TryGetComponent<Bullet>(out Bullet comp))
            {
                if (comp.TryGetComponent(out DamageDealer dmg)) {
                    if (dmg.DestroyOnImpact() && !dmg.DamageOverTime()) {
                        if (((Vector2)dmg.transform.position - playerPosition).magnitude <= radius) {
                            toDestroy.Add(comp);
                        }
                } }
            }
        }

        for (int i = 0; i < toDestroy.Count; i++)
        {
            GameObject particleEffect = Instantiate(toDestroy[i].explosion, toDestroy[i].transform.position, Quaternion.identity);
            Destroy(particleEffect, 0.5f);
            if (toDestroy[i])
            {
                toDestroy[i].Deactivate();
            }
        }
    }

    public static void DestoryAllEnemyBullets() {
        List<Bullet> toDestroy = new List<Bullet>();
        foreach (GameObject obj in enemyBullets.Values) {
            if (obj.TryGetComponent<Bullet>(out Bullet comp))
            {
                toDestroy.Add(comp);
            }
        }

        for (int i = 0; i < toDestroy.Count; i++) {
            GameObject particleEffect = Instantiate(toDestroy[i].explosion, toDestroy[i].transform.position, Quaternion.identity);
            Destroy(particleEffect, 0.5f);
            if (toDestroy[i])
            {
                toDestroy[i].Deactivate();
            }
        }
    }
    public static void DestroyAllEnemyBulletsOnDeath() {
        List<Bullet> toDestroy = new List<Bullet>();
        foreach (GameObject obj in enemyBullets.Values)
        {
            if (obj.TryGetComponent<Bullet>(out Bullet comp))
            {
                if (comp.destroyOnDeath)
                { toDestroy.Add(comp); }
            }
        }

        for (int i = 0; i < toDestroy.Count; i++)
        {
            GameObject particleEffect = Instantiate(toDestroy[i].explosion, toDestroy[i].transform.position, Quaternion.identity);
            Destroy(particleEffect, 0.5f);
            if (toDestroy[i])
            {
                toDestroy[i].Deactivate();
            }
        }
    }
    public static void PlayEndBossMusic() {
        OnPlayBossTheme?.Invoke();
    }
    public static void SummonEndBoss() {

        OnSummonEndBoss?.Invoke();
    }

    public static void SummonMidBoss() {
        OnSummonMidBoss?.Invoke();
    }

    public static void ResetBosses() {
        OnSummonMidBoss = null;
        OnSummonEndBoss = null;
        
    }


}

