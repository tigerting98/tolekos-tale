using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5Wave7 : WavePattern9
{
    public override void SetUp()
    {
        enemy = GameManager.gameData.flyingBook;
        enemies = GameManager.gameData.libraryMonsters;
        bullet = GameManager.gameData.ellipseBullet.GetItem(DamageType.Pure);
        bullets = GameManager.gameData.ellipseBullet;
    }
}
