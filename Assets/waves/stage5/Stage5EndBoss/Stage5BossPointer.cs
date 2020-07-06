using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Stage5BossPointer : MonoBehaviour
{
    public Stage5Boss trackingBoss;
    public SpriteRenderer sprite;
    // Update is called once per frame
    void Start()

    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (trackingBoss&&trackingBoss.isActiveAndEnabled)
        {
            sprite.enabled = true;
            transform.position = new Vector2(trackingBoss.transform.position.x, -4.2f);
        }
        else
        {
            sprite.enabled = false;
        }
    }
}
