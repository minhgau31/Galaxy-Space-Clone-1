using System.Collections;
using System.Collections.Generic;
using ToolBox.Pools;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;
    private int bulletSpeed;
    internal float fireRate;
    Rigidbody2D rb;
    public BulletDetail bulletDetail;
 
    public BaseShipEnemy bEnemy;
  

    // Start is called before the first frame update
    void Awake()
    {
        damage = bulletDetail.bulletDamage;
        bulletSpeed=bulletDetail.bulletSpeed;
        fireRate = bulletDetail.fireRate;

        rb= GetComponent<Rigidbody2D>();
      
        
    }
    private void OnEnable()
    {
        StartCoroutine(DestroyBullet());
        
       
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(fireRate + "Fire rate in ")
        rb.velocity = transform.up*bulletSpeed;
       
    }
    IEnumerator DestroyBullet()
    {
        
            
            yield return new WaitForSeconds(2f);
        gameObject.Release();
       


        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
           
            collision.GetComponent<BaseShipEnemy>().LoseHealth(damage);
        }
    }
}
 