using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Death))]
[RequireComponent(typeof(Movement))]
public class Enemy : MonoBehaviour
{
    int cur = 0;
 
    List<Transform> waypoints;
    float moveSpeed;
    Health health;
    Death deathEffects;
    Movement movement;

    // Start is called before the first frame update
   
    void Start()
    {
        movement = GetComponent<Movement>();
        health = GetComponent<Health>();
        deathEffects = gameObject.GetComponent<Death>();
        transform.position = waypoints[0].transform.position;
        movement.StopMoving();

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
    public void SetWaypoints(List<Transform> waypoint) {

        waypoints = waypoint;
        
    }

    public void SetSpeed(float speed) {
        moveSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!movement.isMoving()){ 
            if (cur == waypoints.Count -1)
            {
                if (waypoints[cur].gameObject.tag == "End")
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                cur++;
                Vector2 diff = waypoints[cur].position - transform.position;

                float time = diff.magnitude / moveSpeed;

                movement.SetStraightPath(diff / diff.magnitude * moveSpeed);
                movement.SetStartingPoint(transform.position);
                movement.StartMoving();
                movement.StopMovingAfter(time);
            }
                

            



        }
    }
}
