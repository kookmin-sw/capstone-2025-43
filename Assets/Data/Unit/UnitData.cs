using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Objects/UnitData")]
public class UnitData : ScriptableObject
{
    [Header("# cur Info")]
    public int curHp;
    public int curMp;
    public bool own = false;

    [Header("# static Info")]
    public string unitName;
    public int price;
    public Sprite unitImage;
    public int maxHp;
    public int maxMp;
    public int damage;
}
