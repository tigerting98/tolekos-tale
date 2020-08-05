using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Movement))]
//This set the behavior of a collectible item
public class Collectible : MonoBehaviour
{
    //public event Action Collect;
    public SFX collectSFX;
    public Movement movement;
    [SerializeField] float initialSpeed;
    [SerializeField] float gravity;
    [SerializeField] float endSpeed;
    private void Awake()
    {
        GameManager.collectibles.Add(GetInstanceID(), gameObject);
    }
    //Set its movement to go up and then accelerate downwards
    protected virtual void Start()
    {      
        if (!movement) {
            movement = GetComponent<Movement>();
        }
        movement.destroyBoundary = 10f;
        movement.SetAcceleration(new Vector2(0, initialSpeed),
            time => time < (initialSpeed + endSpeed) / gravity ? new Vector2(0, -gravity) : new Vector2(0, 0));
    }
    //Play Collection sounds and destroy itself
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<CollectibleMagnet>())
        {
            Collect();

            if (collectSFX)
            {
                AudioManager.current.PlaySFX(collectSFX);
            }
            Destroy(gameObject);
        }
    }
    //A virtual class for its children to implement
    protected virtual void Collect(){
            
    }
    private void Update()
    {
        OutOfBound();
    }
    //Dstroy it when it is no longer in screen
    void OutOfBound() {
        if (transform.position.y < -4.2 || transform.position.x > 4.2 || transform.position.x < -4.2|| transform.position.y > 10) {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        GameManager.collectibles.Remove(GetInstanceID());
    }
}
