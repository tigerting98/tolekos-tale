using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4Wave3 : WavePattern8
{
    public override void SetUp()
    {
        soundeffect = GameManager.gameData.gunSFX;
        ringBullet = GameManager.gameData.fireBall;
        lineBullet = GameManager.gameData.smallRoundBullet.GetItem(DamageType.Fire);
        enemy = GameManager.gameData.linemonster.GetItem(DamageType.Fire);
    }
}
