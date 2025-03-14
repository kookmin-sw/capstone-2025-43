using UnityEngine;

[CreateAssetMenu(fileName = "Icon", menuName = "Scriptable Objects/Icon")]
public class Icon : ScriptableObject
{
    [Header("# Icon Info")]
    public Sprite iconImg;
    public Sprite descImg;
    public string descText;
}
