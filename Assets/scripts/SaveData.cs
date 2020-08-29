using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

[System.Serializable]
public class SaveData
{
    public bool[] cleared = new bool[5];
    public bool[,] acceessible = new bool[5,6];

    public SaveData() {
        for (int i = 0; i < 5; i++) {
            cleared[i] = false;
        }
        for (int i = 0; i < 5; i++)
        {
            acceessible[i,0] = true;
            for (int j = 1; j < 6; j++)
            {
                acceessible[i,j] = false;
            }
           
        }
    }

    public void print() {
        string str = "Cleared: ";
        for (int i = 0; i < 5; i++) {
            str += cleared[i].ToString() + ",";
        }
        str += "Accessible:";
        for (int i = 0; i < 5; i++)
        {
            str += i.ToString() + ":";
            for (int j = 0; j < 6; j++)
            {
                str += acceessible[i, j] + ",";
            }
            str += "   ";
        }
        Debug.Log(str);
    }
}
