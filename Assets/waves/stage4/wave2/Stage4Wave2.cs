using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4Wave2 : WavePattern7
{
    public override void SetUp()
    {
        enemy = GameManager.gameData.linemonster.GetItem(DamageType.Fire);
        bigBullet = GameManager.gameData.bigBullet.GetItem(DamageType.Fire);
        smallBullet = GameManager.gameData.fireBullet;
    }
}
