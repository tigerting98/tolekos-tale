using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class FinalBossFightBackground : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
   float time = 0;
     float angle = 0;
   float scale = 1;
    float opacity = 150f;
    public float defaultopacity = 150f;
    public float opacityamp = 50f;
    public float opacityfreq = 0.1f;
    public float angularvel = 50f;
    public float amp = 0.2f;
    public float ampfreq= 0.3f;
    private void Start()
    {
        opacity = defaultopacity;
    }
    private void FixedUpdate()
    {
        time += Time.deltaTime;
        angle = Functions.modulo(time * angularvel, 360);
        scale = 1 + amp * Mathf.Sin(ampfreq * 2 * Mathf.PI * time);
        opacity = defaultopacity + opacityamp*Mathf.Sin(opacityfreq * 2 * Mathf.PI * time);
        transform.localScale = new Vector2(scale, scale);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        sprite.color = new Color(255, 255, 255, opacity/255);
    }
}
