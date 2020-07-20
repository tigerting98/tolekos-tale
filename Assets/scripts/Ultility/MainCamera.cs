using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 origin = new Vector3(2, 0, -10);
    private void Awake()
    {
        GameManager.maincamera = this;
    }

    public IEnumerator ShakeCamera(float magnitude, float time)
    {
        Invoke("Reset", time + 0.02f);
        float elapsedtime = 0;
        while (elapsedtime < time) {
            transform.position = origin + new Vector3(Random.Range(-magnitude, magnitude), Random.Range(-magnitude, magnitude),0);
            elapsedtime += Time.deltaTime;
            yield return null;
        }
        transform.position = origin;
    
    }

    public void Reset()
    {
        transform.position = origin;
    }
    
}
