using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(menuName = "Bullet Pack")]
public class BulletPack : ScriptableObject
{
    public List<Bullet> bullets;
    public float dmg = 100;

}
