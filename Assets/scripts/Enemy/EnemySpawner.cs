using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
   
    [SerializeField] List<WaveConfig> waves;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWaveAfter(waves[0], 1));
        StartCoroutine(SpawnWaveAfter(waves[1], 4));
        StartCoroutine(SpawnWaveAfter(waves[2], 7));

    }
    IEnumerator SpawnWaveAfter(WaveConfig config, float sec) {
        yield return new WaitForSeconds(sec);
        StartCoroutine(SpawnWave(config));
    }
    IEnumerator SpawnWave(WaveConfig config) {
        for (int i = 0; i < config.number; i++) {
            Enemy enemy = Instantiate(config.enemy, new Vector3(0, 0, 0), Quaternion.identity);
            enemy.SetSpeed(config.moveSpeed);
            enemy.SetWaypoints(config.GetWaypoints());
            yield return new WaitForSeconds(config.spawntime);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
