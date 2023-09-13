using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolBox.Pools;

public class BoomSpawner : BulletSpawner
{
    // Start is called before the first frame update
    public void ApplyDamage( int index)
    {
        playerController.damage = playerController.bulletDetail.bulletStats[index].bulletDamage;
        playerController.bulletSpeed = playerController.bulletDetail.bulletStats[index].bulletSpeed;
        playerController.fireRate = playerController.bulletDetail.bulletStats[index].fireRate;
        playerController.splashRange = playerController.bulletDetail.bulletStats[index].splashRange;

    }
    public override void TakeTag(GameObject a)
    {
        for (int i = 0; i < playerController.bulletDetail.bulletStats.Count; i++)
        {

            if (playerController.teamID == 1)
            {
                a.tag = "bullet";
                a.GetComponent<SpriteRenderer>().color = Color.green;
            }
            if (playerController.id == playerController.bulletDetail.bulletStats[i].id)
            {
                ApplyDamage(i);
                playerController.damage = playerController._calculator.CalculateCriticalDamage(playerController.damage, playerController.multiplyCriticalDamage, playerController.chanceCriticalDamage);
                
                playerController.boomScript.Init(playerController.id, playerController.bulletSpeed, playerController.fireRate + playerController.fireRateLevel, playerController.damage + playerController. damageLevel, playerController.splashRange);

            }
            else
            {
                Debug.Log("Not Found");
            }
        }
    }
    public override void SpawnBullet()
    {
        Vector2 spawnPosition = new Vector2(transform.position.x - (playerController.spawnBulletNumber - 1) * 0.1f, transform.position.y);
        for (int j = 0; j < playerController.bulletDetail.bulletStats.Count; j++)
        {
            if (playerController.id == playerController.bulletDetail.bulletStats[j].id)
            {
                if (playerController.bulletDetail.bulletStats[j]._bulletType == BulletType.Boom)
                {
                    GameObject clonedBullet = playerController.boomBullet.Reuse(spawnPosition, this.transform.rotation);
                    spawnPosition.x = spawnPosition.x + 0.2f;
                    playerController.boomScript = clonedBullet.GetComponent<Boom>();
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
