using System.IO;
using UnityEditor;
using UnityEngine;

public class DataManager
{
    public ScriptableObject GetSOData(string path , string name)
    {
        return (ScriptableObject)AssetDatabase.LoadAssetAtPath($"Data/{path}", typeof(ScriptableObject));
    }
}
