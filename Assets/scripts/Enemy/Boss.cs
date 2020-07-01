using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] new string name = "Boss";
    protected BossHealthBar bar;
    public BossHealth bosshealth;
    public Collider2D hitbox;

   
    public override void Start()
    {
        base.Start();
        hitbox = gameObject.GetComponent<Collider2D>();
      
    }

    // Update is called once per frame

    protected void SetUpBar() {
        bar.SetVisible();
        bar.SetTaker(gameObject.GetComponent<DamageTaker>());
        bar.SetTitle(name);
        bar.SetHealth(gameObject.GetComponent<Health>());
        bar.bosshealth = bosshealth;
    }
    public override void OnEnable()
    {
        base.OnEnable();
        bar = GameManager.bossHealthBar;
        SetUpBar();
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
