using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public event Action Collect;
    public SFX collectSFX;
    public Movement movement;
    [SerializeField] float initialSpeed;
    [SerializeField] float gravity;

    private void Awake()
    {
        GameManager.collectibles.Add(GetInstanceID(), gameObject);
    }
    private void Start()
    {
        movement.destroyBoundary = 10f;
        movement.SetAcceleration(new Vector2(0, initialSpeed),
            time => time < initialSpeed * 2 / gravity ? new Vector2(0, -gravity) : new Vector2(0, 0));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collect?.Invoke();
        if (collectSFX) {
            collectSFX.PlayClip();
        }
        Destroy(gameObject);
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
