using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using NUnit.Framework.Constraints;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class Map : MonoBehaviour
{
    const int nodeNum = 40;
    public DelaunayTriangulation DTri;
    public NodePosition nodePosition;
    public GameObject roads;
    public GameObject locals;
    public Dictionary<Vector2, GameObject> nodes = new Dictionary<Vector2, GameObject>();
    public Base baseObject;

    private void Awake()
    {
        DTri = GetComponent<DelaunayTriangulation>();
        nodePosition = GetComponent<NodePosition>();
    }

    private void CreateNode()
    {
        nodes.Clear();
        int idx = 0;
        foreach (Vector2 position in Managers.instance.dataManager.handOverData.allyNodes)
        {
            GameObject tmpObject = Managers.instance.resourceManager.Instantiate("Node", roads.transform);
            tmpObject.GetComponent<Node>().Init("Ally",position);
            tmpObject.name = $"node {idx}";
            nodes.Add(position, tmpObject);
            idx++;
        }
        foreach (Vector2 position in Managers.instance.dataManager.handOverData.enemyNodes)
        {
            GameObject tmpObject = Managers.instance.resourceManager.Instantiate("Node", roads.transform);
            tmpObject.GetComponent<Node>().Init("Enemy", position);
            tmpObject.name = $"node {idx}";
            nodes.Add(position, tmpObject);
            idx++;
        }
    }
    private void CreateRoad()
    {
        int idx = 0;
        foreach (Edge edge in Managers.instance.dataManager.handOverData.roads)
        {
            GameObject road = Managers.instance.resourceManager.Instantiate("Road", locals.transform);
            road.GetComponent<Line>().Init(nodes[edge.v0], nodes[edge.v1]);
            road.name = $"Road{idx}";
            idx++;
        }
    }
    private void SetPosition()
    {
        Debug.Log("Random position");
        int nodeCnt = 0;
        while( nodeCnt < nodeNum) 
        {
            Vector2 position = nodePosition.CreateRandomSpot();
            if (!baseObject.inmyBound(position) || position == null)
            {
                continue;
            }
            nodeCnt++;
            if (Managers.instance.gameManager.inBorderAlly(position))
                Managers.instance.dataManager.handOverData.allyNodes.Add(position);
            else
                Managers.instance.dataManager.handOverData.enemyNodes.Add(position);
        }
    }

    private void SetEdge()
    {
        Dictionary<Edge, int> edges = new Dictionary<Edge, int>();
        foreach (Triangle triangle in DTri.triangles)
        {
            foreach (Edge edge in triangle.edges)
            {
                if (!edges.ContainsKey(edge) && !edges.ContainsKey(new Edge(edge.v1, edge.v0)))
                {
                    edges.Add(edge, 0);
                    Managers.instance.dataManager.handOverData.roads.Add(edge);
                }
            }
        }
    }

    public void CreateMap()
    {
        roads = new GameObject();
        roads.name = "Roads";
        locals = new GameObject();
        locals.name = "Locals";
        CreateNode();
        CreateRoad();
        baseObject.EnvCreate("Desert");
        baseObject.EnvCreate("NorthLand");
        baseObject.EnvCreate("DarkForest");
        baseObject.EnvCreate("Mount");
    }

    public void Init()
    {
        SetPosition();
        DTri.Init(70, 70);

        foreach (Vector2 point in Managers.instance.dataManager.handOverData.allyNodes)
        {
            DTri.AddPoint(point);
        }
        foreach (Vector2 point in Managers.instance.dataManager.handOverData.enemyNodes)
        {
            DTri.AddPoint(point);
        }
        DTri.RemoveSuperTriangle();
        SetEdge();

        
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
