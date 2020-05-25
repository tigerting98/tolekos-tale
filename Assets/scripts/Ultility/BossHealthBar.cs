using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : HealthBar
{
    // Start is called before the first frame update
    private void Awake()
    {
        GameManager.bossHealthBar = this;
    }
}
