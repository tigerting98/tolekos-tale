using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    [Header("Audio")]
    public SFX bulletSpawnSound = default;
    public virtual void SpawnWave() { }

    public void DestroyAfter(float sec) {
        Invoke("Destroy", sec);
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }

}
