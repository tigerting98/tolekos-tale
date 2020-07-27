using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This component encompass the drops an enemy would drop in the game
public class BasicDroppable : MonoBehaviour
{
    [SerializeField] Death death;
    public float chanceToDropCoins=0.35f;
    public int maxGold=100;
    public int minGold=10;
    bool set = false;
    int goldDrop;
    bool drop;
    [SerializeField] Coin coin;
    public List<Collectible> otherDrops = new List<Collectible>();
    private void Start()
    {
        if (death) {
            death.OnDeath += DropItems;
        }
        if (!set)
        {
            SetDrop();
        }
    }
    public void SetDrop() {
        drop = GameManager.SupplyRandomFloat() <= chanceToDropCoins;
        goldDrop = GameManager.SupplyRandomInt(minGold, maxGold + 1);
        set = true;
    }
    void DropItems() {
        
        if (drop) {
            Coin coin = Instantiate(this.coin, transform.position, Quaternion.identity);
            coin.goldAmount = goldDrop;
        }
        if (otherDrops.Count > 0) {
            foreach (Collectible otherDrop in otherDrops) {
                Instantiate(otherDrop,transform.position,Quaternion.identity);
            }
        }

    
    }
}
