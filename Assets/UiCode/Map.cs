using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using NUnit.Framework.Constraints;
using System.Collections;
using Unity.VisualScripting;

public class Map : MonoBehaviour
{
    const int nodeNum = 40;
    public DelaunayTriangulation DTri;
    public NodePosition nodePosition;
    public Dictionary<Vector2, GameObject> nodes = new Dictionary<Vector2, GameObject>();
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
            tmpObject.name = $"node {nodes.Count}";

            if (Managers.instance.gameManager.inBorderAlly(position))
                tmpObject.GetComponent<Node>().Init("Ally", position);
            else
                tmpObject.GetComponent<Node>().Init("Enemy", position);
            nodes.Add(position, tmpObject);
            Managers.instance.dataManager.handOverData.nodes.Add(tmpObject.GetComponent<Node>());
        }
    }
    private void SetEdge()
    {

        Dictionary<Edge, int> edges = new Dictionary<Edge, int>();
        int idx = 0;
        foreach (Triangle triangle in DTri.triangles)
        {
            foreach (Edge edge in triangle.edges)
            {
                if (!edges.ContainsKey(edge) && !edges.ContainsKey(new Edge(edge.v1, edge.v0)))
                {
                    idx++;
                    edges.Add(edge, 0);
                    GameObject road = Managers.instance.resourceManager.Instantiate("Road", DTri.transform);
                    road.GetComponent<Line>().Init(nodes[edge.v0], nodes[edge.v1]);
                    Managers.instance.dataManager.handOverData.Roads.Add(road.GetComponent<Line>());
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
            Debug.Log($"map :{idx} 번째 시작");
            DTri.AddPoint(point);
            idx++;
        }

        DTri.RemoveSuperTriangle();
        SetEdge();
        baseObject.EnvCreate("Desert");
        baseObject.EnvCreate("NorthLand");
        baseObject.EnvCreate("DarkForest");
        baseObject.EnvCreate("Mount");
    }

    public List<Line> GetLines()
    {
        List<Line> attack = new List<Line>();

        for (int i = 0; i < DTri.transform.childCount; i++)
        {
            Line a = DTri.transform.GetChild(i).GetComponent<Line>();
            if (a.node0.tag != a.node1.tag)
                attack.Add(a);
        }
        return attack;
    }
    public void ReSetRoads()
    {
        GameObject[] objects = DTri.GetComponentsInChildren<GameObject>();
        foreach (GameObject obj in objects)
        {
            obj.GetComponent<Line>().SetColor();
        }
    }

}
