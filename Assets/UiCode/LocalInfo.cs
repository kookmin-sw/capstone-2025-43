using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class LocalInfo
{
    [Header("# Data")]
    public List<BattleWavePreset> battleWaves = new List<BattleWavePreset>();
    public LocalData localData = null;
    public Vector3 poisiton;
    public string side;
    public LocalInfo(Vector3 point , string tag)
    {
        this.poisiton = point;
        this.side = tag;
    }
}
