using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    Material background;
    [SerializeField] float speed = default;
    
    // Start is called before the first frame update
    void Start()
    {
        background = gameObject.GetComponent<Renderer>().material;
     
    }

    // Update is called once per frame
    void Update()
    {
        background.mainTextureOffset += Time.deltaTime * new Vector2(0, speed);
    }
}
