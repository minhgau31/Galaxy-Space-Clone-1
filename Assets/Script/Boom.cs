using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolBox.Pools;

public class Boom : Bullet
{
    [SerializeField] private GameObject particleEffect;
    public int SplashRange;

    public void Start()
    {
       
    }
    public void Init(int id, int speed, float _firerate, int _damage,int _splashRange)
    {
        playerID = id;
        bulletSpeed = speed;
        fireRate = _firerate;
        damage = _damage;
        SplashRange = _splashRange;
    }
  
    public override void Update()
    {

        base.Update();

    }
    public override void BulletMoving()
    {

        base.BulletMoving();


    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, SplashRange);
            foreach (var hitCollider in hitColliders)
            {
                BaseShipEnemy enemy = hitCollider.GetComponent<BaseShipEnemy>();
                if (enemy)
                {
                    var closetPoint = hitCollider.ClosestPoint(transform.position);
                    var distance = Vector3.Distance(closetPoint, transform.position);
                    var damagePercent = Mathf.InverseLerp(SplashRange, 0, distance);
                    int _damage = Mathf.FloorToInt(damagePercent * damage);
                    enemy.LoseHealth(_damage);
                   
                    Debug.Log(damagePercent * damage + "Damage Deal");
                }
            }
            Debug.Log("alo");
            GameObject _particleEffect = particleEffect.Reuse(this.transform.position, this.transform.rotation);
            Destroy(gameObject);
        }
    }
}
