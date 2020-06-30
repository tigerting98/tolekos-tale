using System;
using System.Collections;
using System.Collections.Generic;
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
    public EnemyBossWave midBoss;
    [Header("After MidBoss")]
    public List<EnemyWave> wavesSecondHalf;
    public List<float> timesSecondHalf;
    public EnemyBossWave endBoss;

    public virtual void Start()
    {
        GameManager.ResetBosses();

        Assert.IsTrue(wavesFirstHalf.Count == timesFirstHalf.Count);
        for (int i = 0; i < wavesFirstHalf.Count; i++) {
            EnemyWave wave = Instantiate(wavesFirstHalf[i]);
            StartCoroutine(SpawnWaveAfter(wave, timesFirstHalf[i]));
        }

        GameManager.OnSummonMidBoss += MidBoss;
        GameManager.OnSummonEndBoss += FinalBoss;
    }
    void MidBoss() {
        if (midBoss)
        {
            try
            {
                
                background.SetTrigger("StopMoving");
            }
        
            catch (Exception ex) { Debug.Log(ex.ToString()); }
            EnemyBossWave boss = Instantiate(midBoss);
            StartCoroutine(SpawnWaveAfter(boss, 0));

            boss.OnDefeat += AfterMidBoss;
        }
    
    }
    void AfterMidBoss() {
        try
        {

            background.SetTrigger("StartMoving");
        }
        catch (Exception ex) { Debug.Log(ex.ToString()); }

        for (int i = 0; i < wavesSecondHalf.Count; i++)
        {
            EnemyWave wave = Instantiate(wavesSecondHalf[i]);
            StartCoroutine(SpawnWaveAfter(wave, timesSecondHalf[i]));
        }

    }

    void FinalBoss() {
        if (endBoss)
        {
            EnemyBossWave boss = Instantiate(endBoss);
            StartCoroutine(SpawnWaveAfter(boss, 0));
        }
    }
    // Update is called once per frame
    public virtual void Update()
    {
        timer = timer + Time.deltaTime;
    }

 
    IEnumerator SpawnWaveAfter(EnemyWave wave, float sec)
    {
        yield return new WaitForSeconds(sec);

        wave.SpawnWave();
    }
    private void OnDestroy()
    {
        GameManager.OnSummonEndBoss -= FinalBoss;
        GameManager.OnSummonMidBoss -= MidBoss;
    }

    
}
