using System.Collections.Generic;
using UnityEngine;

public class HandOverData : MonoBehaviour
{
    [Header("#Position")]
    public string[] unitPositions = new string[9];


    [Header("# Node & Edge")]
    public List<GameObject> nodes = new List<GameObject>();
    public List<Line> Roads = new List<Line>();


    [Header("# OpenLocal")]
    public GameObject openLocal;
}
