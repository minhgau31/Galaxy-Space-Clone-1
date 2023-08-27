using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolBox.Pools;
using TMPro;
using UnityEngine.UI;
using System;

public class PlayerController : Subject
{
    public int id;
    public int health=100;
    public int maxHealth = 150;
    private float moveSpeed = 8.5f;
    public float fireRate = 0f;
    [SerializeField] private TextMeshPro healthText;

    private Rigidbody2D rb;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bar;
    [SerializeField] private GameObject healthBar;
    private HealthBar healthBarScript;
    public Bullet bulletScript;
    public VariableJoystick joystick;
    private Vector3 move;


    // Start is called before the first frame update
    private void Awake()
    {
      
        GameObject a=Instantiate(healthBar);
        healthBarScript= a.GetComponent<HealthBar>();
        healthBarScript.Init(this.gameObject);
       
        
       
    }
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ShootingBullet());

        
        fireRate = bulletScript.fireRate;






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
        Vector2 Direction=Target-rb.position;
        Direction.Normalize();
        transform.position = Vector3.MoveTowards(transform.position, Target, moveSpeed * Time.deltaTime);
        transform.position = new Vector2(Mathf.Clamp(transform.position.x,-2.8f,2.8f), Mathf.Clamp(transform.position.y, -4.37f, 3.77f));

    }  
    public void LoseHealth(int damage)
    {
        health = health - damage;
        Debug.Log(health);
    }
    private IEnumerator ShootingBullet()
    {
        while (true) 
        {
            bulletScript = bullet.GetComponent<Bullet>();
            bullet.gameObject.Reuse(transform.position, transform.rotation);
        
            yield return new WaitForSeconds(1/bulletScript.bulletDetail.bulletStats[id-1].fireRate);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        NotifyObserver(EventID.OnBulletHit);
        healthBarScript.HealthBarSize(maxHealth, health);
    }

}
