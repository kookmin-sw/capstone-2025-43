using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "Scriptable Objects/Unit")]

public class Unit : ScriptableObject
{
    [Header("# Current Unit Info")]
    public int curHp;
    public int curMp;


    [Header("# Static Unit Info")]
    public int maxHp;
    public int maxMp;
    public int damage;
}
