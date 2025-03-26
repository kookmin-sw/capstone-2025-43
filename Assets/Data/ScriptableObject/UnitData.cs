using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Objects/UnitData")]
public class UnitData : ScriptableObject
{
    [Header("# cur Info")]
    public int curHp;
    public int curMp;

    [Header("# static Info")]
    public string heroName;
    public int price;
    public Sprite heroImage;
    public int maxHp;
    public int maxMp;
    public int damage;
}
