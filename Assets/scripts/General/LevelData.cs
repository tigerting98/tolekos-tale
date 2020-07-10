using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Level")]
public class LevelData : ScriptableObject
{
    [Header("Before MidBoss")]
    public List<EnemyWave> wavesFirstHalf;
    public List<float> timesFirstHalf;
    [Header("Mid Boss")]
    public EnemyBossWave midBoss;
    [Header("After MidBoss")]
    public List<EnemyWave> wavesSecondHalf;
    public List<float> timesSecondHalf;
    public EnemyBossWave endBoss;
}
