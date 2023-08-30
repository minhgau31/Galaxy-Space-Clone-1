using System.Collections;
using System.Collections.Generic;
using ToolBox.Pools;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private int damage;
    private int bulletSpeed;
    internal float fireRate;
    public int enemyID;
    public int id;
    Rigidbody2D rb;
    public BulletDetail bulletDetail;
    public GameObject player;
    public PlayerController playerScript;


    // Start is called before the first frame update
    void Awake()
    {
        

        rb = GetComponent<Rigidbody2D>();
        playerScript = player.GetComponent<PlayerController>();

    }
    private void OnEnable()
    {
        StartCoroutine(DestroyBullet());


    }
    public void Init(int id, int speed, float _firerate,int _damage)
    {

        enemyID = id;
        bulletSpeed = speed;
        fireRate = _firerate;
        damage = _damage;

    }

    // Update is called once per frame
    void Update()
    {
     
        rb.velocity = transform.up * bulletSpeed;

    }
    IEnumerator DestroyBullet()
    {


        yield return new WaitForSeconds(2f);
        gameObject.Release();




    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerController>().LoseHealth(damage);
        }
    }
}
