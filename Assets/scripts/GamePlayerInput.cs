using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This controls player input for dialogue
public class GamePlayerInput : MonoBehaviour
{
    public event Action OnHoldZ;
    public event Action OnPressZ;
    public event Action OnPressShift;
    public event Action OnPressX;
    public event Action OnPressC;
    public event Action OnPressEnter;

    private void Awake()
    {
        if (GameManager.gameInput == null)
        {
            GameManager.gameInput = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) {
            OnPressZ?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Return)) {
            OnPressEnter?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            OnPressC?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            OnPressX?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            OnPressShift?.Invoke();
        }
        if (Input.GetKey(KeyCode.Z))
        {
            OnHoldZ?.Invoke();
        }
    }
   
}
