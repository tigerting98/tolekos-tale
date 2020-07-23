using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Shop Item/Hit Barrier")]
public class HitBarrier : ShopItem
{
    [SerializeField] int hitBarrierLevel = 1;
    [SerializeField] float hitBarrierRadius = 0.9f;
    public override bool Buyable()
    {
        return base.Buyable() && PlayerStats.hitBarrierLevel == hitBarrierLevel - 1;
    }
    public override void Buy()
    {
        base.Buy();
        PlayerStats.hitBarrierLevel = hitBarrierLevel;
        PlayerStats.hitBarrierRadius = hitBarrierRadius;
    }
}
