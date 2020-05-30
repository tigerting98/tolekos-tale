using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : Death
{
    // Start is called before the first frame update
    public override void Die()
    {
        base.Die();
        GameManager.player = null;
        GameManager.InvokeGameOverEvent(false);
    }
}
