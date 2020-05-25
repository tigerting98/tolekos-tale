using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [HideInInspector] public float timer = 0;
    public List<EnemyWave> waves;
    void Start()
    {
        for (int i = 0; i < waves.Count; i++) {
            EnemyWave wave = Instantiate(waves[i]);
            StartCoroutine(SpawnWaveAfter(wave, wave.startTime));
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
    }
    IEnumerator SpawnWaveAfter(EnemyWave wave, float sec)
    {
        yield return new WaitForSeconds(sec);

        wave.SpawnWave();
    }

    
}
