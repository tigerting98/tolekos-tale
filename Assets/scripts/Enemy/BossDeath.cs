using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDeath : Death
{
    
    // Start is called before the first frame update
    public override void Die()
    {
        base.Die();
        GameManager.InvokeGameOverEvent(true);
    }


}
