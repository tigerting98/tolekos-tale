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
    Player player;
    Vector3 playerPosition;
  
    void Start()
    {
        player = GameManager.player;
        actions = new List<Func<IEnumerator>>();
        actions.Add(Pattern1);
        actions.Add(Pattern2);
        actions.Add(Pattern3);
        actions.Add(Pattern4);
        actions.Add(Pattern5);
        actions.Add(Pattern6);
        if (player != null)
        {
            playerPosition = player.transform.position;
        }
        StartCoroutine(actions[bulletPattern - 1]());
        

    }
   
    // Update is called once per frame
    void Update()
    {
        if (player != null) {
            playerPosition = player.transform.position;
            
        }
        
    }


 


    IEnumerator Pattern1() {
        while (true)
        {

          
            Patterns.ShootStraight(bullet, transform.position, Patterns.AimAt(transform.position, playerPosition), speed);
            yield return new WaitForSeconds(shotRate);
        }

    }

    IEnumerator Pattern2()
    {
        while (true)
        {
            float angle = Patterns.AimAt(transform.position, playerPosition);

            Patterns.ShootStraight(bullet, transform.position, angle, speed);
            Patterns.ShootStraight(bullet, transform.position, angle - 5, speed);
            Patterns.ShootStraight(bullet, transform.position, angle + 5, speed);
            yield return new WaitForSeconds(shotRate);
        }

    }

    IEnumerator Pattern3() {

        while (true) {
            float angle = Patterns.AimAt(transform.position, playerPosition);

            Patterns.RingOfBullets(bullet, this.transform.position, 30, angle, speed);
        
            yield return new WaitForSeconds(shotRate);
        
        
        
        }
    
    
    }

    IEnumerator Pattern4() {
       
        int lines = 10;
        float angularVel = 1;
        float timer = 0;
        while (true) {
            float angle = (float)(180 * Math.Sin(timer * angularVel));
            
            Patterns.RingOfBullets(bullet, this.transform.position, lines, angle, speed);

            timer += shotRate;
            yield return new WaitForSeconds(shotRate);

                
        
        
        }
    }

    IEnumerator Pattern5() {
        while (true) {
            Patterns.ShootSinTrajectory(bullet, this.transform.position, Patterns.AimAt(transform.position, playerPosition), speed, 10, 0.2f);
            yield return new WaitForSeconds(shotRate);
        }
    }
    IEnumerator Pattern6()
    {
        while (true)
        {
            Patterns.ArchimedesSpiral(bullet, this.transform.position, 1, speed, -90);
            yield return new WaitForSeconds(shotRate);
        }
    }
}
