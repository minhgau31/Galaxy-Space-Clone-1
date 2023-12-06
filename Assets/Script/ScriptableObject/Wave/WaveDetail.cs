using System.Collections;
using System.Collections.Generic;
using ToolBox.Pools;
using DG.Tweening;
using UnityEngine;
using System.Linq;
using System;

[CreateAssetMenu(menuName ="Create WaveTable",fileName ="WaveTable")]
public class WaveDetail : ScriptableObject
{
    [Serializable]
    public class Wave
    {
        [SerializeField]
        public float timerinSecond;
        public WaveType waveType;
        public List<EnemySpawn> enemySpawnList;
        public int totalEnemyInWave
        {
            get { return enemySpawnList.Sum(x => x.enemyNum);
            }
        }
       

       
    }

    [SerializeField]
    public List<Wave> waveList;
}
public enum WaveType
{
    Normal,
    Time,
}
