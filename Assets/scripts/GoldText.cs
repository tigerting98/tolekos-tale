using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(TextMeshProUGUI))]
public class GoldText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gold = default;
    
    // Start is called before the first frame update
    void Start()
    {   
        gold.text = string.Format("Gold: {0}g", PlayerStats.gold);
        PlayerStats.OnGainGold += UpdateGold;
    }

    // Update is called once per frame
    void UpdateGold()
    {
        gold.text = string.Format("Gold: {0}g", PlayerStats.gold);
    }

    private void OnDestroy()
    {
        PlayerStats.OnGainGold -= UpdateGold;
    }
}
