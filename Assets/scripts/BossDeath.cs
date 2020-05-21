using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour
{
    [SerializeField] SceneLoader loader;
    // Start is called before the first frame update
    public void Death()
    {
        Debug.Log("died");
        loader.StartGame("Victory");
    }


}
