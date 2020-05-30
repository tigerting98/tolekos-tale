using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScene : MonoBehaviour
{
    [SerializeField] Text endText = default;
    [SerializeField] Color defeat = default, victory = default;
    private void Awake()
    {
        endText.text = GameManager.victory ? "Victory!!" : "YOU DIED";
        endText.color = GameManager.victory ? victory : defeat;
    }
}
