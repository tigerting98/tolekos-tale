using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5Boss : Boss
{
    // Start is called before the first frame update
    public override void OnEnable()
    {
        GameManager.enemies.Add(gameObject.GetInstanceID(), gameObject);
        SetUpBar();

    }
    public void SetUpBossHealthbar(BossHealthBar bar) {
        this.bar = bar;
        SetUpBar();
    }
}
