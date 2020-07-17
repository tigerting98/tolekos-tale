using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// this class holds a list of prefabs that are commonly used in the game
public class GameData : MonoBehaviour
{
    public SpellCardUI spellcardUI;
    public SFX lifeDepletedSFX;
    public BulletPack pointedBullet, smallRoundBullet, bigBullet, arrowBullet, ellipseBullet, starBullet, laserBullet;
    public BulletPack magicCircles;
    public BulletPack stage5lines;
    public Bullet punchBullet, stage1arrowBullet, explosionBullet;
    public Bullet leafBullet1, leafBullet2, leafBullet3, rockBullet;
    public Bullet whiteArrowBullet;
    public Bullet waterCircle, earthCircle, fireCircle;
    public Bullet mushroomPillar;
    public Bullet earthPillar, firePillar;
    public Bullet fireBullet, fireBall;
    public Bullet fireBeam, fireShortLaser, fireBeam2;
    public Bullet masterSpark;
    public Bullet snowflake, icicle;
    public Bullet treeRockBullet;
    public Bullet fireWheel;
    public Bullet pageBullet;
    public Bullet raindrop;
    public Bullet featherBullet;
    public EnemyPack ghosts, linemonster, mushrooms, pixies, libraryMonsters;
    public Enemy waterFairy;
    public Enemy patternSprite, treeant;
    public Enemy midBossMushroomMob;
    public Enemy greenSlime;
    public Enemy candelabra, watercandle, pottedplant;
    public Enemy flyingBook;
    public Sprite playerDialogueSprite;
    public LevelDescription beforeStage1;

    public BombDrop defaultBombDrop;
    public LifeDrop lifeDrop1000;
    public LifeDrop lifeDrop300;
    public LifeDrop lifeDropFull;
    public SFX waterBolt1;

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
