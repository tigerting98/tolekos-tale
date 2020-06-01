using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDroppable : MonoBehaviour
{
    [SerializeField] Death death;
    [SerializeField] float chanceToDropCoins=0.35f;
    [SerializeField] int maxGold=100;
    [SerializeField] int minGold=10;
    [SerializeField] Coin coin;

    private void Start()
    {
        if (death) {
            death.OnDeath += DropItems;
        }
    }
    void DropItems() {
        
        if (Random.Range(0f, 1f) <= chanceToDropCoins) {
            Coin coin = Instantiate(this.coin, transform.position, Quaternion.identity);
            coin.goldAmount = Random.Range(minGold, maxGold + 1);
        }
    
    }
}
