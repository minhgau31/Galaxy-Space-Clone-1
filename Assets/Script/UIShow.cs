using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIShow : MonoBehaviour,IObserver
{
    [SerializeField] Subject Player;
    [SerializeField] Subject MENUUI;
    [SerializeField] Subject GameManager;
    public PlayerController player;
    public GameObject currentHealthBar;
    public float health;
    public GameManager _gameManager;
    public GameObject gameManager;
    public MenuUI menuUI;
    public AudioManager audioSource;

    [SerializeField] private TextMeshPro healthText;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        _gameManager = gameManager.GetComponent<GameManager>();
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
        }
       else if (eventID == EventID.ChangedPoint)
        {
            _gameManager.pointText.text = "Score: "+_gameManager.currentPoint.ToString();
            _gameManager.highestPointText.text = "High Score: "+ _gameManager.highestScore[_gameManager._levelCreate.currentLevel - 1].ToString();
        }
        else if (eventID==EventID.OpenStartUI)
        {
            menuUI.levelSelection.SetActive(true);
            //_levelCreate.StartCoroutine(_levelCreate.StartLevel());
            menuUI.startMenu.SetActive(false);
            //OnGamePause();
        }
        else if (eventID == EventID.OpenPauseUI)
        {
            menuUI.pauseMenu.SetActive(true);
            
            menuUI.OnGamePause();
        }
        else if (eventID == EventID.OpenDeathUI)
        {
            menuUI.DEATHMENU.SetActive(true);
            menuUI.OnGamePause(); 
        }

    }
    private void OnEnable()
    {
        Player.AddObserver(this);
        MENUUI.AddObserver(this);
        GameManager.AddObserver(this);
    }
    private void OnDisable()
    {
        Player.RemoveObserver(this);
        MENUUI.RemoveObserver(this);
        GameManager.RemoveObserver(this);
    }
}
