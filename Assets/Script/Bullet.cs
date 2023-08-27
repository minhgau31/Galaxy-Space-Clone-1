using System.Collections;
using System.Collections.Generic;
using ToolBox.Pools;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage=0;
    private int playerID;
    private int bulletSpeed=0;
    internal float fireRate=0;
    Rigidbody2D rb;
    public BulletDetail bulletDetail;
    public BulletDetail.BulletStats bulletStats;
    public BaseShipEnemy bEnemy;
  

    // Start is called before the first frame update
    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();





    }
    public void Init(int id, int speed, float _firerate)
    {
        
        playerID = id;
        bulletSpeed = speed;
        fireRate = _firerate;
      
     
    }
    private void OnEnable()
    {
       
        StartCoroutine(DestroyBullet());
        
       
    }

    // Update is called once per frame
    void Update()
    {

        BulletMoving();

    }
    public void BulletMoving()
    {

        rb.velocity = transform.up * bulletSpeed;
        Debug.Log(bulletSpeed + "Bullet SPEED");
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
 