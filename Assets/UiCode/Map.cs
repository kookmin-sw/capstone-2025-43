using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using NUnit.Framework.Constraints;

public class Map : MonoBehaviour
{
    const int nodeNum = 40;
    public DelaunayTriangulation DTri;
    public NodePosition nodePosition;
    public Dictionary<Vector2, GameObject> nodes = new Dictionary<Vector2, GameObject>();
    public Dictionary<Edge, int> edges = new Dictionary<Edge, int>();
    public Base baseObject;

    public void CreateNodes()
    {
        Debug.Log("Random position");
        while(nodes.Count < nodeNum)
        {
            Vector2 position = nodePosition.CreateRandomSpot();
            if (!baseObject.inmyBound(position) || position == null)
            {
                continue;
            }
            GameObject tmpObject = Managers.instance.resourceManager.Instantiate("Node", nodePosition.transform);
            if (Managers.instance.gameManager.inBorderAlly(position))
                tmpObject.GetComponent<Node>().Init("Ally", position);
            else
                tmpObject.GetComponent<Node>().Init("Enemy", position);
            nodes.Add(position, tmpObject);
        }
    }
    private void SetEdge()
    {
        int idx = 0;
        foreach (Triangle triangle in DTri.triangles)
        {
            
            foreach (Edge edge in triangle.edges)
            {
                if (!edges.ContainsKey(edge))
                {
                    Debug.Log($"Edge{idx}");
                    idx++;
                    edges.Add(edge, 0);
                    GameObject road = Managers.instance.resourceManager.Instantiate("Road", DTri.transform);
                    road.GetComponent<Line>().Init(nodes[edge.v0], nodes[edge.v1]);
                    road.name = $"Road{idx}";
                }
            }
        }
    }

    public void CreateMap()
    {
        CreateNodes();
        DTri.Init(70, 70);

        int idx = 0;
        foreach (Vector2 point in nodes.Keys)
        {
            Debug.Log($"map :{idx} ��° ����");
            DTri.AddPoint(point);
            idx++;
        }

        DTri.RemoveSuperTriangle();
        SetEdge();
    }
}
