using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ToolBox.Serialization;

public class GameManager : Subject
{
    public List<GameObject> bullet;
    public List<GameObject> Enemy;
    public Text timerText;
    public Text pointText;
    public Text highestPointText;
    public int tempHighScore;
    public GameObject levelCreate;
    public LevelWork _levelCreate;
    public GameObject MenuUI;
    public MenuUI _menuUI;
    public int currentPoint;
    public List<int> highestScore;
    public RandomUpgradeButton _upgradeButton;
    public void Start()
    {

        levelCreate = GameObject.Find("LevelManager");
        _levelCreate = levelCreate.GetComponent<LevelWork>();
        MenuUI = GameObject.Find("MENUUI");
        _menuUI = MenuUI.GetComponent<MenuUI>();
        tempHighScore = 0;
       
        Debug.Log("LEVEL" + _levelCreate.maxLevel);
        for (int i = 0; i < _levelCreate.maxLevel-1; i++)
        {
            if (DataSerializer.HasKey("SaveHighestScore2" + i.ToString()))
            {
                highestScore[i] = DataSerializer.Load<int>("SaveHighestScore2" + i.ToString());
            }
                   
        }
    }
    public void SaveHighScore()
    {
        if (highestScore[_levelCreate.currentLevel - 1] < currentPoint)
        {
            highestScore[_levelCreate.currentLevel - 1] = currentPoint;
            DataSerializer.Save("SaveHighestScore2" + (_levelCreate.currentLevel - 1).ToString(), highestScore[_levelCreate.currentLevel - 1]);
            Debug.Log("SAVE HIGHEST SCORE");
        }
    }
    public void Update()
    {
        NotifyObserver(EventID.ChangedPoint);
        //pointText.text = currentPoint.ToString();
        Debug.Log(_levelCreate.wave.totalEnemyInWave+" TOTAL ENEMY IN WAVE");
        Debug.Log(_levelCreate.currentEnemyDestroy+" CURRENT ENEMY ");
        SaveHighScore();
        Debug.Log(_levelCreate.maxLevel + " MAX LEVEL");
        Debug.Log(_levelCreate.currentLevel);
        if (_levelCreate.currentWaveList == _levelCreate._level._waveDetail[_levelCreate.currentLevel - 1].waveList.Count)
        {
            if (_levelCreate.isTimer == false)
            {
                if (_levelCreate.currentEnemyDestroy == _levelCreate.wave.totalEnemyInWave)
                {
                    _levelCreate.currentWaveList = 0;
                    _menuUI.LevelUP();
                    _levelCreate.currentEnemyDestroy = 0;
                   // _menuUI.waveDone.SetActive(true);
                    _upgradeButton.Init();
                    currentPoint = 0;
                    Time.timeScale = 0;
                }
            }
            if (_levelCreate.isTimer == true)
            {
                if (_levelCreate.currentEnemyDestroy == _levelCreate.wave.totalEnemyInWave)
                {
                    currentPoint += (int)_levelCreate.timerCount * 50;
                    pointText.text = currentPoint.ToString();
                    SaveHighScore();
                    _levelCreate.currentWaveList = 0;                    
                    _levelCreate.currentEnemyDestroy = 0;
                    //_menuUI.waveDone.SetActive(true);
                    _menuUI.LevelUP();
                    _upgradeButton.Init();
                    currentPoint = 0;
                    Time.timeScale = 0;
                }
                if (_levelCreate.timerCount<0)
                {
                    _levelCreate.currentWaveList = 0;
                    _menuUI.LevelUP();
                    _levelCreate.currentEnemyDestroy = 0;
                   // _menuUI.waveDone.SetActive(true);
                    _upgradeButton.Init();
                    Time.timeScale = 0;
                }
            }
        }
    }
}
