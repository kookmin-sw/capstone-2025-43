using UnityEngine;

[CreateAssetMenu(fileName = "LocalData", menuName = "Scriptable Objects/LocalData")]
public class LocalData : ScriptableObject
{
    [Header("# localData")]
    public string localname;
    public string env;
    public string desc;
    public Sprite image;
}
