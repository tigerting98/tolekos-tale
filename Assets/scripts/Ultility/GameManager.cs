
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

    public static Hashtable enemies = new Hashtable();

    public static Hashtable collectibles = new Hashtable();

    public static event Action<bool> OnGameover;

    public static void InvokeGameOverEvent( bool victory) {
        OnGameover?.Invoke(victory);
    
    }

    public static void CollectEverything() {
        foreach (GameObject collectible in collectibles.Values) {
            collectible.GetComponent<Movement>().Homing(player.gameObject, 5f);
        }
    
    }

    



}

