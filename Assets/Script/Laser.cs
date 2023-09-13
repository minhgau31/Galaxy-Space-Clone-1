using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public int damage;
    [SerializeField]private float defDistanceRay = 7;
    public Transform laserFirePoint;
    public LineRenderer m_LineRenderer;
    Transform m_transform;
    public BaseShipEnemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        m_transform = GetComponent<Transform>();
    }
     void Update()
    {
        ShootLaser();
    }
    // Update is called once per frame
    void ShootLaser()
    {
        if(Physics2D.Raycast(m_transform.position,transform.up))
        {
            
            RaycastHit2D _hit = Physics2D.Raycast(laserFirePoint.position, transform.up);
            if (_hit.collider.tag == "enemy")
            {
                enemy = _hit.collider.gameObject.GetComponent<BaseShipEnemy>();
                enemy.LoseHealth(1);
                Debug.Log("hit");
                m_LineRenderer.SetPosition(0, laserFirePoint.position);
                m_LineRenderer.SetPosition(1, _hit.point);
            }
            else
            {
              
            } 
            
          
           

        }
        else 
        {
           m_LineRenderer.SetPosition(0, laserFirePoint.position);
            m_LineRenderer.SetPosition(1, new Vector2(laserFirePoint.transform.position.x,laserFirePoint.transform.position.y+ defDistanceRay));
            Debug.Log(laserFirePoint.transform.up);
           
        }
    }
     void Draw2DRay(Vector2 startPos,Vector2 endPos)
    {
       

    }
}
