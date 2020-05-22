using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour
{
    
    // Start is called before the first frame update
    public void Death()
    {
        
        FindObjectOfType<SceneLoader>().Victory();
    }


}
