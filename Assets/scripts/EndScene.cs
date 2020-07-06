using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI endText = default;
    [SerializeField] Color defeat = default, victory = default;
    private void Awake()
    {
        endText.text = PlayerStats.deathCount==0 ? "Victory!!" : "BAD END \n Try to Clear without Dying!";
        endText.color = PlayerStats.deathCount == 0 ? victory : defeat;
    }
}
