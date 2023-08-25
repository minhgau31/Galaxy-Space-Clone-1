using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject maxHealthBar;
    public GameObject healthBar;
    public GameObject stickedShip;
    public float Health = 0;
    public float MaxHealth = 0;
    public float HealthBarSize1;

    public void Start()
    {
        
    }
    public void Init(GameObject _stickedShip)
    {
        stickedShip = _stickedShip;               
        Debug.Log("alo");
        HealthBarSize1 = 1f;
        healthBar.transform.localScale = new Vector3(HealthBarSize1, 0.10751f, 1f);
    }

    public void Update()
    {
        
        FollowShip();
       


    }
    public void FollowShip()
    {
        transform.position = new Vector3(stickedShip.transform.position.x, stickedShip.transform.position.y-0.2f, stickedShip.transform.position.z);
    }    
    public void HealthBarSize(float maxHealth, float health)
    {
        Health = health;
        MaxHealth = maxHealth; 
        HealthBarSize1 = Health / MaxHealth;         
        healthBar.transform.localScale = new Vector3(HealthBarSize1, 0.10751f, 1f);
       
        
    }

    
}
