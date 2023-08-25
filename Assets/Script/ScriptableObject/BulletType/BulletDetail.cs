using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create BulletDetail", fileName = "BulletDetail")]
public class BulletDetail : ScriptableObject
{
    public int bulletDamage;
    public int bulletSpeed;
    public float fireRate;

}
