using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public SpellCardUI spellcardUI;
    public SFX lifeDepletedSFX;
    public BulletPack pointedBullet, smallRoundBullet, bigBullet, arrowBullet, ellipseBullet;
    public Bullet whiteArrowBullet;
    public EnemyPack ghosts, linemonster;
    public Enemy waterFairy;
    public Enemy patternSprite;
   
    private void Awake()
    {
        if (GameManager.gameData == null)
        {
            GameManager.gameData = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else {
            Destroy(this.gameObject);
        }
    }

    
}
