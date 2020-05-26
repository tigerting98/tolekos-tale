using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    public List<Enemy> enemies = default;
    public List<Bullet> bullets = default;
    public BulletPack bulletPack = default;
    

    public virtual void SpawnWave() { }

  

}
