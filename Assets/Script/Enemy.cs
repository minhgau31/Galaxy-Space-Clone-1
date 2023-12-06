using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolBox.Pools;

public class Enemy : BaseShipEnemy
{
    public Rigidbody2D rb;
    private int moveSpeed = 20;
    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public override void  Update()
    {
        Death();
        rb.velocity = -transform.up * moveSpeed;
    }
    public override IEnumerator ShootingBullet()
    {
        yield return new WaitForSeconds(1f);
    }
  }
