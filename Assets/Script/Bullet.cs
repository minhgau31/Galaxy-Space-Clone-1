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
   
  

    // Start is called before the first frame update
    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();

    }
    public virtual void OnEnable()
    {
        StartCoroutine(DestroyBullet());
    }
   
    public  void Init(int id, int speed, float _firerate,int _damage)
    {
        
        playerID = id;
        bulletSpeed = speed;
        fireRate = _firerate;
        damage = _damage;
     
    }
   

    // Update is called once per frame
    public virtual void Update()
    {
        Debug.Log(playerID + "Player ID");
        Debug.Log(bulletSpeed + "Bullet Speed");
        Debug.Log(fireRate + "Bullet FireRate");
        Debug.Log(damage + "Bullet damage");
        BulletMoving();

    }
    public virtual void BulletMoving()
    {

        rb.velocity = transform.up * bulletSpeed;
        
        
    }
    public void DealDamageToEnemy(GameObject a,int damage)
    {
        a.GetComponent<BaseShipEnemy>().LoseHealth(damage);
    }

    public virtual IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.gameObject.tag=="bullet")
        {
            if (collision.gameObject.tag == "enemy")
            {

                //collision.GetComponent<BaseShipEnemy>().LoseHealth(damage);
                DealDamageToEnemy(collision.gameObject, damage);
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
 