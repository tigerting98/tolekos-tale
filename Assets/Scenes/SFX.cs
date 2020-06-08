using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
[CreateAssetMenu(fileName = "SFX/sound", menuName = "Create SFX")]
public class SFX : ScriptableObject
{
    public AudioClip clip;
    public float volume = 0.1f;

    public void PlayClip() {
        AudioSource.PlayClipAtPoint(clip, GameManager.mainCamera.transform.position, volume);
    
    }
}
