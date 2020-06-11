using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShop : MonoBehaviour
{
    List<GameObject> bombs = new List<GameObject>();
    [SerializeField] GameObject bombLocation;
    [SerializeField] GameObject bombImage;
    [SerializeField] Slider healthbar;
    [SerializeField] Text healthtext;
    [SerializeField] TextMeshProUGUI goldText;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < PlayerStats.maxBomb; i++) {
            bombs.Add(Instantiate(bombImage, bombLocation.transform));
        }

        Refresh();
    }

    public void Refresh() {
        for (int i = 0; i < PlayerStats.maxBomb; i++)
        {
            bombs[i].SetActive(i < PlayerStats.bombCount);
        }
        healthtext.text = ((int)PlayerStats.playerCurrentHP).ToString() + "/" + ((int)PlayerStats.playerMaxHP).ToString();
        healthbar.value = PlayerStats.playerCurrentHP / PlayerStats.playerMaxHP;
        goldText.text = "Gold: " + PlayerStats.gold.ToString();
 
    }
}
