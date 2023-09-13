using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolBox.Pools;
using TMPro;
using UnityEngine.UI;
using System;

public class PlayerController : Subject
{
    public int spawnBulletNumber;
    internal int damageLevel;
    internal int fireRateLevel;
    public int teamID = 1;
    public int id;
    [NonSerialized]public int chanceCriticalDamage;
    [NonSerialized] public float multiplyCriticalDamage;
    public int health = 100;
    public int maxHealth = 150;
    public int damage;
    public int bulletSpeed;
    public int splashRange;
    private float moveSpeed = 8.5f;
    public float fireRate;
    public bool isCriticalDamage = false;
    [SerializeField] private TextMeshPro healthText;

    private Rigidbody2D rb;
    [SerializeField] internal GameObject bullet;
    [SerializeField] internal GameObject boomBullet;
    [SerializeField] internal GameObject boomerangBullet;
    [SerializeField] private GameObject bar;
    [SerializeField] private GameObject healthBar;
    private HealthBar healthBarScript;
    public Bullet bulletScript;
    public Boom boomScript;
    public Boomerang boomerangScript;
    public VariableJoystick joystick;
    private Vector3 move;
    public BulletDetail bulletDetail;
    public BulletDetail.BulletStats bulletStats;
    public Calculator _calculator;
    public BulletType _bulletType;
    public BulletSpawner bulletSpawner;
    public BoomSpawner boomSpawner;
    public BoomerangSpawner boomerangSpawner;

    // Start is called before the first frame update
    private void Awake()
    {
        spawnBulletNumber = 1;
        damageLevel = 0;
        fireRateLevel = 0;
        chanceCriticalDamage = 5;
        multiplyCriticalDamage = 1.2f;
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
        Debug.Log(damage);
    }
    #region moveShip
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
    #endregion 
    #region DamageLevelUP
    public void LevelDamage()
    {
        damageLevel=_calculator.DamageLevelUp(damageLevel);
    }
    #endregion
    #region FireRateLevelUp
    public void FireRateLevel()
    {
        fireRateLevel = _calculator.FireRateLevelUp(fireRateLevel);
    }
    #endregion
    #region BulletLevelUP
    public void BulletLevel()
    {
        spawnBulletNumber = _calculator.BulletLevelUp(spawnBulletNumber);
    }
    #endregion
    #region LoseHealth
    public void LoseHealth(int damage)
    {
        
        health = health - damage;
       
    }
    #endregion
    #region TakeTag
    //public void TakeTag(GameObject a)
    //{
    //    for (int i = 0; i < bulletDetail.bulletStats.Count; i++)
    //    {
            
    //        if (teamID == 1)
    //        {
    //            a.tag = "bullet";
    //            a.GetComponent<SpriteRenderer>().color = Color.green;
    //        }
    //        if (id == bulletDetail.bulletStats[i].id)
    //        {
    //            switch(bulletDetail.bulletStats[i]._bulletType)
    //            {
                   
    //                case BulletType.Normal:
    //                 damage = bulletDetail.bulletStats[i].bulletDamage;
    //                 bulletSpeed = bulletDetail.bulletStats[i].bulletSpeed;
    //                 fireRate = bulletDetail.bulletStats[i].fireRate;
    //                 damage = _calculator.CalculateCriticalDamage(damage, multiplyCriticalDamage, chanceCriticalDamage);
    //                 Debug.Log(damage);
    //                 bulletScript.Init(id, bulletSpeed, fireRate + fireRateLevel, damage+damageLevel);                       
    //                   break;
    //                case BulletType.Boom:
    //                    damage = bulletDetail.bulletStats[i].bulletDamage;
    //                    bulletSpeed = bulletDetail.bulletStats[i].bulletSpeed;
    //                    fireRate = bulletDetail.bulletStats[i].fireRate;
    //                    splashRange = bulletDetail.bulletStats[i].splashRange;
    //                    boomScript.Init(id, bulletSpeed, fireRate+fireRate, damage+damageLevel, splashRange);
    //                    break;
    //                case BulletType.Boomerang:
    //                    damage = bulletDetail.bulletStats[i].bulletDamage;
    //                    bulletSpeed = bulletDetail.bulletStats[i].bulletSpeed;
    //                    fireRate = bulletDetail.bulletStats[i].fireRate;
    //                    boomerangScript.Init(id, bulletSpeed, fireRate, damage);
    //                    break;
    //            }
               
               

    //            break;
    //        }
    //    }
    //}
    #endregion
    #region ShootingBullet Ienumerator
    private IEnumerator ShootingBullet()
    {

        while (true)
        {
           
            //Spawnbullet number phai -1 vì đó là số khoảng trống giữa các bullet nếu có 2 bullet thì khoảng trống sẽ là 1 còn 3 bullet thì có 2 khoảng trống
            Vector2 spawnPosition = new Vector2(transform.position.x -(spawnBulletNumber -1)* 0.1f, transform.position.y);
            for (int i = 0; i < spawnBulletNumber; i++)
            {

                bulletSpawner.SpawnBullet();
                boomSpawner.SpawnBullet();
                boomerangSpawner.SpawnBullet();


                //for (int j = 0; j < bulletDetail.bulletStats.Count; j++)
                //{
                //    if (id == bulletDetail.bulletStats[j].id)
                //    {
                //        switch (bulletDetail.bulletStats[j]._bulletType)
                //        {
                //            case BulletType.Normal:
                //                GameObject clonedBullet = bullet.Reuse(spawnPosition, this.transform.rotation);
                //                spawnPosition.x = spawnPosition.x + 0.2f;
                //                bulletScript = clonedBullet.GetComponent<Bullet>();
                //               // TakeTag(clonedBullet);
                //                break;
                //            case BulletType.Boom:
                //                GameObject clonedBullet1 = boomBullet.Reuse(spawnPosition, this.transform.rotation);
                //                spawnPosition.x = spawnPosition.x + 0.2f;
                //                boomScript = clonedBullet1.GetComponent<Boom>();
                //               // TakeTag(clonedBullet1);
                //                break;
                //            case BulletType.Boomerang:
                //                GameObject clonedBullet2 = boomerangBullet.Reuse(spawnPosition, this.transform.rotation);
                //                spawnPosition.x = spawnPosition.x + 0.2f;
                //                boomerangScript = clonedBullet2.GetComponent<Boomerang>();
                //               // TakeTag(clonedBullet2);
                //                break;
                //        }
                //    }
                //}


            }   
               

            yield return new WaitForSeconds(1 / fireRate);
        }

    }
    #endregion

    public void OnTriggerEnter2D(Collider2D collision)
    {
        NotifyObserver(EventID.OnBulletHit);
        healthBarScript.HealthBarSize(maxHealth, health);
    }

}
