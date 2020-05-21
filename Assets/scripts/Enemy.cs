using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int cur = 0;
 
    List<Transform> waypoints;
    WaveConfig config;
    [SerializeField] int hp;

    // Start is called before the first frame update
    void Start()
    {
     
        waypoints = config.GetWaypoints();
        transform.position = waypoints[0].transform.position;


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Bullet bullet= collision.gameObject.GetComponent<Bullet>();
        if (bullet != null) {
            hp -= bullet.TakeDamage();
            Destroy(bullet.gameObject);
        }
       
        
        if (hp <= 0) {
            BossDeath death = gameObject.GetComponent<BossDeath>();
            if (death != null) {
                death.Death();
            }

            Destroy(gameObject);
        }
        
    }
    public void SetWaveConfig(WaveConfig waveConfig) {
     
        config = waveConfig;
        
    }


    // Update is called once per frame
    void Update()
    {
        
        
        if (cur < waypoints.Count - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[cur + 1].position, config.moveSpeed * Time.deltaTime);

            if (transform.position.x == waypoints[cur + 1].position.x && transform.position.y == waypoints[cur + 1].position.y)
            {

                if (waypoints[cur + 1].gameObject.tag == "End")
                {

                    Destroy(gameObject);
                }
                else
                {
                    cur++;

                }

            }



        }
    }
}
