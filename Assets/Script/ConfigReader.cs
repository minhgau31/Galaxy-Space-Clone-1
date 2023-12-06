using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigReader : MonoBehaviour
{
    public BulletDetail bulletDetail;
    public BulletDetail.BulletStats bulletStats;
    public Calculator _calculator;


    public BulletDetail.BulletStats GetBulletStats(int id,int teamID)
    {
        BulletDetail.BulletStats _bulletStats = new BulletDetail.BulletStats();
        for (int i = 0; i < bulletDetail.bulletStats.Count; i++)
        {
            if(id==bulletDetail.bulletStats[i].id)
            {
                switch (bulletDetail.bulletStats[i]._bulletType)
                {

                    case BulletType.Normal:
                        _bulletStats._bulletType = bulletDetail.bulletStats[i]._bulletType;
                        _bulletStats.bulletDamage = bulletDetail.bulletStats[i].bulletDamage;
                        _bulletStats.bulletSpeed = bulletDetail.bulletStats[i].bulletSpeed;
                        _bulletStats.fireRate = bulletDetail.bulletStats[i].fireRate;
                        //damage = _calculator.CalculateCriticalDamage(damage, multiplyCriticalDamage, chanceCriticalDamage);
                                            
                        break;
                    case BulletType.Boom:
                        _bulletStats._bulletType = bulletDetail.bulletStats[i]._bulletType;
                        _bulletStats.bulletDamage = bulletDetail.bulletStats[i].bulletDamage;
                        _bulletStats.bulletSpeed = bulletDetail.bulletStats[i].bulletSpeed;
                        _bulletStats.fireRate = bulletDetail.bulletStats[i].fireRate;
                       _bulletStats.splashRange = bulletDetail.bulletStats[i].splashRange;                      
                        break;
                    case BulletType.Boomerang:
                        _bulletStats._bulletType = bulletDetail.bulletStats[i]._bulletType;
                        _bulletStats.bulletDamage = bulletDetail.bulletStats[i].bulletDamage;
                        _bulletStats.bulletSpeed = bulletDetail.bulletStats[i].bulletSpeed;
                        _bulletStats.fireRate = bulletDetail.bulletStats[i].fireRate;                    
                        break;



                        
                       
                }
            }
        }
        return _bulletStats;
    }
    
}
