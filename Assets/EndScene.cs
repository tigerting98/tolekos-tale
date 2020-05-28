using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScene : MonoBehaviour
{
    [SerializeField] Text endText;
    private void Awake()
    {
        endText.text = GameManager.victory ? "Victory!!" : "YOU DIED";
    }
}
