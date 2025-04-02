using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "SqaudData", menuName = "Scriptable Objects/SqaudData")]
public class SqaudData : ScriptableObject
{
    public int level;
    public List<UnitData> members;
    public List<int> memeberNums;   
}
