using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float time;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Bullet>()) {
            Destroy(other.gameObject);
        }
    }
    public void Deactivate()
    {
        Destroy(gameObject);
    }
}
