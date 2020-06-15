using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Functions : MonoBehaviour
{
    // Start is called before the first frame update
    public static IEnumerator RepeatAction(Action action, float shotRate)
    {
        return RepeatCustomAction(i => action(), shotRate);

    }

    public static T StartMultipleCustomCoroutines<T>(T obj, Func<int, IEnumerator> subpattern, int times) where T : MonoBehaviour {
        for (int i = 0; i < times; i++) {
            obj.StartCoroutine(subpattern(i));
        }
        return obj;
    }

    public static T StartMultipleCustomCoroutines<T>(T obj, Func<int, IEnumerator> subpattern, int times, float delay) where T : MonoBehaviour {

        obj.StartCoroutine(MultipleCustomCorountines(obj, subpattern, times, delay));


        return obj;
    }

    public static IEnumerator MultipleCustomCorountines<T>(T obj, Func<int, IEnumerator> subpattern, int times, float delay) where T : MonoBehaviour { 
        for (int i = 0; i < times; i++)
        {
            obj.StartCoroutine(subpattern(i));
            yield return new WaitForSeconds(delay);
        }
    }
    public static IEnumerator RepeatActionXTimes(Action action, float shotRate, int x)
    {
        return RepeatCustomActionXTimes(i => action(), shotRate, x);

    }

    public static IEnumerator RepeatCustomAction(Action<int> action, float shotRate) {
        return RepeatCustomActionCustomTime(action, i => shotRate);
    }

    public static IEnumerator RepeatCustomActionXTimes(Action<int> action, float shotRate, int x)
    {
        for (int i = 0; i < x; i++)
        {
            action(i);
            yield return new WaitForSeconds(shotRate);
        }
    }

    public static IEnumerator RepeatCustomActionCustomTime(Action<int> action, Func<int, float> shotRateFunction) {
        float timer = 0;
        int i = 0;

        while (true)
        {
            
            action(i);
            float nextShotRate = shotRateFunction(i);
            i++;
            timer += nextShotRate;
            yield return new WaitForSeconds(nextShotRate);
        }
    }
    
}
