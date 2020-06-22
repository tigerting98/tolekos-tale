using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledObject 
{
    // Start is called before the first frame update
    void Deactivate();
    void OnSpawn();
}
