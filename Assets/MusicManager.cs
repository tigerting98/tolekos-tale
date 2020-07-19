using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    float volume = 1;
    public AudioSource source;
    bool fadeOut;
    SFX sfx;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    public void PlayTrack(SFX sfx) {
        if (sfx)
        {
            this.sfx = sfx;
            volume = 1;
            source.clip = sfx.clip;
            source.volume = this.sfx.volume * GameManager.musicVolume * volume;
            source.Play();
        }
    }
    public void SetVolume() {
        if (sfx)
        {
            source.volume = sfx.volume * GameManager.musicVolume * volume;
        }
    }
    public Coroutine ChangeTrack(SFX sfx) {
        if (sfx)
        {
            return StartCoroutine(Change(sfx));
        }
        return null;
    }
     IEnumerator Change(SFX sfx) {
        
        fadeOut = true;
        yield return new WaitForSeconds(1.2f);
        fadeOut = false;
        PlayTrack(sfx);

    }
    private void Update()
    {
        if (fadeOut) {
            if (volume <= 0)
            {
                fadeOut = false;
            }
            else {
                volume -= Time.deltaTime;
                if (sfx)
                { source.volume = sfx.volume * GameManager.musicVolume * volume; }
            }
        }
    }
}
