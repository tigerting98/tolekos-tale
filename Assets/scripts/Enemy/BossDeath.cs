using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDeath : Death
{

    // Start is called before the first frame update

    [SerializeField] bool endBoss = false;
    public override void Die()
    {
        base.Die();
        if (endBoss)
        {
            GameManager.InvokeGameOverEvent(true);
        }
    }


}
