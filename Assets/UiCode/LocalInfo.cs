using UnityEngine;
using System.Collections.Generic;

public class LocalInfo : MonoBehaviour
{

    [Header("# Data")]
    public Dictionary<UnitData, int> enemyData;
    public string[] unitPositionData = new string[9];
    public LocalData localData;
    public UnitData bossData;

    [Header("# Object")]
    public GameObject enemyList;
    public GameObject local;
    public GameObject boss;



}
