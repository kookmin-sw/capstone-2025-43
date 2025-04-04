using System.IO;
using UnityEditor;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public UnitData[] GetUnitDataset(string path)
    {
        return Resources.LoadAll<UnitData>($"Data/Unit/{path}");
    }
}
