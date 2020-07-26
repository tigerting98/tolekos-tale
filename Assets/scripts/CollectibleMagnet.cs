using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This class is used to suck in collectibles into the player
public class CollectibleMagnet : MonoBehaviour
{
    [SerializeField] float speed;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Collectible collectible)) {
            if (collectible.TryGetComponent(out Movement movement)) {
                movement.Homing(GameManager.player.gameObject, speed);
            }
        } 
    }
}
