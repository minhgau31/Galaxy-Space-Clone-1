using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using ToolBox.Pools;
using UnityEngine;
using UnityEngine.Animations;

public class BaseShipEnemy : MonoBehaviour
{
    private DOTweenPath path;
    public EnemyDetail enemyDetail;
    public int teamID;
    public int id;
    public int Health;
    public int MaxHealth;
    public float rotationSpeed;
    public bool lookAtPlayer;
    public GameObject player;
    public PlayerController playerScript;
    public GameObject bullet;
    public Bullet enemyBullet;
    public GameObject firingPoint;
    public GameObject floatingHealth;
    public GameObject gFloatingHealth;
    public BulletDetail bulletDetail;
    public BulletDetail.BulletStats bulletStats;
    public float fireRate;
    public int damage;
    public int bulletSpeed;

    public void Start()
    {
        for (int i = 0; i < enemyDetail.enemyStats.Count; i++)
        {
            if (id == enemyDetail.enemyStats[i].id)
            {

                Health = enemyDetail.enemyStats[i].Health;
                rotationSpeed = enemyDetail.enemyStats[i].rotationSpeed;
                lookAtPlayer = enemyDetail.enemyStats[i].LookAtPlayer;
                MaxHealth = enemyDetail.enemyStats[i].Health;
                break;
            }
        }
        Debug.Log(MaxHealth + "Max Health");

        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerController>();
        StartCoroutine(ShootingBullet());
    }
    public void Init(DOTweenPath mainPath)
    {
        enemyBullet = bullet.GetComponent<Bullet>();
        
        path = mainPath;   
        Move();
       
    }
    public void OnEnable()
    {

    }
    public void Update()
    {
        Death();
        LookAtPlayer();
    }
    public void Move()
    {
        transform.position = path.wps[0];
        transform.DOPath(path.wps.ToArray(), path.duration, path.pathType, PathMode.TopDown2D, path.pathResolution)
                   .SetLoops(path.loops, path.loopType);
        
    }
    public void LookAtPlayer()
    {
        if (lookAtPlayer == true)
        {
            Vector3 direction = player.transform.position - this.transform.position;
            direction.Normalize();
            float angle = MathF.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, rotationSpeed * Time.deltaTime);
            

        }

    }
    public void LoseHealth(int damage)
    {
        Health = Health - damage;
        gFloatingHealth=floatingHealth.gameObject.Reuse(transform.position, new Quaternion(0,0,0,0));
       
        gFloatingHealth.GetComponentInChildren<TextMesh>().text = damage.ToString();
        if(playerScript.isCriticalDamage==true)
        {
            gFloatingHealth.GetComponentInChildren<TextMesh>().color = Color.yellow;
        }
      
        Debug.Log(damage+" damage gay ra");
        //StartCoroutine(DestroyText());


    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            
        }
    }
    public void Death()
    {
        if (Health <= 0) 
        {
            Debug.Log("Death");
            this.gameObject.Release();
        }
    }
    private IEnumerator ShootingBullet()
    {             
        while (true)
        {

            GameObject clonedBullet = bullet.Reuse(transform.position, this.transform.rotation);
            enemyBullet = clonedBullet.GetComponent<Bullet>();
            for (int i = 0; i < bulletDetail.bulletStats.Count; i++)
            {
                if(teamID==2)
                {
                    clonedBullet.tag = "Enemybullet";
                    clonedBullet.GetComponent<SpriteRenderer>().color = Color.red;
                }
                if (id == bulletDetail.bulletStats[i].id)
                {

                    damage = bulletDetail.bulletStats[i].bulletDamage;
                    bulletSpeed = bulletDetail.bulletStats[i].bulletSpeed;
                    fireRate = bulletDetail.bulletStats[i].fireRate;
                    
                    enemyBullet.Init(id, bulletSpeed, fireRate,damage);
                    break;
                }
            }
           
            
            yield return new WaitForSeconds(1 /fireRate);
        }
    }
    private IEnumerator DestroyText()
    {
        yield return new WaitForSeconds(1f);
        gFloatingHealth.gameObject.Release();
        
    }
}
   
