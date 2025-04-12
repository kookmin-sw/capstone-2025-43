using System.Collections.Generic;
using UnityEngine;

public class HandOverData : MonoBehaviour
{
    [Header("#Position")]
    public string[] unitPositions = new string[9];


    [Header("# Node & Edge")]
    public List<Node> nodes = new List<Node>();
    public List<Line> Roads = new List<Line>();

    [Header("# OpenLocal")]
    public GameObject openLocal;
}
