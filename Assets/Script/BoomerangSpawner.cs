using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolBox.Pools;

public class BoomerangSpawner : BulletSpawner
{
    public override void ApplyDamage( int index)
    {
        base.ApplyDamage(index);
    }
    public override void TakeTag(GameObject a)
    {
        base.TakeTag(a);
    }
    public override void SpawnBullet()
    {
        Vector2 spawnPosition = new Vector2(transform.position.x - (playerController.spawnBulletNumber - 1) * 0.1f, transform.position.y);
        for (int j = 0; j < playerController.bulletDetail.bulletStats.Count; j++)
        {
            if (playerController.id == playerController.bulletDetail.bulletStats[j].id)
            {
                if (playerController.bulletDetail.bulletStats[j]._bulletType == BulletType.Boomerang)
                {
                    GameObject clonedBullet = playerController.boomerangBullet.Reuse(spawnPosition, this.transform.rotation);
                    spawnPosition.x = spawnPosition.x + 0.2f;
                    playerController.boomerangScript = clonedBullet.GetComponent<Boomerang>();
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
