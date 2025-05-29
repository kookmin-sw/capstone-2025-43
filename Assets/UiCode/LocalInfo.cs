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
        switch (tag)
        {
            case "Ally":
                break;
            case "Enemy":
                SetStages();
                break;
        }
    }
    public void SetStages()
    {
        int waveCount = Random.Range(1, 3);
        for (int i = 0; i < waveCount; i++)
        {
            battleWaves.Add(Managers.Pool.GetCreepPool());
        }
    }
}
