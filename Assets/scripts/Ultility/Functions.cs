using System;
using System.Collections;
using UnityEditor.Build;
using UnityEngine;
//a list of useful utility functions commonly used
public class Functions : MonoBehaviour
{
    // Start is called before the first frame update
    //repeat an action some amount of time
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
    public static Vector2 RandomLocation(float minX, float maxX, float minY, float maxY) {
        return new Vector2(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY));
    }

    public static Vector2 RandomLocation(Vector2 origin, float bounds) {
        return origin + RandomLocation(-bounds, bounds, -bounds, bounds);
    }

    public static GameObject GetNearestEnemy(Vector2 origin)
    {
        GameObject obj = null;
        float distance = Mathf.Infinity;
        foreach (GameObject item in GameManager.enemies.Values)
        {
           
            Vector2 pos = item.transform.position;
                if (WithinBounds(pos, 4.1f))
                {
                    float dist = (pos - origin).magnitude;

                    if (dist < distance)
                    {
                        distance = dist;
                        obj = item;

                    }
                } 
        }

        

        return obj;
    }

    public static float AimAt(Vector2 shooter, Vector2 target)
    {

        Vector2 diff = target - shooter;
        return Mathf.Rad2Deg * Mathf.Atan2(diff.y, diff.x);
    }

    public static bool WithinBounds(Vector2 pos, float minx, float maxx, float miny, float maxy) {
        return pos.x < maxx && pos.x > minx && pos.y < maxy && pos.y > miny;
    }

    public static bool WithinBounds(Vector2 pos, float x, float y) {
        return WithinBounds(pos, -x, x, -y, y);
    }

    public static bool WithinBounds(Vector2 pos, float i) {
        return WithinBounds(pos, i, i);
    }

    public static DamageType RandomType(bool includePure) {

        int x = UnityEngine.Random.Range(0, includePure ? 4 : 3);
        switch (x) {
            case 0:
                return DamageType.Water;
            case 1:
                return DamageType.Earth;
            case 2:
                return DamageType.Fire;
            case 3:
                return DamageType.Pure;
            default:
                return DamageType.Pure;
        }
    }

    public static float modulo(float number, float mod) {
        if (number < 0)
        {
            return mod - modulo(Math.Abs(number), mod);
        }
        else if (number >= mod)
        {
            return number - mod * Mathf.FloorToInt(number / mod);
        }
        else {
            return number;
        }
    }
}
