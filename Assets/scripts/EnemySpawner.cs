using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    int waveNo = 0;
    [SerializeField] List<WaveConfig> waves;
    WaveConfig currentWave;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAllWaves());
    }
    IEnumerator SpawnAllWaves() {

        for (int i = 0; i < waves.Count; i++) {
            yield return SpawnWave(waves[i]);
        }
        

    }
    IEnumerator SpawnWave(WaveConfig config) {
        for (int i = 0; i < config.number; i++) {
            Enemy enemy = Instantiate(config.enemy, new Vector3(0, 0, 0), Quaternion.identity);
            enemy.SetWaveConfig(config);
            yield return new WaitForSeconds(config.spawntime);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
