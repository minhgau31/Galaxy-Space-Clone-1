using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolBox.Pools;
using TMPro;
using UnityEngine.UI;
public class PlayerController : Subject
{
    public int health=150;
    public int maxHealth = 150;
    private float moveSpeed = 8.5f;
    [SerializeField] private TextMeshPro healthText;

    private Rigidbody2D rb;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bar;
    public VariableJoystick joystick;
    private Vector3 move;
    

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ShootingBullet());
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveShip();
        UpdateHealthBarAndText();
    }
    private void UpdateHealthBarAndText()
    {
        healthText.text=health.ToString();
       

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
    private IEnumerator ShootingBullet()
    {
        while (true) 
        {
            yield return new WaitForSeconds(0.5f);            
            bullet.gameObject.Reuse(transform.position, transform.rotation);


        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        health = health - 10;
        NotifyObserver(EventID.OnBulletHit);
        Debug.Log("Collision");
    }
}
