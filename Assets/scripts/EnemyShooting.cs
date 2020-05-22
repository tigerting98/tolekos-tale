﻿using System;
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
    GameObject player;
    Vector3 playerPosition = new Vector2(0,-4);
  
    void Start()
    {
        
        actions = new List<Func<IEnumerator>>();
        actions.Add(Pattern1);
        actions.Add(Pattern2);
        actions.Add(Pattern3);
        actions.Add(Pattern4);
        actions.Add(Pattern5);
        player = GameObject.FindGameObjectWithTag("Player");
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
       

        bul.setSpeed(Quaternion.Euler(0, 0, angle) * new Vector3(0, speed, 0));
      
        
    }

     float getPlayerAngle() {

        Vector2 diff = playerPosition - transform.position;
        return Mathf.Rad2Deg * Mathf.Atan2(diff.y, diff.x) + 270;
    }


    IEnumerator Pattern1() {
        while (true)
        {
            

            shoot(transform.position, getPlayerAngle());
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

            for (int i = 0; i < 30; i++) {

                shoot(transform.position, angle);
                angle += 360 / 30;
            
            }
        
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
            Bullet bull = Instantiate(bullet, transform.position, Quaternion.identity);
            BulletMovement move = bull.gameObject.GetComponent<BulletMovement>();
            move.SetCustomPath(time => new Vector2((float)(0.5 * Math.Sin(time * 10)), -speed * time));
            move.SetStartingPoint(transform.position);
            move.RotateTrajectory(30);
            yield return new WaitForSeconds(shotRate);
        }
    }
}
