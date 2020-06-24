using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Wave1b : WavePattern1
{
    // Start is called before the first frame update
    public override void SetUp()
    {
        enemies.Add(GameManager.gameData.greenSlime);
        bullet = GameManager.gameData.smallRoundBullet.GetItem(DamageType.Earth);
    }
}
