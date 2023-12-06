using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Calculator : MonoBehaviour
{
    // Start is called before the first frame update
    public int CalculateCriticalDamage(int damage, float mutiplyCriticalDamage, float chanceCriticalDamage)
    {
        Debug.Log(chanceCriticalDamage + "Change Critical Damage");
        int radNumber = UnityEngine.Random.Range(1, 100);
        if (radNumber < chanceCriticalDamage)
        {
            damage = Convert.ToInt32(damage * mutiplyCriticalDamage);

        }
        else
        {
            damage = damage;
        }
        return damage;
    }
    public int IncreaseMaxHealth(int Health)
    {
        Health = Health + 10;
        return Health;
    }    
    public int DamageLevelUp(int damageLevel)
    {
        damageLevel++;
        return damageLevel;
    }
    public int FireRateLevelUp(int fireRateLevel)
    {
        fireRateLevel++;
        return fireRateLevel;
    }
    public int BulletLevelUp(int numberBullet)
    {
        numberBullet++;
        return numberBullet;
    }

}
