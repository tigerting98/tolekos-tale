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
        try
        {
            PlayLifeDepletedSound();
            currentBoss.shooting.StopAllCoroutines();
            currentBoss.movement.StopMoving();
            currentBoss.enemyAudio.StopAllCoroutines();
            GameManager.DestoryAllEnemyBullets();
            GameManager.DestroyAllNonBossEnemy(true);
            if (currentUI)
            { Destroy(currentUI.gameObject); }
            SwitchToImage();
            }catch (Exception ex)
        {
            Debug.Log(ex);
        }
        
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
        try
        {
            bossImage.transform.position = currentBoss.transform.position;
            currentBoss.gameObject.SetActive(false);
        }
        catch (Exception ex) 
        {
            Debug.Log(ex);
        }

      
    }

    protected virtual void SwitchToBoss()
    {
         try
         {
             currentBoss.gameObject.SetActive(true);
             currentBoss.transform.position = bossImage.transform.position;
             bossImage.GetComponent<SpriteRenderer>().enabled = false;
         }
         catch (Exception ex)
         {
             Debug.Log(ex);
         }
      
    }

}
