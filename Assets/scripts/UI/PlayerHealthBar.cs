using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : HealthBar
{
    // Start is called before the first frame update
    public override void Start()
    {
        SetVisible();
        SetHealth(GameManager.player.health);
    }

    // Update is called once per frame

}
