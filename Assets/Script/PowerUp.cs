using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject Player;
    public PlayerController _playerController;
    public PowerUpData _PowerUpData;
    public virtual void Start()
    {
        Player = GameObject.Find("Player");
        _playerController = Player.GetComponent<PlayerController>();
    }
    public void powerUP()
    {
        StartCoroutine(StartPowerUp());
    }
    IEnumerator StartPowerUp()
    {
        _playerController.fireRateBoosted = _PowerUpData.fireRate;
        _playerController.bulletSpeedBoosted = _PowerUpData.bulletSpeed;
        _playerController.damageBoosted = _PowerUpData.damage;
        _playerController.splashRangeBoosted = _PowerUpData.splashRange;
        _playerController.health = _playerController.maxHealth;
       GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        Player.GetComponent<SpriteRenderer>().color = Color.yellow;
        yield return new WaitForSeconds(_PowerUpData.powerupLasted);
        _playerController.fireRateBoosted = 0;
        _playerController.bulletSpeedBoosted = 0;
        _playerController.damageBoosted = 0;
        _playerController.splashRangeBoosted = 0;
        Player.GetComponent<SpriteRenderer>().color = Color.white;
        Destroy(this.gameObject);

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("COLLIDER WITH POWER UP");
        if(other.gameObject.tag=="Player")
        {
            powerUP();
            
        }    
    }
}
