using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;

public class Level : MonoBehaviour
{
    [SerializeField] Animator background = default;
    [HideInInspector] public float timer = 0;
    [Header("Before MidBoss")]
    public List<EnemyWave> wavesFirstHalf;
    public List<float> timesFirstHalf;
    [Header("Mid Boss")]
    public Stage1MidBoss midBoss;
    public float midBossTimer;
    [Header("After MidBoss")]
    public List<EnemyWave> wavesSecondHalf;
    public List<float> timesSecondHalf;
    public Stage1EndBoss endBoss;

    void Start()
    {


        Assert.IsTrue(wavesFirstHalf.Count == timesFirstHalf.Count);
        for (int i = 0; i < wavesFirstHalf.Count; i++) {
            EnemyWave wave = Instantiate(wavesFirstHalf[i]);
            StartCoroutine(SpawnWaveAfter(wave, timesFirstHalf[i]));
        }
        if (midBoss)
        {
            Stage1MidBoss boss = Instantiate(midBoss);
        StartCoroutine(SpawnMidBoss(boss, midBossTimer));
        
        boss.OnDefeat += AfterMidBoss; }
        GameManager.OnSummonBoss += FinalBoss;
    }

    void AfterMidBoss() {
        background.SetTrigger("StartMoving");
        for (int i = 0; i < wavesSecondHalf.Count; i++)
        {
            EnemyWave wave = Instantiate(wavesSecondHalf[i]);
            StartCoroutine(SpawnWaveAfter(wave, timesSecondHalf[i]));
        }

    }

    void FinalBoss() {
        Debug.Log("SUmmoned");
        if (endBoss)
        {
            Debug.Log("instant");
            Stage1EndBoss boss = Instantiate(endBoss);
            StartCoroutine(SpawnWaveAfter(boss, 0));
        }
    }
    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
    }

    IEnumerator SpawnMidBoss(EnemyWave wave, float sec) {
        yield return new WaitForSeconds(sec);
        background.SetTrigger("StopMoving");
        wave.SpawnWave();
    }
    IEnumerator SpawnWaveAfter(EnemyWave wave, float sec)
    {
        yield return new WaitForSeconds(sec);

        wave.SpawnWave();
    }

    
}
