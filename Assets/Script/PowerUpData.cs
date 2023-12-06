using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create PowerUp", fileName = "PowerUpData")]
public class PowerUpData : ScriptableObject
{
    public float fireRate;
    public int damage;
    public int bulletSpeed;
    public int splashRange;
    public int powerupLasted;
    
}
