using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject maxHealthBar;
    public GameObject healthBar;

    public void Start()
    {
        
    }
 
    //currentHealthBar.transform.localScale = new Vector3(currentHealthBarSize, 0.1075f,1);
    public void FollowShip()
    {
        //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }    
}
