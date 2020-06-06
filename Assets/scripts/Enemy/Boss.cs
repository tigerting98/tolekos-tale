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
        
      
    }


    // Update is called once per frame
    void Update()
    {
        
    }
  
    public override void OnEnable()
    {
        base.OnEnable();
        bar = GameManager.bossHealthBar;
        bar.SetVisible();
        bar.SetTitle(name);
        bar.SetHealth(gameObject.GetComponent<Health>());
        bar.bosshealth = bosshealth;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        if (bar)
        {
            bar.SetInvisible();
        }
    }

}
