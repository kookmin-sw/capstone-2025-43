using System;
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

    public T Load<T>(string filename) where T : class
    {
        if (File.Exists(filename))
        {
            try
            {
                string json = File.ReadAllText(filename);
                return JsonUtility.FromJson<T>(json);
            }
            catch (Exception e)
            {
                Debug.LogError($"[Load Error] {e.Message}");
            }
        }
        return null;
    }

    public void Save<T>(string filename, T data) where T : class
    {
        try
        {
            string json = JsonUtility.ToJson(data, true); // true = pretty print
            File.WriteAllText(filename, json);
        }
        catch (Exception e)
        {
            Debug.LogError($"[Save Error] {e.Message}");
        }
    }
}
