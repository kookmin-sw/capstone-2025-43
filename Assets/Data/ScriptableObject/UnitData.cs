using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Objects/UnitData")]
public class UnitData : ScriptableObject
{
    [Header("# cur Info")]
    public int curHp;
    public int curMp;

    [Header("# static Info")]
    public int maxHp;
    public int maxMp;
    public int damage;
}
