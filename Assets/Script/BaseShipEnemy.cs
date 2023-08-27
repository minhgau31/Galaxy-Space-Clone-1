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
    public EnemyStats enemyStats;
    public int Health;
    public int MaxHealth;
    public GameObject player;
    public GameObject bullet;
    public EnemyBullet enemyBullet;
    public GameObject firingPoint;
    public GameObject floatingHealth;
    public GameObject gFloatingHealth;
    
    public void Start()
    {
        Health = enemyStats.Health;
        player = GameObject.Find("Player");
        StartCoroutine(ShootingBullet());
    }
    public void Init(DOTweenPath mainPath)
    {
        enemyBullet = bullet.GetComponent<EnemyBullet>();
        MaxHealth =enemyStats.Health;
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
        if (enemyStats.LookAtPlayer == true)
        {
            Vector3 direction = player.transform.position - this.transform.position;
            direction.Normalize();
            float angle = MathF.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, enemyStats.rotationSpeed * Time.deltaTime);
            

        }

    }
    public void LoseHealth(int damage)
    {
        Health = Health - damage;
        gFloatingHealth=floatingHealth.gameObject.Reuse(this.transform.position, new Quaternion(0,0,0,0));
        floatingHealth.GetComponentInChildren<TextMesh>().text = damage.ToString();
        Debug.Log(Health);
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
            bullet.gameObject.Reuse(firingPoint.transform.position, transform.rotation);
            
            yield return new WaitForSeconds(1 /2);
        }
    }
    private IEnumerator DestroyText()
    {
        yield return new WaitForSeconds(1f);
        gFloatingHealth.gameObject.Release();
        
    }
}
   
