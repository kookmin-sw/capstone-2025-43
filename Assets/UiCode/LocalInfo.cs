using UnityEngine;
using System.Collections.Generic;

public class LocalInfo
{
    [Header("# Data")]
    public List<List<BattleWavePreset>> battleWaves = new List<List<BattleWavePreset>>();
    public LocalData localData = null;
    public Vector3 poisiton;
    public string side;
    public LocalInfo(Vector3 point , string tag)
    {
        this.poisiton = point;
        this.side = tag;
    }
}
