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
    public DamageTaker damagetaker;
    public Collider2D collider;

    // Start is called before the first frame update

    private void Awake()
    {
        GameManager.enemies.Add(gameObject.GetInstanceID(), gameObject);
        movement.SetDestroyWhenOut(false);
    }
    
    private void OnDestroy()
    {
        GameManager.enemies.Remove(gameObject.GetInstanceID());
    }

    private void Start()
    {
        if (!collider) { 
            collider = GetComponent<Collider2D>(); 
        }
        collider.enabled = false;
        Hittable();
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
        Hittable();
        
    }

    void Hittable() {
        if (transform.position.x < 4.2 && transform.position.x > -4.2 && transform.position.y < 4.2 && transform.position.y > -4.2) {
            collider.enabled = true;
        }
    
    }
}
