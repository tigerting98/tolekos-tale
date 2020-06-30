
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static Player player;
    public static Camera mainCamera;
    public static SceneLoader sceneLoader;
    public static BossHealthBar bossHealthBar;
    public static Vector2 playerPosition;
    public static bool victory = false;
    public static GameData gameData;
    public static Hashtable enemies = new Hashtable();
    public static Hashtable enemyBullets = new Hashtable();
    public static Hashtable collectibles = new Hashtable();
    public static BulletPools bulletpools;

    public static event Action<bool> OnGameover;
    public static event Action OnSummonEndBoss;
    public static event Action OnSummonMidBoss;
    public static GamePlayerInput gameInput;
    public static DialogueUI dialogueUI;
    public const float WeakMultiplier = 0.5f, StrongMultiplier = 1.5f;

    public static LevelDescription levelDescription = null;

    public static Enemy currentBoss = null;

    public static void Reset() {
        victory = false;

        enemies = new Hashtable();
        enemyBullets = new Hashtable();
        collectibles = new Hashtable();
        ResetBosses();
        levelDescription = null;
        currentBoss = null;
    }
 

    public static void InvokeGameOverEvent(bool victory) {
        OnGameover?.Invoke(victory);

    }

    public static void CollectEverything() {
        foreach (GameObject collectible in collectibles.Values) {
            collectible.GetComponent<Movement>().Homing(player.gameObject, 5f);
        }

    }

    public static void DestroyAllNonBossEnemy(bool dropLoot) {
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

