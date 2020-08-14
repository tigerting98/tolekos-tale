using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Create Music Track")]
//Stores the reference to which music track should be played and whetehr there are loops
public class MusicTrack : ScriptableObject
{
    public float volume;
    public bool looping = false;
    public AudioClip mainBody, introBody;
    public float delay;
}
