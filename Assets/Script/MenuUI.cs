using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ToolBox.Pools;
using UnityEngine.UI;
using DG.Tweening;


public class MenuUI : Subject
{
    public bool isPause = false;
    public GameObject startMenu;
    public GameObject pauseMenu;
    public GameObject DEATHMENU;
    public GameObject levelSelection;
    public GameObject player;
    public LevelWork _levelCreate;
    public GameObject gameManager;
    public GameManager _gameManager;
    public GameObject waveDone;
    public Button[] levelButton;
    public void Start()
    {
        for (int i = 0; i < levelButton.Length; i++)
        {
            levelButton[i].interactable = false;
        }
        for (int i = 0; i <= _levelCreate.maxLevel-1;i++)
        {
            levelButton[i].interactable = true;
        }
       
         gameManager = GameObject.Find("GameManager");
        _gameManager = gameManager.GetComponent<GameManager>();
        player = GameObject.Find("Player");
        OnGamePause();
    }
    public void OpenLevel(int Level)
    {
        levelSelection.SetActive(false);
        OnGamePause();
        _levelCreate.currentLevel = Level;
        _levelCreate.StartCoroutine(_levelCreate.StartLevel());
    }
    public void StartFunction()
    {
        NotifyObserver(EventID.OpenStartUI);
       
    }
    public void PauseMenu()
    {
        NotifyObserver(EventID.OpenPauseUI);
       
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        OnGamePause();
    }
    public void LevelUP()
    {
        _levelCreate.currentLevel = _levelCreate.currentLevel + 1;
    }
    public void NextWave()
    {
        _gameManager.currentPoint = 0;
        Time.timeScale = 1;
        PlayerController _player = player.GetComponent<PlayerController>();
        player.transform.position = new Vector3(-0.03f, -3.551f, 0);
        _player.health = _player.maxHealth;
        waveDone.SetActive(false);
        _levelCreate.StartCoroutine(_levelCreate.StartLevel());
    }    
    public void ResetScene()
    {
        _gameManager.currentPoint = 0;
        Time.timeScale = 1;
        PlayerController _player = player.GetComponent<PlayerController>();
        player.transform.position = new Vector3(-0.03f, -3.551f, 0);      
        _player.health = _player.maxHealth;
        DEATHMENU.SetActive(false);
        _levelCreate.StartCoroutine(_levelCreate.StartLevel());
        _levelCreate.currentEnemyDestroy = 0;
    }    
    public void playerDeath()
    {
        DEATHMENU.SetActive(true);
        Time.timeScale = 0;
        _levelCreate.StopAllCoroutines();
        
        foreach (GameObject e in _gameManager.Enemy)
        {
            BaseShipEnemy a = e.GetComponent<BaseShipEnemy>();
            a.DestroySelf();
         
        }
        foreach (GameObject bullet in _gameManager.bullet)
        {
            bullet.gameObject.tag = "Untagged";
            bullet.GetComponent<SpriteRenderer>().color = Color.white;
            bullet.Release();
            Bullet a = bullet.GetComponent<Bullet>();
        }
        _gameManager.Enemy.Clear();
        _gameManager.bullet.Clear();
       
    }
    public void OnGamePause()
    {
        if(isPause==true)
        {
            isPause = false;
            Time.timeScale = 1;
        }
        else 
        {
            isPause = true;
            Time.timeScale = 0;
        }

    }
    public void Update()
    {
      
    }
}

