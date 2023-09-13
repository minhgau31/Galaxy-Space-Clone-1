using System.Collections;
using System.Collections.Generic;
using ToolBox.Pools;
using UnityEngine;

public class DestroyAfterSecond : MonoBehaviour
{
 
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(Destroy());
        Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, transform.position.z), 5f);
    }

    // Update is called once per frame
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1f);
        gameObject.Release();
    }
  
   
}
