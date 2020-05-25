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
    public float moveSpeed;
    public Health health;
    public Death deathEffects;
    public Movement movement;
    public Shooting shooting;

    // Start is called before the first frame update
   
    void Start()
    {
        
       // transform.position = waypoints[0].transform.position;
       // movement.StopMoving();

    }
    
    public void SetWaypoints(List<Transform> waypoint) {

        waypoints = waypoint;
        
    }

    public void SetSpeed(float speed) {
        moveSpeed = speed;
    }

    public void DestroyAfter(float seconds) {
        StartCoroutine(DestoryAt(seconds));
    }

    IEnumerator DestoryAt(float seconds) {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
       /*
        if (!movement.IsMoving()){ 
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
                movement.MoveTo(waypoints[cur].position, moveSpeed);
            }
                

            



        }*/
    }
}
