using System.Collections;
using System.Collections.Generic;
using ToolBox.Pools;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    private int bulletSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        
    }
    private void OnEnable()
    {
        StartCoroutine(DestroyBullet());
       
    }

    // Update is called once per frame
    void Update()
    {
        
        rb.velocity = transform.up* bulletSpeed;
    }
    IEnumerator DestroyBullet()
    {
        
            
            yield return new WaitForSeconds(2f);
        gameObject.Release();
       


        
    }

}
 