using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDroppable : MonoBehaviour
{
    [SerializeField] Death death;
    public float chanceToDropCoins=0.35f;
    public int maxGold=100;
    public int minGold=10;
    [SerializeField] Coin coin;
    public List<GameObject> otherDrops = new List<GameObject>();
    private void Start()
    {
        if (death) {
            death.OnDeath += DropItems;
        }
    }
    void DropItems() {
        
        if (Random.Range(0f, 1f) <= chanceToDropCoins) {
            Debug.Log("dropped");
            Coin coin = Instantiate(this.coin, transform.position, Quaternion.identity);
            coin.goldAmount = Random.Range(minGold, maxGold + 1);
        }
        if (otherDrops.Count > 0) {
            foreach (GameObject otherDrop in otherDrops) {
                Instantiate(otherDrop);
            }
        }

    
    }
}
