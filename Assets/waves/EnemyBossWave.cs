using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossWave : EnemyWave
{
    protected static float endPhaseTransition = 0.2f;
    protected static float spellCardTransition = 1.7f;
    public Boss boss;

    public GameObject bossImage;
    [Header("Audio")]

    protected SpellCardUI currentUI;
    protected Boss currentBoss;
    [SerializeField] protected List<string> namesOfSpellCards;




    [Header("Next Scene")]
    [SerializeField] protected LevelDescription nextLevel;

    public Action OnDefeat;



    



   
    public virtual void Collect()
    {
        GameManager.CollectEverything();
    }
    public virtual void NextStage()
    {
        GameManager.victory = true;
        GameManager.sceneLoader.LoadShopScene(nextLevel);


    }

    

    protected virtual void PlayLifeDepletedSound()
    {
        AudioManager.current.PlaySFX(GameManager.gameData.lifeDepletedSFX);

    }

    public virtual void EndPhase()
    {
        PlayLifeDepletedSound();
        currentBoss.shooting.StopAllCoroutines();
        currentBoss.movement.StopMoving();
        GameManager.DestoryAllEnemyBullets();
        GameManager.DestroyAllNonBossEnemy(true);
        if (currentUI)
        { Destroy(currentUI.gameObject); }
        SwitchToImage();
    }

    public virtual void SpellCardUI(string name)
    {
        currentUI = Instantiate(GameManager.gameData.spellcardUI);
        currentUI.PlaySFX();
        currentUI.SetText(name.Replace("\\n", "\n"));

    }

    protected virtual void SwitchToImage()
    {
        bossImage.GetComponent<SpriteRenderer>().enabled = true;
        bossImage.transform.position = currentBoss.transform.position;
        currentBoss.gameObject.SetActive(false);
    }

    protected virtual void SwitchToBoss()
    {
        currentBoss.gameObject.SetActive(true);
        currentBoss.transform.position = bossImage.transform.position;
        bossImage.GetComponent<SpriteRenderer>().enabled = false;
    }

}
