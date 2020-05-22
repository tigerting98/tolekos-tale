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
    Health health;
    Death deathEffects;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        waypoints = config.GetWaypoints();
        transform.position = waypoints[0].transform.position;
        deathEffects = gameObject.GetComponent<Death>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        DamageDealer dmg = collision.gameObject.GetComponent<DamageDealer>();
        if (dmg != null) {
            health.TakeDamage(dmg.GetDamage());
            Destroy(dmg.gameObject);
        }
       
        
        if (health.ZeroHP()) {
            BossDeath death = gameObject.GetComponent<BossDeath>();
            if (death != null) {
                death.Death();
            }
            if (deathEffects != null)
            {
                deathEffects.die();
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
