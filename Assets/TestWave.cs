using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWave : MonoBehaviour
{
    [SerializeField] EnemyWave wave;
    void Start()
    {
        Instantiate(wave).SpawnWave();
    }

   
}
