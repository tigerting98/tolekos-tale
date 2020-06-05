using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] new string name = "Boss";
    BossHealthBar bar;
    public BossHealth bosshealth;


   
    public override void Start()
    {
        base.Start();
        bar = GameManager.bossHealthBar;
        bar.SetVisible();
        bar.SetTitle(name);
        bar.SetHealth(gameObject.GetComponent<Health>());
        bar.bosshealth = bosshealth;
      
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        if (bar) {
            bar.SetInvisible();
        }
    }
}
