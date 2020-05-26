using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageTaker : DamageTaker
{
    [SerializeField] AudioClip hitSound = default;
    [SerializeField] float volume = 0.1f;
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        AudioSource.PlayClipAtPoint(hitSound, GameManager.mainCamera.transform.position, volume);
    }
}
