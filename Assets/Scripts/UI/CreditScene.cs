using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This is used to control the credit scene. Allows player to move on after the credits is finished flashing
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
