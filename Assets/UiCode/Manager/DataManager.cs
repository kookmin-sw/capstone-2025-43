using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DataManager
{
    public HandOverData handOverData = new HandOverData();
    public Dictionary<Vector2, LocalInfo> localInfos = new Dictionary<Vector2, LocalInfo>();
    public void SetDictionary()
    {
        foreach (var vl in handOverData.list_localinfos)
        {
            localInfos[vl.localPosition] = vl.localInfo;
        }
    }

    public void SetList()
    {
        handOverData.list_localinfos.Clear();
        foreach (var vl in localInfos)
        {
            handOverData.list_localinfos.Add(new vector_localinfo { localPosition = vl.Key, localInfo = vl.Value });
        }
    }

    public BattleWavePreset[] GetBattleWaveDataset()
    {
        return Resources.LoadAll<BattleWavePreset>($"Data/Unit/BattleWave");
    }
    public LocalData GetLocalData(string path)
    {
        return Resources.Load<LocalData>($"Data/Local/{path}");
    }
    public string GetOpenLocalEnv()
    {
        return localInfos[handOverData.openLocal].localData.env;
    }
    public List<BattleWavePreset> GetOpenLocalMonsterWave()
    {
        return localInfos[handOverData.openLocal].battleWaves;
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
        else
        {
            Debug.LogWarning($"[Load] File not found: {filename}");
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
