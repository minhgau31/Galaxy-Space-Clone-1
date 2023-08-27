using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIShow : MonoBehaviour,IObserver
{
    [SerializeField] Subject Player;
     public PlayerController player;
    public GameObject currentHealthBar;
    public float health;
    [SerializeField] private TextMeshPro healthText;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnNotify(EventID eventID)
    {
        if(eventID==EventID.OnBulletHit)
        {
            healthText.text = player.health.ToString();
            Debug.Log(player.health);
        //    currentHealthBarSize =(float)player.health / (float)player.maxHealth;
        //    healthBar.healthBar.transform.localScale = new Vector3(currentHealthBarSize, 0.1075f,1);
        //    Debug.Log(currentHealthBarSize);
        //    Debug.Log(player.health+"Health");
        //    Debug.Log(player.maxHealth+"Max Health");
        //    Debug.Log("Notify UISHOW");
        }
       
    }
    private void OnEnable()
    {
        Player.AddObserver(this);
    }
    private void OnDisable()
    {
        Player.RemoveObserver(this);
    }
}
