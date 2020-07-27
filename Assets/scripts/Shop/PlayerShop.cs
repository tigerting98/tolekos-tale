using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//Control the stats displayed of a the player in the shop
public class PlayerShop : MonoBehaviour
{
    List<GameObject> bombs = new List<GameObject>();
    [SerializeField] GameObject bombLocation;
    [SerializeField] GameObject bombImage;
    [SerializeField] Slider healthbar;
    [SerializeField] Text healthtext;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI playerStatsText;
    [SerializeField] TextMeshProUGUI playerlevel;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < PlayerStats.maxBomb; i++) {
            bombs.Add(Instantiate(bombImage, bombLocation.transform));
        }

        Refresh();
        playerlevel.text = "Level: " + PlayerStats.playerLevel;
    }

    public void Refresh() {
        for (int i = 0; i < PlayerStats.maxBomb; i++)
        {
            bombs[i].SetActive(i < PlayerStats.bombCount);
        }
        healthtext.text = ((int)PlayerStats.playerCurrentHP).ToString() + "/" + ((int)PlayerStats.playerMaxHP).ToString();
        healthbar.value = PlayerStats.playerCurrentHP / PlayerStats.playerMaxHP;
        goldText.text = "Gold: " + PlayerStats.gold.ToString();
        playerStatsText.text = string.Format("Attack Upgrade Level: {4}\nAttack Damage Multiplier: {5}\nHarden Skin Level: {0}\nDamage Reduction: {1} %\nBomb Upgrade Level: {2}\n" +
            "Bomb Damage: {3}\nHit Barrier Upgrade Level: {6}\nHit Barrier Radius: {7}"
            , PlayerStats.hardenSkinLevel, (int)(100*(1- PlayerStats.damageMultiplier)), PlayerStats.bombLevel, PlayerStats.bombDamage*PlayerStats.bombEffectiveness,
            PlayerStats.shotDamageUpgradeLevel, PlayerStats.shotDamageMultiplier, PlayerStats.hitBarrierLevel, PlayerStats.hitBarrierRadius);


    }
}
