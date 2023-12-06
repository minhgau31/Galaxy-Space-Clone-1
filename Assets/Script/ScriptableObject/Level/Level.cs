using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Create Level", fileName = "Level")]
public class Level : ScriptableObject
{
    public List<WaveDetail> _waveDetail;
}
