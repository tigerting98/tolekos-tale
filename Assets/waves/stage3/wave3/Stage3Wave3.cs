using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Wave3 : WavePattern4
{
    public override void SetUp()
    {
        this.enemy = GameManager.gameData.greenSlime;
        this.bullet = GameManager.gameData.leafBullet1;
    }
}
