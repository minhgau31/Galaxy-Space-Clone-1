using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class EnemySpawn
{
     public int timeStart;
     public int timeDelay;
     public EnemyType enemyType;
     public int enemyNum;
     public Vector3 Position;
     public DOTweenPath mainPath;
    public DOTweenPath additionPath;
}
