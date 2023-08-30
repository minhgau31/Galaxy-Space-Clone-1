using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolBox.Pools;
using TMPro;
using UnityEngine.UI;
using System;

public class PlayerController : Subject
{
    private int spawnBulletNumber;
    private int damageLevel;
    private int fireRateLevel;
    public int teamID = 1;
    public int id;
    public int health = 100;
    public int maxHealth = 150;
    public int damage;
    public int bulletSpeed;
    private float moveSpeed = 8.5f;
    public float fireRate;
    [SerializeField] private TextMeshPro healthText;

    private Rigidbody2D rb;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bar;
    [SerializeField] private GameObject healthBar;
    private HealthBar healthBarScript;
    public Bullet bulletScript;
    public VariableJoystick joystick;
    private Vector3 move;
    public BulletDetail bulletDetail;
    public BulletDetail.BulletStats bulletStats;

    // Start is called before the first frame update
    private void Awake()
    {

        GameObject a = Instantiate(healthBar);
        healthBarScript = a.GetComponent<HealthBar>();
        healthBarScript.Init(this.gameObject);



    }
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ShootingBullet());







    }

    // Update is called once per frame
    void Update()
    {

        MoveShip();



    }

    private void MoveShip()
    {
        Quaternion target1 = Quaternion.Euler(0, 0, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target1, 100 * Time.deltaTime);

        //if (Input.touchCount > 0)
        //{

        //    Touch touch = Input.GetTouch(0);
        //    Vector3 touch_Pos = Camera.main.ScreenToWorldPoint(touch.position);
        //    touch_Pos.z = 0;
        //    touch_Pos.y = touch_Pos.y + 1;
        //    
        //    if(transform.position.x<touch_Pos.x)
        //    {
        //        Quaternion target = Quaternion.Euler(0, -15, 0);
        //        transform.rotation = Quaternion.Slerp(transform.rotation, target, 100 * Time.deltaTime);
        //    }
        //    if(transform.position.x>touch_Pos.x)
        //    {
        //        Quaternion target = Quaternion.Euler(0, 15, 0);
        //        transform.rotation = Quaternion.Slerp(transform.rotation, target, 100 * Time.deltaTime);
        //    }



        //}
        move.x = joystick.Horizontal;
        move.y = joystick.Vertical;
        Vector2 Target = new Vector2(rb.position.x + move.x, rb.position.y + move.y);
        Vector2 Direction = Target - rb.position;
        Direction.Normalize();
        transform.position = Vector3.MoveTowards(transform.position, Target, moveSpeed * Time.deltaTime);
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -2.8f, 2.8f), Mathf.Clamp(transform.position.y, -4.37f, 3.77f));

    }
    public void LevelDamage()
    {
        damageLevel++;
        Debug.Log(damage + "Player Damage");
        Debug.Log(damageLevel + "damageLevel");
    }
    public void FireRateLevel()
    {
        fireRateLevel++;
        Debug.Log(fireRate + "Player FireRate");
        Debug.Log(fireRateLevel + "Level Fire Rate");
    }
    public void BulletLevel()
    {
        spawnBulletNumber++;
    }
    public void LoseHealth(int damage)
    {
        
        health = health - damage;
        Debug.Log(health);
    } 
    public void TakeTag(GameObject a)
    {
        for (int i = 0; i < bulletDetail.bulletStats.Count; i++)
        {
            if (teamID == 1)
            {
                a.tag = "bullet";
                a.GetComponent<SpriteRenderer>().color = Color.green;
            }
            if (id == bulletDetail.bulletStats[i].id)
            {

                damage = bulletDetail.bulletStats[i].bulletDamage;
                bulletSpeed = bulletDetail.bulletStats[i].bulletSpeed;
                fireRate = bulletDetail.bulletStats[i].fireRate;
           
                bulletScript.Init(id, bulletSpeed, fireRate+fireRateLevel, damage+damageLevel);
                break;
            }
        }
    }
    private IEnumerator ShootingBullet()
    {

        while (true)
        {
            
            //GameObject clonedBullet = bullet.Reuse(transform.position, this.transform.rotation);
            //bulletScript = clonedBullet.GetComponent<Bullet>();
            //for (int i = 0; i < bulletDetail.bulletStats.Count; i++)
            //{
               //if(teamID==1)
               //{
               //     clonedBullet.tag = "bullet";
               //    clonedBullet.GetComponent<SpriteRenderer>().color=Color.green;
               //}
            //if (id == bulletDetail.bulletStats[i].id)
            // {

            //    damage = bulletDetail.bulletStats[i].bulletDamage;
            //    bulletSpeed = bulletDetail.bulletStats[i].bulletSpeed;
            //     fireRate = bulletDetail.bulletStats[i].fireRate;
            //    Debug.Log(damage + "Player Damage");
            //     Debug.Log(fireRate + "Player Firerate");
            //    bulletScript.Init(id, bulletSpeed, fireRate,damage);
            //    break;
            //}
            //Spawnbullet number phai -1 vì đó là số khoảng trống giữa các bullet nếu có 2 bullet thì khoảng trống sẽ là 1 còn 3 bullet thì có 2 khoảng trống
            Vector2 spawnPosition = new Vector2(transform.position.x -(spawnBulletNumber -1)* 0.1f, transform.position.y);
            for (int i = 0; i < spawnBulletNumber; i++)
            {

                
                
                
                    GameObject clonedBullet = bullet.Reuse(spawnPosition, this.transform.rotation);                   
                    spawnPosition.x = spawnPosition.x + 0.2f;                    
                    bulletScript = clonedBullet.GetComponent<Bullet>();
                        TakeTag(clonedBullet);

               

               


                
            }

            yield return new WaitForSeconds(1 / fireRate);
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        NotifyObserver(EventID.OnBulletHit);
        healthBarScript.HealthBarSize(maxHealth, health);
    }

}
