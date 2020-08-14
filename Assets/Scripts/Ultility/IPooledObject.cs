using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This is for items that uses the object pools
public interface IPooledObject 
{
    // Start is called before the first frame update
    void Deactivate();
    void OnSpawn();
}
