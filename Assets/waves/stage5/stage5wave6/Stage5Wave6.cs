using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5Wave6 : WavePattern8
{
    public override void SetUp()
    {
        ringBullet = GameManager.gameData.fireBall;
        lineBullet = GameManager.gameData.smallRoundBullet.GetItem(DamageType.Fire);
        ringBullets = GameManager.gameData.bigBullet;
        lineBullets = GameManager.gameData.ellipseBullet;
        enemy = GameManager.gameData.flyingBook;
        enemies = GameManager.gameData.libraryMonsters;
    }
}
