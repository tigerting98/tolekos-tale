using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Wave3 : WavePattern4
{
    public override void SetUp()
    {
        enemy = GameManager.gameData.greenSlime;
        bullet = GameManager.gameData.leafBullet1;
    }
}
