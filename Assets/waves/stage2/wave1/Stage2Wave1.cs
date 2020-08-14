using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Wave1 : WavePattern1
{
    // Start is called before the first frame update
    public override void SetUp()
    {
        bullet = GameManager.gameData.smallRoundBullet.GetItem(DamageType.Water);
        enemies.Add(GameManager.gameData.ghosts.GetItem(DamageType.Water));
        enemies.Add(GameManager.gameData.linemonster.GetItem(DamageType.Water));
        pulsingSFX = GameManager.gameData.waterpulseSFX; 
    }
}
