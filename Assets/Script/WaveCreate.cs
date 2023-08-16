using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCreate : MonoBehaviour
{
    [SerializeField]
    private WaveDetail waveDetail;
    private int currentEnemyDestroy;
    private WaveDetail.Wave wave;
    private int currentWaveList;
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

        yield return new WaitForSeconds(0.5f);
    }
}
