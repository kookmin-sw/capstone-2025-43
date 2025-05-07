using System.IO;
using UnityEditor;
using UnityEngine;

public class DataManager
{
    public HandOverData handOverData;

    public void Init()
    {
        if (handOverData == null) {
            GameObject go = GameObject.Find("@HandOverData");
            if (go == null)
            {
                go = new GameObject { name = "@HandOverData" };
                go.AddComponent<HandOverData>();
                go.GetComponent<HandOverData>().Init();
            }
            handOverData = go.GetComponent<HandOverData>();
        }
    }

    public BattleWavePreset[] GetBattleWaveDataset(string path)
    {
        return Resources.LoadAll<BattleWavePreset>($"Data/Unit/{path}");
    }
    public LocalData GetLocalData(string path)
    {
        return Resources.Load<LocalData>($"Data/Local/{path}");
    }
}
