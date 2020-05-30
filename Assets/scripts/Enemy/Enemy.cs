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

    public float moveSpeed;
    public Health health;
    public Death deathEffects;
    public Movement movement;
    public Shooting shooting;

    // Start is called before the first frame update
   
    void Start()
    {
        


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
      
        
    }
}
