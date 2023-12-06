using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Create EnemyStats", fileName = "EnemyStats")]
public class EnemyDetail : ScriptableObject
{
 

    [Serializable]
    public class EnemyStats
    {
        public int id;
        public int Health;
        public bool LookAtPlayer;
        public float rotationSpeed;
        public int point;
    }
    public List<EnemyStats> enemyStats;
}

