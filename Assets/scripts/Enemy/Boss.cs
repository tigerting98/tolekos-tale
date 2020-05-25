using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] string name = "Boss";
    BossHealthBar bar;
    void Start()
    {
        bar = GameManager.bossHealthBar;
        
            
        bar.SetVisible();
        bar.SetTitle(name);
        bar.SetHealth(gameObject.GetComponent<Health>());
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        if (bar) {
            bar.SetInvisible();
        }
    }
}
