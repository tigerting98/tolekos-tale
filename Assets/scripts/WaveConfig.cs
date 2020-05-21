using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Create wave")]
public class WaveConfig : ScriptableObject
{
    public GameObject path;
    public Enemy enemy;
    public float spawntime;
    public float number;

    public float moveSpeed;


    public List<Transform> GetWaypoints() {
        List<Transform> lst = new List<Transform>();
        foreach (Transform child in path.transform) {
          
            lst.Add(child);

        }
        return lst;

    }
}
