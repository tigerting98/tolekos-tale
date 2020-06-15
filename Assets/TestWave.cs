using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWave : MonoBehaviour
{
    [SerializeField] EnemyWave wave;
    [SerializeField] int playerLevel= 1;
    void Start()
    {
        Instantiate(wave).SpawnWave();
        for (int i = 1; i < playerLevel; i++) {
            PlayerStats.LevelUp();
        }
        PlayerStats.GainEXP(1);
    }

   
}
