using UnityEngine;
using System.Collections.Generic;

public class LocalInfo : MonoBehaviour
{
    [Header("# Data")]
    public List<List<BattleWavePreset>> battlewave = new List<List<BattleWavePreset>>();
    public string[] unitPositionData = new string[9];
    public LocalData localData;
    public Vector2 poisiton;
}
