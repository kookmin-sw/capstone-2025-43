using UnityEngine;
using System.Collections.Generic;
public class LocalInfo : MonoBehaviour
{
    [Header("# Data")]
    public Dictionary<UnitData, int> enemyData;
    public LocalData localData;
    public UnitData bossData;

    [Header("# Object")]
    public GameObject enemyList;
    public GameObject local;
    public GameObject boss;



}
