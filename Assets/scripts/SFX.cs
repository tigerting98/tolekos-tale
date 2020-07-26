using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
[CreateAssetMenu(fileName = "SFX/sound", menuName = "Create SFX")]
//This is an sfx file which contains the clip to be played, the volume and its cooldown
public class SFX : ScriptableObject
{
    public AudioClip clip;
    public float volume = 0.1f;
    public string id;
    public float cooldownTime = 0.01f;

    public void PlayClip() {
        AudioSource.PlayClipAtPoint(clip, GameManager.maincamera.transform.position, volume);
    
    }
}
