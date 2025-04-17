using System.Collections.Generic;
using UnityEngine;

public class HandOverData : MonoBehaviour
{
    [Header("#Position")]
    public string[] unitPositions = new string[9];


    [Header("# Node & Edge")]
    public Dictionary<int, LocalInfo> localInfos = new Dictionary<int, LocalInfo>();
    public List<Edge> roads = new List<Edge>();

    [Header("# OpenLocal")]
    public int openLocal;
}
