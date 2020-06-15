﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator PlaySoundAfter(SFX sfx, float interval, float start)
    {
        yield return new WaitForSeconds(start);
        while (true)
        {
            sfx.PlayClip();
            yield return new WaitForSeconds(interval);
        }
    }
    IEnumerator PlaySoundForXTimePulses(SFX sfx, float interval, int number, float pulse) {
        while (true) {
            yield return PlaySoundForTimes(sfx, interval, number);
            yield return new WaitForSeconds(pulse - interval*number);
        }
    }
    IEnumerator PlaySoundForTimes(SFX sfx, float interval, int times) {
        for (int i = 0; i < times; i++) {
            sfx.PlayClip();
            yield return new WaitForSeconds(interval);
        }
    
    }
    public Coroutine PlayAudioForXTimeAndPause(SFX sfx, float interval, int number, float pause) {
        return StartCoroutine(PlaySoundForXTimePulses(sfx, interval, number,pause));
    }
    public Coroutine PlayAudioTimes(SFX sfx, float interval, int times) {
        return StartCoroutine(PlaySoundForTimes(sfx, interval, times));
    
    }

    public Coroutine PlayAudioDuration(SFX sfx, float interval, float duration) {
        return StartCoroutine(PlaySoundForTimes(sfx, interval, (int)(duration/interval)+1));

    }

    public Coroutine PlayAudio(SFX sfx, float interval, float start)
    {
        return StartCoroutine(PlaySoundAfter(sfx, interval, start));
    }
}
