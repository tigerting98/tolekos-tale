using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Coroutine StartShooting(IEnumerator pattern) {
        Coroutine firing = StartCoroutine(pattern);
  
        return firing;
    }

    public Coroutine ShootWhenInBound(IEnumerator pattern, float checkInterval = 0.1f) {
        Coroutine firing = StartCoroutine(ShootInBound(pattern, checkInterval));
;
        return firing;
    }

    public IEnumerator ShootInBound(IEnumerator pattern, float checkInterval) {
        bool shooting = false;
        Coroutine firing = null;
        while (!shooting) {
            if (InBound()) {
                shooting = true;
                firing = StartCoroutine(pattern);

            }
            yield return new WaitForSeconds(checkInterval);
        
        }
        while (shooting) {
            if (!InBound()) {
                shooting = false;
                StopCoroutine(firing);
   
            }
            yield return new WaitForSeconds(checkInterval);
        
        }
    
    }

    public Coroutine PlayAudio(AudioClip clip, float interval, float volume, float start) {
        return StartCoroutine(PlaySoundAfter(clip, interval, volume, start));
    }
    public Coroutine StartShootingAfter(IEnumerator pattern, float time) {
        return StartCoroutine(StartAfter(pattern, time));
        
    }

    public Coroutine StartShootingFor(IEnumerator pattern, float startTime, float duration) {
        return StartCoroutine(StartAndStopAfter(pattern, startTime, duration));

    }

    IEnumerator StartAfter(IEnumerator function, float time) {
        yield return new WaitForSeconds(time);
        yield return StartCoroutine(function); 

    }

    IEnumerator StartAndStopAfter(IEnumerator function, float time, float duration) {
        yield return new WaitForSeconds(time);
        Coroutine fire = StartCoroutine(function);
        yield return new WaitForSeconds(duration);
        StopCoroutine(fire);
        yield return null;
    
    }

    public IEnumerator PlaySoundAfter(AudioClip clip, float interval, float volume, float start) {
        yield return new WaitForSeconds(start);
        while (true)
        {
            AudioSource.PlayClipAtPoint(clip, GameManager.mainCamera.transform.position, volume);
            yield return new WaitForSeconds(interval);
        }
    }

    public bool InBound() {
        return transform.position.x < 4 && transform.position.x > -4 && transform.position.y > -4 && transform.position.y > -4;


    }
}
