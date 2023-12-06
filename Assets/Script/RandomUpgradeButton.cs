using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RandomUpgradeButton : MonoBehaviour
{
    public List<Button> UpgradeButton;
    public List<int> gottedFunction;
    public PlayerController playerController;
    public GameObject Player;
    public GameObject menuUI;
    public MenuUI _menuUI;
    int random;
    // Start is called before the first frame update
    public void Start()
    {
        Player = GameObject.Find("Player");
        playerController = Player.GetComponent<PlayerController>();
        menuUI = GameObject.Find("MENUUI");
        _menuUI = menuUI.GetComponent<MenuUI>();
    }
    public void Init()
    {
        foreach(Button e in UpgradeButton)
        {
            e.onClick.RemoveAllListeners();
            e.gameObject.SetActive(true);
            RandomFunction(e);           
            gottedFunction.Add(random);
            
            
        }
    }
    public void RandomFunction(Button e)
    {
         random = Random.Range(1, 4);
        switch (random)
        {
            case 1:
                e.onClick.AddListener(delegate {LevelDamage(); });
                e.GetComponentInChildren<TMP_Text>().text = "Damage";
                    break;
            case 2:
                e.onClick.AddListener(delegate { FireRateLevel(); });
                e.GetComponentInChildren<TMP_Text>().text = "FireRate";
                break;
            case 3:
                e.onClick.AddListener(delegate { BulletLevel(); });
                e.GetComponentInChildren<TMP_Text>().text = "Bullet";
                break;
            case 4:
                e.onClick.AddListener(delegate { BulletLevel(); });
                e.GetComponentInChildren<TMP_Text>().text = "MaxHealth";
                break;

        }
    }
    public void DisableButton()
    {
        foreach (Button e in UpgradeButton)
        {
            e.gameObject.SetActive(false);


        }
    }    
    #region DamageLevelUP
    public void LevelDamage()
    {
        playerController.damageLevel = playerController._calculator.DamageLevelUp(playerController.damageLevel);
        _menuUI.waveDone.SetActive(true);
        DisableButton();
        Debug.Log("Upgrade Damage");
    }
    #endregion
    #region FireRateLevelUp
    public void FireRateLevel()
    {
        playerController.fireRateLevel = playerController._calculator.FireRateLevelUp(playerController.fireRateLevel);
        _menuUI.waveDone.SetActive(true);
        Debug.Log("Upgrade Fire Rate");
        DisableButton();
    }
    #endregion
    #region BulletLevelUP
    public void BulletLevel()
    {
        playerController.spawnBulletNumber = playerController._calculator.BulletLevelUp(playerController.spawnBulletNumber);
        _menuUI.waveDone.SetActive(true);
        Debug.Log("Upgrade Bullet");
        DisableButton();
    }
    #endregion
    public void IncreaseMaxHealth()
    {
        playerController.maxHealth = playerController._calculator.IncreaseMaxHealth(playerController.maxHealth);
        _menuUI.waveDone.SetActive(true);
        Debug.Log("Upgrade Health");
        DisableButton();
    }
}
