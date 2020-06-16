using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Collectible))]
public class Coin : MonoBehaviour
{
    [SerializeField] Collectible collectible;
    public int goldAmount;
    void Start()
    {
        collectible.Collect += this.Collect;
        
    }
    void Collect()
    {

        PlayerStats.AddGold(goldAmount);
    }
    // Update is called once per frame
    private void OnDestroy()
    {
        collectible.Collect -= this.Collect;
    }
}
