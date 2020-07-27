
using UnityEngine;
using System.Collections.Generic;
// this class holds a list of prefabs that are commonly used in the game
public class GameData : MonoBehaviour
{
    public SpellCardUI spellcardUI;
    public GameObject playerSpellCardUI;
    [Header("SFX")]
    public SFX touhoustreamingSFX;
    public SFX magicCircleSummonSFX;
    public SFX lifeDepletedSFX;
    public SFX shortarrowSFX;
    public SFX longarrowSFX;
    public SFX windboltSFX;
    public SFX explosionSFX;
    public SFX magicPulse1SFX;
    public SFX gunSFX;
    public SFX tpSFX;
    public SFX UISFX;
    public SFX waterpulseSFX;
    public SFX waterstreaming1SFX;
    public SFX waterswooshSFX;
    public SFX leaf1SFX;
    public SFX slamSFX;
    public SFX magicPulse1LouderSFX;
    public SFX flyinginSFX;
    public SFX firestreamingSFX;
    public SFX firepulseSFX;
    public SFX clickSFX;
    public SFX mastersparkSFX, laser1SFX;
    public SFX firelizardSummonSFX;
    public SFX firebossSummonSFX;
    public SFX kiraSFX;
    public SFX kirasoftSFX;
    public SFX splashSFX;
    public SFX bookFiendSummonSFX;
    public SFX stage6TpSFX;
    public SFX pylferTpSFX;
    public SFX playershootSFX;
    [Header("Bullets")]
    public BulletPack magicCircles;
    public BulletPack pointedBullet, smallRoundBullet, bigBullet, arrowBullet, ellipseBullet, starBullet, laserBullet;
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

    [Header("Enemies")]
    public Enemy flyingBook;
    public EnemyPack ghosts, linemonster, mushrooms, pixies, libraryMonsters;
    public Enemy waterFairy;
    public Enemy patternSprite, treeant;
    public Enemy midBossMushroomMob;
    public Enemy greenSlime;
    public Sprite playerDialogueSprite;
    [Header("Drops")]
    public BombDrop defaultBombDrop;
    public LifeDrop lifeDrop1000;
    public LifeDrop lifeDrop300;
    public LifeDrop lifeDropFull;
    [Header("Levels")]
    public List<LevelDescription> levels;

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
