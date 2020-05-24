using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] string name = "Boss";
    [SerializeField] HealthBar bar;
    void Start()
    {
        bar = GameObject.Find("Canvas/stats UI/BossHealthBar").GetComponent<HealthBar>();
        if (bar != null)
        {
            
            bar.SetVisible();
            bar.SetTitle(name);
            bar.SetHealth(gameObject.GetComponent<Health>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        if (bar != null) {
            bar.SetInvisible();
        }
    }
}
