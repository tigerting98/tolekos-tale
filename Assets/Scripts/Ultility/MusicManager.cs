using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    float volume = 1;
    public AudioSource source;
    bool fadeOut;
    MusicTrack track;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    public void PlayTrack(MusicTrack track) {
        if (track)
        {
            StopAllCoroutines();
            this.track = track;
            volume = 1;
            if (track.looping)
            {
                source.clip = track.introBody;
                StartCoroutine(PlayMainLoop(track.delay));
            }
            else {
                source.clip = track.mainBody;
            }
            source.volume = this.track.volume * GameManager.musicVolume * volume;
            source.Play();
        }
    }
    public IEnumerator PlayMainLoop(float delay) {
        if (track)
        {
            yield return new WaitForSeconds(delay);
            source.clip = track.mainBody;
            source.Play();
        }
    }
    public void SetVolume() {
        if (track)
        {
            source.volume = track.volume * GameManager.musicVolume * volume;
        }
    }
    public void ChangeTrack(MusicTrack track) {
        if (track)
        {
            StopAllCoroutines();
            StartCoroutine(Change(track));
        }
    }
     IEnumerator Change(MusicTrack track) {
        
        fadeOut = true;
        yield return new WaitForSeconds(0.4f);
        fadeOut = false;
        PlayTrack(track);

    }
    private void Update()
    {
        if (fadeOut) {
            if (volume <= 0)
            {
                fadeOut = false;
            }
            else {
                volume -= Time.deltaTime*3f;
                if (track)
                { source.volume = track.volume * GameManager.musicVolume * volume; }
            }
        }
    }
}
