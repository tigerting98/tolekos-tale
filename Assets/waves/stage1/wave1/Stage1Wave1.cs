using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stage1Wave1 : WavePattern2
{
    // Start is called before the first frame update
    public override void SetUp()
    {
        bullet = GameManager.gameData.ellipseBullet.GetItem(DamageType.Pure);
        enemy = GameManager.gameData.ghosts.GetItem(DamageType.Pure);
    }


}
