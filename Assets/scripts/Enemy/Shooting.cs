using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Coroutine StartShooting(IEnumerator pattern) {
        Coroutine firing = StartCoroutine(pattern);
        return firing;
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
        yield return StartCoroutine(function);
        yield return new WaitForSeconds(duration);
        yield return null;
    
    }

   

}
