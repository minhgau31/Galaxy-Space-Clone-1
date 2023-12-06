using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolBox.Pools;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using System;

public class PlayerController : Subject
{
    public int spawnBulletNumber;
    public int damageLevel;
    public int fireRateLevel;
    public int teamID = 1;
    public int id;
    [NonSerialized]public int chanceCriticalDamage;
    [NonSerialized] public float multiplyCriticalDamage;
    public int health = 100;
    public int maxHealth = 150;
    public int damageBoosted;
    public int bulletSpeedBoosted;
    public int splashRangeBoosted;
    public float fireRateBoosted;
    private float moveSpeed = 8.5f;
    

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
    public ConfigReader cfgReader;
    
    public BulletSpawner bulletSpawner;
    public GameObject MENUUI;
    public Vector2 spawnPosition;
    public Sprite bulletSprite;
    MenuUI menuUI;
   

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
        MENUUI = GameObject.Find("MENUUI");
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ShootingBullet());
        menuUI = MENUUI.GetComponent<MenuUI>();
    }
    // Update is called once per frame
    void Update()
    {
        Death();
        MoveShip();
        Debug.Log(fireRateBoosted+"FIRE RATE BOOSTED");
     
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
   
    #region LoseHealth
    public void LoseHealth(int damage)
    {
        
        health = health - damage;
       
    }
    #endregion
    #region TakeTag
    public void SpawnBullet()
    {
        

    }

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




    //            break;
    //        }
    //    }
    //}
    #endregion
    #region ShootingBullet Ienumerator
    public void Death()
    {
        if (health <= 0)
        {          
            menuUI.playerDeath();         
        }
    }
    private IEnumerator ShootingBullet()
    {

        while (true)
        {
            spawnPosition = new Vector2(transform.position.x - (spawnBulletNumber - 1) * 0.1f, transform.position.y);
            //Spawnbullet number phai -1 vì đó là số khoảng trống giữa các bullet nếu có 2 bullet thì khoảng trống sẽ là 1 còn 3 bullet thì có 2 khoảng trống
           BulletDetail.BulletStats _bulletStats =cfgReader.GetBulletStats(id, teamID);
            bulletSpawner.SpawnBullet(_bulletStats._bulletType, _bulletStats.bulletDamage+ damageLevel, _bulletStats.bulletSpeed, _bulletStats.fireRate+ fireRateLevel+fireRateBoosted, _bulletStats.splashRange, spawnBulletNumber, spawnPosition,bulletSprite);        
            
            yield return new WaitForSeconds(1 / (_bulletStats.fireRate+fireRateBoosted+fireRateLevel));
        }

    }
    #endregion

    public void OnTriggerEnter2D(Collider2D collision)
    {
        NotifyObserver(EventID.OnBulletHit);
        healthBarScript.HealthBarSize(maxHealth, health);
    }

}
