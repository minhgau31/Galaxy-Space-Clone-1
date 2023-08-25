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
    }

    // Update is called once per frame
   IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1f);
        gameObject.Release();
    }    
}
