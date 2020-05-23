using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        GameManager.mainCamera = gameObject.GetComponent<Camera>();
    }
}
