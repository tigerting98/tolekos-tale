using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(TextMeshProUGUI))]
public class BombText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bomb = default;
    
    // Start is called before the first frame update
    void Start()
    {   
        bomb.text = string.Format("Bombs: {0}", PlayerStats.bombCount);
        PlayerStats.OnUseBomb += UpdateBomb;
    }

    // Update is called once per frame
    void UpdateBomb()
    {
        bomb.text = string.Format("Bombs: {0}", PlayerStats.bombCount);
    }

    private void OnDestroy()
    {
        PlayerStats.OnGainGold -= UpdateBomb;
    }
}
