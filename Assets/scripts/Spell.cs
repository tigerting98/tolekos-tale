using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    [SerializeField] float volume = 0.2f;
    public float invulTimer;
    void Start()
    {
        AudioSource.PlayClipAtPoint(clip, GameManager.mainCamera.transform.position, volume);
        DestroyAfter(invulTimer + 2f);
    }

    IEnumerator DestroyAfter(float seconds) {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    
    }
}
