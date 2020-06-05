using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GameManager.gameInput = this;
    }
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) {
            OnPressZ?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter)) {
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
    private void OnDestroy()
    {
        GameManager.gameInput = null;
    }
}
