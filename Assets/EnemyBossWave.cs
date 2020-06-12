using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossWave : EnemyWave
{
    public Boss boss;


    [Header("Audio")]
    [SerializeField] protected SFX lifeDepletedSFX;

    protected SpellCardUI currentUI;
    protected Boss currentBoss;
    [SerializeField] protected List<string> namesOfSpellCards;




    [Header("Next Scene")]
    [SerializeField] protected LevelDescription nextLevel;





    



   
    public virtual void Collect()
    {
        GameManager.CollectEverything();
    }
    public virtual void NextStage()
    {
        GameManager.victory = true;
        GameManager.sceneLoader.LoadShopScene(nextLevel);


    }

    

    void PlayLifeDepletedSound()
    {
        lifeDepletedSFX.PlayClip();

    }

    public virtual void EndPhase()
    {
        PlayLifeDepletedSound();
        currentBoss.shooting.StopAllCoroutines();
        currentBoss.movement.StopMoving();
        GameManager.DestoryAllEnemyBullets();
        if (currentUI)
        { Destroy(currentUI.gameObject); }
    }

    public virtual void SpellCardUI(string name)
    {
        currentUI = Instantiate(GameManager.gameData.spellcardUI);
        currentUI.PlaySFX();
        currentUI.SetText(name.Replace("\\n", "\n"));

    }

}
