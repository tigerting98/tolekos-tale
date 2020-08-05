using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class GoldText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gold = default;
    [SerializeField] TextMeshProUGUI goldtext;
    int i = -1;
    // Start is called before the first frame update
    void Start()
    {   
        gold.text = string.Format("Gold: {0}g", PlayerStats.gold);
        PlayerStats.OnGainGold += UpdateGold;
    }

    // Update is called once per frame
    void UpdateGold(int amount)
    {
        i = (i + 1) % 5;
        TextMeshProUGUI text = Instantiate(goldtext, (Vector2)transform.position + new Vector2(-150+i*65,25f), Quaternion.identity, transform);
        text.text = "+ " + amount.ToString() + "g";

        Movement movement = text.GetComponent<Movement>();
        movement.SetSpeed(50f, 90);

        Destroy(text.gameObject, 2.1f);
        gold.text = string.Format("Gold: {0}g", PlayerStats.gold);
    }

    private void OnDestroy()
    {
        PlayerStats.OnGainGold -= UpdateGold;
    }
}
