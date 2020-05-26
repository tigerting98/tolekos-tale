using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    public List<Enemy> enemies;
    public List<Bullet> bullets;
    

    public virtual void SpawnWave() { }

  

}
