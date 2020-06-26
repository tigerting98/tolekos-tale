using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4Wave1 : WavePattern6
{
    public override void SetUp()
    {
        enemy = GameManager.gameData.ghosts.GetItem(DamageType.Fire);
        bullet = GameManager.gameData.fireBall;
    }
}
