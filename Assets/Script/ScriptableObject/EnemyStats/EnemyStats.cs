using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Create EnemyStats", fileName = "EnemyStats")]
public class EnemyStats : ScriptableObject
{
    [SerializeField]
    public int Health;
    public bool LookAtPlayer;
    public float rotationSpeed;

}
