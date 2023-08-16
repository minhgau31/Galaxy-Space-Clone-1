using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class EnemySpawn
{
     public int timeStart;
     public EnemyType enemyType;
     public int enemyNum;
     public Vector3 Position;
     public DOTweenPath mainPath;
}
