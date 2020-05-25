using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed;
    [SerializeField] Bullet bullet;
    [SerializeField] float shotRate;
    [SerializeField] int bulletPattern;
    List<Func<IEnumerator>> actions;
         
  
    void Start()
    {
        
        actions = new List<Func<IEnumerator>>();
        actions.Add(() => EnemyPatterns.ShootAtPlayer(bullet, transform.position, speed, shotRate));
        actions.Add(() => EnemyPatterns.ShootAtPlayerWithLines(bullet, transform.position, speed, shotRate, 5f , 3));
        actions.Add(() => EnemyPatterns.PulsingBullets(bullet, transform.position, speed, shotRate, 30));
        actions.Add(() => EnemyPatterns.BorderOfWaveAndParticle(bullet, transform.position, speed, shotRate, 10 , 1));
        actions.Add(() => EnemyPatterns.ShootSineAtPlayer(bullet, transform.position, speed, shotRate, 10f, 0.2f));
        actions.Add(() => EnemyPatterns.ArchimedesSpiral(bullet, transform.position, 1, speed, shotRate , -90));
        
        StartCoroutine(actions[bulletPattern - 1]());
        

    }
   
}
