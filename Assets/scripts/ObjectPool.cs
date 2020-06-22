using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//this is an generic implementation of object pooling
public abstract class ObjectPool<T> : MonoBehaviour where T : Component
{
    Queue<T> objectPool = new Queue<T>();
    //object prefab from which all instances are pooled form
    public T pooledObject;

    //retrieves a new object from the pool
    public T SpawnFromPool(Vector3 position, Quaternion rotation) {
        if (objectPool.Count == 0)
        {
            T obj =  Instantiate(pooledObject, position, rotation);
            obj.GetComponent<IPooledObject>().OnSpawn();
            return obj;
        }
        else {
            T obj = objectPool.Dequeue();
            if (obj == null || obj.gameObject.activeInHierarchy)
            {
                return SpawnFromPool(position, rotation);
            }
            else {

                obj.transform.position = position;
                obj.transform.rotation = rotation;
                obj.gameObject.SetActive(true);
                obj.GetComponent<IPooledObject>().OnSpawn();
                return obj;
            }
        
        }
    }

    //return an object back to its pool
    public void ReturnToPool(T tobeReturned) {
        tobeReturned.gameObject.SetActive(false);
        objectPool.Enqueue(tobeReturned);
    }
}
