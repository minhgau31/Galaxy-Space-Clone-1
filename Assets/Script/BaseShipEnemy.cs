using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using ToolBox.Pools;
using UnityEngine;
using UnityEngine.Animations;

public class BaseShipEnemy : MonoBehaviour
{
    public DOTweenPath path;
    public DOTweenPath additionPath;
    public EnemyDetail enemyDetail;
    public int teamID;
    public int id;
    public int bulletID;
    public int Health;
    public int MaxHealth;
    public float rotationSpeed;
    public int enemyPoint;
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
    public event Action OnEnemyDestroy;
    public bool isMove=false;
    public bool isInit = false;
    public GameObject gameManager;
    public GameManager _gameManager;
    public Sprite bulletSprite;

    public virtual void Start()

    {
        gameManager = GameObject.Find("GameManager");
        _gameManager = gameManager.GetComponent<GameManager>();
        Debug.Log(MaxHealth + "Max Health");
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerController>();
        
    }
    public void Init(DOTweenPath mainPath, DOTweenPath _additionpath)
    {
        
        isInit = true;
        isMove = true;
        path = mainPath;
        additionPath = _additionpath;
        transform.position = path.wps[0];
        for (int i = 0; i < enemyDetail.enemyStats.Count; i++)
        {
            if (id == enemyDetail.enemyStats[i].id)
            {
                if (id == enemyDetail.enemyStats[i].id)
                {
                    Health = enemyDetail.enemyStats[i].Health;
                    rotationSpeed = enemyDetail.enemyStats[i].rotationSpeed;
                    lookAtPlayer = enemyDetail.enemyStats[i].LookAtPlayer;
                    MaxHealth = enemyDetail.enemyStats[i].Health;
                    enemyPoint = enemyDetail.enemyStats[i].point;
                    break;
                }


                break;
            }
        }
       
        Health = MaxHealth;
        enemyBullet = bullet.GetComponent<Bullet>();                        
        Move();
        StartCoroutine(ShootingBullet());

    }
    //public  void OnEnable()
    //{
    //    for (int i = 0; i < enemyDetail.enemyStats.Count; i++)
    //    {
    //        if (id == enemyDetail.enemyStats[i].id)
    //        {
    //            Health = enemyDetail.enemyStats[i].Health;
    //            rotationSpeed = enemyDetail.enemyStats[i].rotationSpeed;
    //            lookAtPlayer = enemyDetail.enemyStats[i].LookAtPlayer;
    //            MaxHealth = enemyDetail.enemyStats[i].Health;
    //            break;
    //        }
    //    }
       
    //}
    
    private void EnemyDetail(int index)
    {
       
    }
    public virtual void Update()
    {
       
            Debug.Log("aloo");
            Death();
            LookAtPlayer();

        
    }
    public void Move()
    {

        transform.position = path.wps[0];

        transform.DOPath(path.wps.ToArray(), path.duration, path.pathType, PathMode.TopDown2D, path.pathResolution)
            .SetOptions(path.isClosedPath)
            .SetDelay(path.delay)
            .SetLoops(path.loops, path.loopType)
            .SetSpeedBased(path.isSpeedBased)
            .SetEase(path.easeCurve)
            .onComplete += delegate
            {
                if (!additionPath)
                {
                    DeathByEndPath();
                    Debug.Log("END OF ROAD");
                }
                else
                {
                    ContinueAdditionPath();
                }
            };
    }
        
    public void ContinueAdditionPath()
    {
        transform.DOPath(additionPath.wps.ToArray(), additionPath.duration, additionPath.pathType, PathMode.TopDown2D, additionPath.pathResolution)
     .SetOptions(additionPath.isClosedPath)
     .SetDelay(additionPath.delay)
     .SetLoops(additionPath.loops, additionPath.loopType)
     .SetSpeedBased(additionPath.isSpeedBased)
     .SetEase(additionPath.easeCurve);
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
        AudioManager.Instance.PlaySFX("GetHit");
        Debug.Log(damage+" damage gay ra");
        //StartCoroutine(DestroyText());


    }
    public void Reset()
    {
       
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            
        }
    }
   
    public void DeathByEndPath()
    {
        transform.DOKill();
        Debug.Log("Deathhhhhhh");
     
        if (OnEnemyDestroy != null)
        {

            OnEnemyDestroy();
            OnEnemyDestroy = null;
        }

        gameObject.Release();
    }
    public void DestroySelf()
    {
        gameObject.Release();
        transform.DOKill();
        OnEnemyDestroy = null;
    }    
    public void Death()
    {
       
        if (Health <= 0) 
        {
            AudioManager.Instance.PlaySFX("Explosion");
            transform.DOKill();
            Debug.Log("Deathhhhhhh");
            _gameManager.currentPoint += enemyPoint;
            if (OnEnemyDestroy!=null)
            {
                
                OnEnemyDestroy();
                OnEnemyDestroy = null;
            }
            
            gameObject.Release();
                            
            


        }
    }
    public virtual IEnumerator ShootingBullet()
    {             
        while (true)
        {

            GameObject clonedBullet = bullet.Reuse(firingPoint.transform.position, this.transform.rotation);
            enemyBullet = clonedBullet.GetComponent<Bullet>();
            for (int i = 0; i < bulletDetail.bulletStats.Count; i++)
            {
                if(teamID==2)
                {
                    clonedBullet.tag = "Enemybullet";
                    clonedBullet.GetComponent<SpriteRenderer>().sprite = bulletSprite;
                    Animator animator = clonedBullet.GetComponent<Animator>();
                    animator.SetBool("PurpleBullet",true);
                    animator.SetBool("RedBullet", false);
                }
                if (bulletID == bulletDetail.bulletStats[i].id)
                {

                    GetBulletStat(i);
                    break;
                }
            }
           
            
            yield return new WaitForSeconds(1 /fireRate);
        }
    }
    public void GetBulletStat(int index)
    {
        damage = bulletDetail.bulletStats[index].bulletDamage;
        bulletSpeed = bulletDetail.bulletStats[index].bulletSpeed;
        fireRate = bulletDetail.bulletStats[index].fireRate;

        enemyBullet.Init(bulletSpeed, fireRate, damage);
    }    
    private IEnumerator DestroyText()
    {
        yield return new WaitForSeconds(1f);
        gFloatingHealth.gameObject.Release();
        
    }
}
   
