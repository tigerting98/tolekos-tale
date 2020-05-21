using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Camera cam; 
    [SerializeField] int damage;
    UnityEngine.Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        
    }

    public int TakeDamage() {

        return this.damage;
    }

    public void setSpeed(UnityEngine.Vector2 vel) {
        velocity = vel;
        GetComponent<Rigidbody2D>().velocity = vel;
    }

    // Update is called once per frame
    void Update()
    {   
        UnityEngine.Vector3 pos = cam.WorldToViewportPoint(transform.position);
        if (pos.x < 0 || pos.x > 1 || pos.y < 0 || pos.y > 1) {
            Destroy(gameObject);
        }
        
    }
}
