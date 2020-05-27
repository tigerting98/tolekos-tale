using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Level : MonoBehaviour
{
    [HideInInspector] public float timer = 0;
    public List<EnemyWave> waves;
    public List<float> times;
    void Start()
    {
        Assert.IsTrue(waves.Count == times.Count);
        for (int i = 0; i < waves.Count; i++) {
            EnemyWave wave = Instantiate(waves[i]);
            StartCoroutine(SpawnWaveAfter(wave, times[i]));
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
