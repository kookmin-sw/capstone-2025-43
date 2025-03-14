using UnityEngine;

[CreateAssetMenu(fileName = "IconData", menuName = "Scriptable Objects/IconData")]
public class IconData : ScriptableObject
{
    [Header("# Icon Info")]
    public Sprite iconImg;
    public Sprite descImg;
    public string descText;
}
