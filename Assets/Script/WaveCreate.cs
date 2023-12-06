using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolBox.Pools;

public class WaveCreate : MonoBehaviour
{
    [SerializeField]
    public WaveDetail waveDetail;
    public int currentEnemyDestroy;
    public WaveDetail.Wave wave;
    public int currentWaveList;
    public GameObject meteorite;
    public GameObject BaseShipEnemy;
    public GameObject EnemyShipRed;
    public GameObject Boss;
    public GameObject gameManager;
    public GameManager _gameManager;
    public float timerCount;
    public bool isTimer = false;

    // Start is called before the first frame update
    void Start()
    {
        currentWaveList = 0;
        StartCoroutine(StartLevel());
        gameManager = GameObject.Find("GameManager");
        _gameManager = gameManager.GetComponent<GameManager>();

    }

    // Update is called once per frame
    public IEnumerator StartLevel()
    {
       
                for (int i = 0; i < waveDetail.waveList.Count; i++)
                {
                    currentWaveList++;
                    currentEnemyDestroy = 0;
                    wave = waveDetail.waveList[i];
                    for (int j = 0; j < wave.enemySpawnList.Count; j++)
                    {
                        StartCoroutine(SpawnEnemyList(wave.enemySpawnList[j]));
                    }
                    Debug.Log("TOTAL ENEMY IN WAVE" + wave.totalEnemyInWave);
                    Debug.Log("CURRENT ENEMY DESTROY" + currentEnemyDestroy);
                    switch (wave.waveType)
                    {
                         case WaveType.Normal:
                            isTimer = false;
                         yield return new WaitUntil(() => (currentEnemyDestroy == wave.totalEnemyInWave));                  
                         break;
                        case WaveType.Time:
                        isTimer = true;
                        yield return new WaitUntil(() => (wave.timerinSecond <=0));
                        break;
                    }

                }

    }
    public void Update()
    {
        if(isTimer==true)
        {
            timerCount = wave.timerinSecond;
            timerCount -= Time.deltaTime;
            DisplayTime(timerCount);
        }
        else
        {
            _gameManager.timerText.text = "";
        }
        Debug.Log(isTimer + "IS TIMER");
    }
    public void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay<0)
        {
            timeToDisplay = 0;
        }
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.Floor(timeToDisplay % 60);
        _gameManager.timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
         
    }
    private void EnemyGotDestroy()
    {
        currentEnemyDestroy++;
    }
  public  IEnumerator SpawnEnemyList(EnemySpawn spawn)
    {
        yield return new WaitForSeconds(spawn.timeStart);
        for(int i=0;i<spawn.enemyNum;i++)
        {
            //Spawn Enemy
            GameObject enemy = null;
            
            switch(spawn.enemyType)
            {
                case EnemyType.Meteorite:
                enemy =meteorite.gameObject.Reuse(new Vector3(transform.position.x+UnityEngine.Random.Range(-3,3),transform.position.y),transform.rotation);
                 break;
                case EnemyType.BaseEnemyShip:
                enemy=BaseShipEnemy.gameObject.Reuse();                 
                break;
                case EnemyType.EnemyShipRed:
                enemy = EnemyShipRed.gameObject.Reuse();
                break;
                case EnemyType.Boss:
                    enemy = Boss.gameObject.Reuse();
                    break;
            }
            BaseShipEnemy baseShipEnemy = enemy.GetComponent<BaseShipEnemy>();
            baseShipEnemy.Init(spawn.mainPath,spawn.additionPath);
            baseShipEnemy.OnEnemyDestroy += EnemyGotDestroy;
            _gameManager.Enemy.Add(enemy);
            Debug.Log("SPAWN ENEMY");
            yield return new WaitForSeconds(spawn.timeDelay);
        }    
        
    }
    
}
