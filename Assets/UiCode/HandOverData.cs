using System.Collections.Generic;
using UnityEngine;

public class HandOverData : MonoBehaviour
{
    [Header("#Position")]
    public string[] unitPositions = new string[9];


    [Header("# Node & Edge")]
    public Dictionary<Vector2, LocalInfo> localInfos = new Dictionary<Vector2, LocalInfo>();
    public List<Edge> roads = new List<Edge>();

    [Header("# OpenLocal")]
    public Vector2 openLocal;

    public string GetOpenLocalEnv()
    {
        return localInfos[openLocal].localData.env;
    }
    public List<BattleWavePreset> GetOpenLocalMonsterWave()
    {
        return localInfos[openLocal].battleWaves[0];
    }

    public void Init()
    {
        DontDestroyOnLoad(gameObject);
    }
}
