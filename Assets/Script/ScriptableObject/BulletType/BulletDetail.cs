using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create BulletDetail", fileName = "BulletDetail")]
public class BulletDetail : ScriptableObject
{
  
    [Serializable]
    public class BulletStats
    {
        public BulletType _bulletType;
        public int id;
        public int bulletDamage;
        public int bulletSpeed;
        public float fireRate;
        public int splashRange;

    }
    public List<BulletStats> bulletStats;
    
}
public enum BulletType
{
    Normal,
    Boom,
    Boomerang
}


