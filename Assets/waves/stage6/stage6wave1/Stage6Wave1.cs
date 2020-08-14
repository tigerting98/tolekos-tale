using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6Wave1 : EnemyWave
{
    public override void SpawnWave()
    {
        GameManager.SummonMidBoss();
    }
}
