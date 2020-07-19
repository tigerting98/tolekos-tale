using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Create Music Track")]
public class MusicTrack : ScriptableObject
{
    public float volume;
    public bool looping = false;
    public AudioClip mainBody, introBody;
    public float delay;
}
