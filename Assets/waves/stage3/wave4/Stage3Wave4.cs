using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Wave4 : WavePattern5
{
    public override void SetUp()
    {
        enemy = GameManager.gameData.ghosts.GetItem(DamageType.Earth);
        bullet = GameManager.gameData.smallRoundBullet.GetItem(DamageType.Earth);
    }
}
