using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
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

    void shoot(Vector3 pos, float angle) {


        Bullet bul = Instantiate(bullet, pos, Quaternion.Euler(0,0,angle));
      

        bul.setSpeed(Quaternion.Euler(0, 0, angle) * new Vector3(speed, 0, 0));
      
        
    }

     float getPlayerAngle() {

        Vector2 diff = playerPosition - transform.position;
        return Mathf.Rad2Deg * Mathf.Atan2(diff.y, diff.x);
    }


    IEnumerator Pattern1() {
        while (true)
        {

          
            Patterns.ShootStraight(bullet, transform.position, getPlayerAngle(), speed);
            yield return new WaitForSeconds(shotRate);
        }

    }

    IEnumerator Pattern2()
    {
        while (true)
        {
            float angle = getPlayerAngle();

            shoot(transform.position, angle);
            shoot(transform.position, angle - 5);
            shoot(transform.position, angle + 5);
            yield return new WaitForSeconds(shotRate);
        }

    }

    IEnumerator Pattern3() {

        while (true) {
            float angle = getPlayerAngle();

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
            for (int i = 0; i < lines; i++) {

                shoot(transform.position, angle + i *  360 / lines );
               
            
            }
            
            timer += shotRate;
            yield return new WaitForSeconds(shotRate);

                
        
        
        }
    }

    IEnumerator Pattern5() {
        while (true) {
            Patterns.ShootSinTrajectory(bullet, this.transform.position, getPlayerAngle(), speed, 10, 0.2f);
            yield return new WaitForSeconds(shotRate);
        }
    }
    IEnumerator Pattern6()
    {
        while (true)
        {
            Patterns.ArchimedesSpiral(bullet, this.transform.position, 1, speed, 180);
            yield return new WaitForSeconds(shotRate);
        }
    }
}
