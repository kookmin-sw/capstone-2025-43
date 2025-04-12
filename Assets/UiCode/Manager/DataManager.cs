using System.IO;
using UnityEditor;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public HandOverData handOverData;
    public UnitData[] GetUnitDataset(string path)
    {
        return Resources.LoadAll<UnitData>($"Data/Unit/{path}");
    }
    public LocalData GetLocalData(string path)
    {
        return Resources.Load<LocalData>($"Data/Local/{path}");
    }
}
