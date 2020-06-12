
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

    public static event Action<bool> OnGameover;
    public static event Action OnBossLifeDepleted;
    public static event Action OnSummonBoss;
    public static GamePlayerInput gameInput;
    public static DialogueUI dialogueUI;

    public static LevelDescription levelDescription = null;

    public static Enemy currentBoss = null;

    public static void PlaySFX(AudioClip clip, float volume) {
        AudioSource.PlayClipAtPoint(clip, mainCamera.transform.position, volume);
    
    }

    public static void InvokeGameOverEvent(bool victory) {
        OnGameover?.Invoke(victory);

    }

    public static void CollectEverything() {
        foreach (GameObject collectible in collectibles.Values) {
            collectible.GetComponent<Movement>().Homing(player.gameObject, 5f);
        }

    }

    public static void BossLifeDepleted() {
        OnBossLifeDepleted?.Invoke();
    
    }


    public static void DestoryAllEnemyBullets() {
        foreach (GameObject obj in enemyBullets.Values) {
            GameObject particleEffect = Instantiate(obj.GetComponent<Bullet>().explosion, obj.transform.position, Quaternion.identity);
            Destroy(particleEffect, 1f);
            Destroy(obj);
        }
    }

    public static void SummonBoss() {
        OnSummonBoss?.Invoke();
    }


}

