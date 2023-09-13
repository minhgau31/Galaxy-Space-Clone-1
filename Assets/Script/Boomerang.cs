using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Bullet
{
    public Vector2 playerForward;
    public bool go;
    public GameObject player;
   
    // Start is called before the first frame update
    public  void Awake()
    {
        player = GameObject.Find("Player");
    }
    public override void OnEnable()
    {
       
        playerForward = new Vector2(player.transform.position.x, player.transform.position.y + 8);
        StartCoroutine(Boom());
        
       
    }

    // Update is called once per frame
   public override void Update()
    {
        if(go)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerForward, Time.deltaTime * 10);
        }
        if (!go)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * 10);
        }
        if (!go&& Vector2.Distance(player.transform.position,transform.position)<1.5)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator Boom()
    {
        go = true;
        yield return new WaitForSeconds(1);
        go = false;

    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
       
            if (collision.gameObject.tag == "enemy")
            {

            DealDamageToEnemy(collision.gameObject,damage);
                Debug.Log(damage + "Boomerang damage");
            }
        
       

    }
}
