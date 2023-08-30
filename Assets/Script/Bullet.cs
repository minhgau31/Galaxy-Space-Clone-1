using System.Collections;
using System.Collections.Generic;
using ToolBox.Pools;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public int playerID;
    public int bulletSpeed;
    public float fireRate;
    Rigidbody2D rb;
    public BulletDetail bulletDetail;
    public BulletDetail.BulletStats bulletStats;
    public BaseShipEnemy bEnemy;
  

    // Start is called before the first frame update
    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();





    }
    public void Init(int id, int speed, float _firerate,int _damage)
    {
        
        playerID = id;
        bulletSpeed = speed;
        fireRate = _firerate;
        damage = _damage;
     
    }
    private void OnEnable()
    {
       
        StartCoroutine(DestroyBullet());
        
       
    }

    // Update is called once per frame
    public void Update()
    {

        BulletMoving();

    }
    public void BulletMoving()
    {

        rb.velocity = transform.up * bulletSpeed;
        
        
    }
    IEnumerator DestroyBullet()
    {
        
            
            yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
       


        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.gameObject.tag=="bullet")
        {
            if (collision.gameObject.tag == "enemy")
            {

                collision.GetComponent<BaseShipEnemy>().LoseHealth(damage);
            }
        }
        if (this.gameObject.tag == "Enemybullet")
        {
            if (collision.gameObject.tag == "Player")
            {

                collision.GetComponent<PlayerController>().LoseHealth(damage);
            }
        }

    }
       
}
 