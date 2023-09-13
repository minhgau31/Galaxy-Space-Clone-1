using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolBox.Pools;

public  class BulletSpawner : MonoBehaviour
{
    public PlayerController playerController;
   public virtual void ApplyDamage(int index)
    {
        playerController.damage = playerController.bulletDetail.bulletStats[index].bulletDamage;
        playerController.bulletSpeed = playerController.bulletDetail.bulletStats[index].bulletSpeed;
        playerController.fireRate = playerController.bulletDetail.bulletStats[index].fireRate;
      

    }
    public virtual void TakeTag(GameObject a)
    {
        for (int i = 0; i < playerController. bulletDetail.bulletStats.Count; i++)
        {
            Debug.Log(playerController.damage + "Player DAMAGE");
            Debug.Log(playerController.bulletSpeed + "Player BULLETSPEED");
            Debug.Log(playerController.fireRate + "Player FIRERATE");
            if (playerController. teamID == 1)
            {
                a.tag = "bullet";
                a.GetComponent<SpriteRenderer>().color = Color.green;
            }
            if (playerController.id == playerController.bulletDetail.bulletStats[i].id)
            {
               
                ApplyDamage(i);
                playerController. damage = playerController._calculator.CalculateCriticalDamage(playerController.damage, playerController.multiplyCriticalDamage, playerController.chanceCriticalDamage);
            
                playerController.bulletScript.Init(playerController.id, playerController.bulletSpeed, playerController.fireRate + playerController.fireRateLevel, playerController.damage + playerController.damageLevel);

            }
            else
            {
                Debug.Log("Not Found");
            }
        }
    }
    public virtual void SpawnBullet()
    {
        Vector2 spawnPosition = new Vector2(playerController.transform.position.x - (playerController.spawnBulletNumber - 1) * 0.1f, playerController.transform.position.y);
        for (int j = 0; j < playerController.bulletDetail.bulletStats.Count; j++)
        {
            if (playerController.id == playerController.bulletDetail.bulletStats[j].id)
            {
                if (playerController.bulletDetail.bulletStats[j]._bulletType == BulletType.Normal)
                {
                    GameObject clonedBullet = playerController.bullet.Reuse(spawnPosition, this.transform.rotation);
                spawnPosition.x = spawnPosition.x + 0.2f;
                playerController. bulletScript = clonedBullet.GetComponent<Bullet>();
                TakeTag(clonedBullet);
                    }
            }
            else
            {
                Debug.Log("Not Found");
            }    
        }
    }    
}
