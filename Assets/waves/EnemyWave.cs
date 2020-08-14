using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
//The base enemy wave class 
public class EnemyWave : MonoBehaviour
{
    //Method to be implemented
    public virtual void SpawnWave() { }

    public void DestroyAfter(float sec) {
        Invoke("Destroy", sec);
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }

}
