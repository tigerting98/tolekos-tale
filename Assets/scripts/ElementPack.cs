using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementPack<T> : ScriptableObject
{
    // Start is called before the first frame update
    [SerializeField] protected List<T> objects = new List<T>(4);
    public T GetItem(int i)
    {
        return objects[i];
    }


    public T GetItem(DamageType type)
    {
        switch (type)
        {
            case DamageType.Pure:
                return GetItem(3);
            case DamageType.Water:
                return GetItem(0);
            case DamageType.Earth:
                return GetItem(1);
            case DamageType.Fire:
                return GetItem(2);
            default:
                return GetItem(0);
        }
    }



    public List<T> GetAllItems()
    {
        return objects;

    }
}
