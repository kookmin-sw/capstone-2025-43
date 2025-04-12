using System.Collections.Generic;
using UnityEngine;

public class HandOverData : MonoBehaviour
{
    [Header("#Position")]
    public string[] unitPositions = new string[9];


    [Header("# Node & Edge")]
    public List<Vector2> allyNodes = new List<Vector2>();
    public List<Vector2> enemyNodes = new List<Vector2>();
    public List<Edge> roads = new List<Edge>();

    [Header("# OpenLocal")]
    public Vector2 openLocal;
}
