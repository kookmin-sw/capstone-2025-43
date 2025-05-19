using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DataManager
{
    public HandOverData handOverData = new HandOverData();

    public BattleWavePreset[] GetBattleWaveDataset(string path)
    {
        return Resources.LoadAll<BattleWavePreset>($"Data/Unit/{path}");
    }
    public LocalData GetLocalData(string path)
    {
        return Resources.Load<LocalData>($"Data/Local/{path}");
    }
    public string GetOpenLocalEnv()
    {
        return handOverData.localInfos[handOverData.openLocal].localData.env;
    }
    public List<BattleWavePreset> GetOpenLocalMonsterWave()
    {
        return handOverData.localInfos[handOverData.openLocal].battleWaves;
    }
}
