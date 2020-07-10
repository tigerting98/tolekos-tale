using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Movement))]
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
    private void Start()
    {      
        if (!movement) {
            movement = GetComponent<Movement>();
        }
        movement.destroyBoundary = 10f;
        movement.SetAcceleration(new Vector2(0, initialSpeed),
            time => time < (initialSpeed + endSpeed) / gravity ? new Vector2(0, -gravity) : new Vector2(0, 0));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collect();
        if (collectSFX) {
            AudioManager.current.PlaySFX(collectSFX);
        }
        Destroy(gameObject);
    }

    protected virtual void Collect(){
            
    }
    private void Update()
    {
        OutOfBound();
    }
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
