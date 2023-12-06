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
    public GameObject gameManager;
    private GameManager _gameManager;


    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        _gameManager = gameManager.GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();

    }
    public virtual void OnEnable()
    {
        StartCoroutine(DestroyBullet());
    }
   
    public  void Init( int speed, float _firerate,int _damage)
    {

        _gameManager.bullet.Add(this.gameObject);
        bulletSpeed = speed;
        fireRate = _firerate;
        damage = _damage;
     
    }
   

    // Update is called once per frame
    public virtual void Update()
    {
       
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
    public void DealDamageToPlayer(GameObject a, int damage)
    {
        a.GetComponent<PlayerController>().LoseHealth(damage);
    }


    public virtual IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(2f);
        BulletDestroy();


    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.gameObject.tag=="bullet")
        {
            if (collision.gameObject.tag == "enemy")
            {
                BulletDestroy();
                //collision.GetComponent<BaseShipEnemy>().LoseHealth(damage);
                DealDamageToEnemy(collision.gameObject, damage);
            }
        }
        if (this.gameObject.tag == "Enemybullet")
        {
            if (collision.gameObject.tag == "Player")
            {
                BulletDestroy();
                DealDamageToPlayer(collision.gameObject, damage);
            }
        }

    }
    public void BulletDestroy()
    {
        this.gameObject.Release();
        _gameManager.bullet.Remove(this.gameObject);
        this.gameObject.tag = "Untagged";
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
       
}
 