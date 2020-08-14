using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4Wave4 : WavePattern9
{
    public override void SetUp() 
    {
        bullet = GameManager.gameData.ellipseBullet.GetItem(DamageType.Fire);
        //TODO: make fire slime
        enemy = GameManager.gameData.linemonster.GetItem(DamageType.Fire);
        soundeffect = GameManager.gameData.clickSFX;
    }
}
