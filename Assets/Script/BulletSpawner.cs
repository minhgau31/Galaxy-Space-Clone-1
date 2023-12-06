using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolBox.Pools;

public  class BulletSpawner : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject normalBullet;
    public GameObject boomerangBullet;
    public GameObject boomBullet;
    public Bullet bulletScript;
    public Boom boomScript;
    public Boomerang boomerangScript;

    public void SpawnBullet(BulletType bulletType,int damage,int bulletSpeed,float fireRate,int splashRange,int spawnBulletNumber, Vector2 spawnPosition, Sprite bullet)
    {
        Debug.Log(fireRate + "FIRE RATE CURRENT");
       
            for (int i = 0; i < spawnBulletNumber; i++)
            {
            GameObject clonedBullet = null;
                switch (bulletType)
                {
                    
                    case BulletType.Normal:

                         clonedBullet = normalBullet.Reuse(spawnPosition, this.transform.rotation);
                      spawnPosition.x = spawnPosition.x + 0.2f;
                        bulletScript = clonedBullet.GetComponent<Bullet>();
                        bulletScript.Init(bulletSpeed, fireRate, damage);
                        break;
                    case BulletType.Boomerang:
                    clonedBullet = boomerangBullet.Reuse(spawnPosition, this.transform.rotation);
                        spawnPosition.x = spawnPosition.x + 0.2f;
                        boomerangScript = clonedBullet.GetComponent<Boomerang>();
                        boomerangScript.Init(bulletSpeed, fireRate, damage);
                        break;
                    case BulletType.Boom:
                         clonedBullet = boomBullet.Reuse(spawnPosition, this.transform.rotation);
                        spawnPosition.x = spawnPosition.x + 0.2f;
                        boomScript = clonedBullet.GetComponent<Boom>();
                        boomScript.Init(bulletSpeed, fireRate, damage, splashRange);

                        break;


                }
            clonedBullet.GetComponent<SpriteRenderer>().sprite = bullet;
            clonedBullet.gameObject.tag = "bullet";
            Animator animator = clonedBullet.GetComponent<Animator>();
            animator.SetBool("PurpleBullet", false);
            animator.SetBool("RedBullet", true);
        }
       
    }    
}
