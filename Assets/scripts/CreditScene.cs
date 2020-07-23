using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditScene : MonoBehaviour
{
    bool next = false;
    [SerializeField] float delay;
    private void Update()
    {
        if (next&&(Input.GetKeyDown(KeyCode.Z)|| Input.GetKeyDown(KeyCode.Return))) {
            GameManager.sceneLoader.LoadScene("StartPage");
        }
    }
    private void Start()
    {
        Invoke("ChangeNext", delay);
    }
    void ChangeNext() {
        next = true;
    }
}
