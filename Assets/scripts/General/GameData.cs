using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// this class holds a list of prefabs that are commonly used in the game
public class GameData : MonoBehaviour
{
    public SpellCardUI spellcardUI;
    public SFX lifeDepletedSFX;
    public BulletPack pointedBullet, smallRoundBullet, bigBullet, arrowBullet, ellipseBullet;
    public Bullet leafBullet1, leafBullet2, leafBullet3, rockBullet;
    public Bullet whiteArrowBullet;
    public Bullet waterCircle, earthCircle;
    public Bullet mushroomPillar;
    public EnemyPack ghosts, linemonster, mushrooms;
    public Enemy waterFairy;
    public Enemy patternSprite, treeant;
    public Enemy midBossMushroomMob;
    public Sprite playerDialogueSprite;

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
