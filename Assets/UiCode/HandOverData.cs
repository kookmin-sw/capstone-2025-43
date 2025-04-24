using System.Collections.Generic;
using UnityEngine;

public class HandOverData : MonoBehaviour
{
    [Header("#Position")]
    public string[] unitPositions = new string[9];


    [Header("# Node & Edge")]
    public Dictionary<Vector2, LocalInfo> localInfos = new Dictionary<Vector2, LocalInfo>();
    public List<Edge> roads = new List<Edge>();

    [Header("# OpenLocal")]
    public Vector2 openLocal;

    public void Init()
    {
        DontDestroyOnLoad(gameObject);
    }
}
