using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolBox.Pools; 

public class WaveCreate : MonoBehaviour
{
    [SerializeField]
    private WaveDetail waveDetail;
    private int currentEnemyDestroy;
    private WaveDetail.Wave wave;
    private int currentWaveList;
    public GameObject meteorite;
    public GameObject BaseShipEnemy;
   
    // Start is called before the first frame update
    void Start()
    {
        currentWaveList= 0;
        StartCoroutine(StartLevel());
    }

    // Update is called once per frame
  public  IEnumerator StartLevel()
    {
        for (int i=0; i<waveDetail.waveList.Count;i++)
        {
            currentWaveList++;
            currentEnemyDestroy = 0;
            wave = waveDetail.waveList[i];
            for(int j=0;j<wave.enemySpawnList.Count;j++) 
            {
                StartCoroutine(SpawnEnemyList(wave.enemySpawnList[j]));
            }

        }
        yield return new WaitUntil(()=>(currentEnemyDestroy==wave.totalEnemyInWave));
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
                meteorite.gameObject.Reuse(new Vector3(transform.position.x+UnityEngine.Random.Range(-3,3),transform.position.y),transform.rotation);
                 break;
                case EnemyType.BaseEnemyShip:
                enemy=BaseShipEnemy.gameObject.Reuse(transform.position,transform.rotation);
                    BaseShipEnemy baseShipEnemy = enemy.GetComponent<BaseShipEnemy>();
                    
                    baseShipEnemy.Init(spawn.mainPath);
                    
                   
                    break;
                    

            }
            yield return new WaitForSeconds(spawn.timeDelay);
        }    
        
    }
}
