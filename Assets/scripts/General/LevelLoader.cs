using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
//This class loads a level data to render the level out
public class LevelLoader : MonoBehaviour
{
    [SerializeField] LevelData veryeasyLevel, easyLevel, normalLevel, hardLevel, lunaticLevel;
    LevelData level;
    [SerializeField] Animator background;
    [SerializeField] MusicTrack stageTheme, bossTheme;


    protected virtual void Awake()
    {
        ChooseLevel(GameManager.difficultyLevel);
    }
    protected virtual void ChooseLevel(Difficulty difficulty) {
        switch (difficulty) {
            case Difficulty.VeryEasy:
                level = veryeasyLevel;
                break;
            case Difficulty.Easy:
                level = easyLevel;
                break;
            case Difficulty.Normal:
                level = normalLevel;
                break;
            case Difficulty.Hard:
                level = hardLevel;
                break;
            case Difficulty.Nightmare:
                level = lunaticLevel;
                break;
            default:
                level = normalLevel;
                break;
        }
    }

    public void PlayBossFightTheme() {
        AudioManager.current.music.ChangeTrack(bossTheme);
    }

    public virtual void Start()
    {
       
        AudioManager.current.music.ChangeTrack(stageTheme);
        if (!level) {
            level = normalLevel;
        }
        if (!background) {
            background = FindObjectOfType<Background>().GetComponent<Animator>();
        }
        GameManager.ResetBosses();

        Assert.IsTrue(level.wavesFirstHalf.Count == level.timesFirstHalf.Count);
        for (int i = 0; i < level.wavesFirstHalf.Count; i++)
        {
            EnemyWave wave = Instantiate(level.wavesFirstHalf[i]);
            StartCoroutine(SpawnWaveAfter(wave, level.timesFirstHalf[i]));
        }
        GameManager.OnPlayBossTheme += PlayBossFightTheme;
        GameManager.OnSummonMidBoss += MidBoss;
        GameManager.OnSummonEndBoss += FinalBoss;
    }
    void MidBoss()
    {
        if (level.midBoss)
        {
            try
            {

                background.SetTrigger("StopMoving");
            }

            catch (Exception ex) { Debug.Log(ex.ToString()); }
            EnemyBossWave boss = Instantiate(level.midBoss);
            StartCoroutine(SpawnWaveAfter(boss, 0));

            boss.OnDefeat += AfterMidBoss;
        }

    }
    void AfterMidBoss()
    {
        
        try
        {

            background.SetTrigger("StartMoving");
        }
        catch (Exception ex) { Debug.Log(ex.ToString()); }
        Assert.IsTrue(level.wavesSecondHalf.Count == level.timesSecondHalf.Count);
        for (int i = 0; i < level.wavesSecondHalf.Count; i++)
        {
            EnemyWave wave = Instantiate(level.wavesSecondHalf[i]);
            StartCoroutine(SpawnWaveAfter(wave, level.timesSecondHalf[i]));
        }

    }

    void FinalBoss()
    {
        if (level.endBoss)
        {
            EnemyBossWave boss = Instantiate(level.endBoss);
            StartCoroutine(SpawnWaveAfter(boss, 0));
        }
    }


    IEnumerator SpawnWaveAfter(EnemyWave wave, float sec)
    {
        yield return new WaitForSeconds(sec);

        wave.SpawnWave();
    }
    private void OnDestroy()
    {
        GameManager.OnPlayBossTheme -= PlayBossFightTheme;
        GameManager.OnSummonEndBoss -= FinalBoss;
        GameManager.OnSummonMidBoss -= MidBoss;
    }
}
