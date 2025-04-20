using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using NUnit.Framework.Constraints;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using static Unity.Burst.Intrinsics.X86.Sse4_2;

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
        foreach (var info  in Managers.Data.handOverData.localInfos)
        {
            idx++;
            GameObject tmpObject = Managers.Resource.Instantiate("Node", locals.transform);
            tmpObject.GetComponent<Node>().Init(info.Value, info.Key);
            tmpObject.name = $"node {idx}";
            nodes.Add(info.Value.poisiton, tmpObject);
        }
    }
    private void CreateRoad()
    {
        int idx = 0;
        foreach (Edge edge in Managers.Data.handOverData.roads)
        {
            GameObject road = Managers.Resource.Instantiate("Road", roads.transform);
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

            if (Managers.Game.inBorderAlly(position))
                Managers.Data.handOverData.localInfos.Add(nodeCnt, new LocalInfo(position, "Ally"));
            else
                Managers.Data.handOverData.localInfos.Add(nodeCnt, new LocalInfo(position, "Enemy"));
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
                    Managers.Data.handOverData.roads.Add(edge);
                }
            }
        }
    }

    public void CreateMap()
    {
        Destroy(baseObject.gameObject);
        roads = new GameObject();
        roads.name = "Roads";
        locals = new GameObject();
        locals.name = "Locals";
        CreateNode();
        CreateRoad();
        EnvCreate("Desert");
        EnvCreate("NorthLand");
        EnvCreate("DarkForest");
        EnvCreate("Mount");
    }

    public void Init()
    {
        SetPosition();
        DTri.Init(70, 70);

        foreach (LocalInfo info in Managers.Data.handOverData.localInfos.Values)
        {
            Vector2 point = info.poisiton;
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

    public void EnvCreate(string env)
    {
        GameObject a = Managers.Resource.Instantiate(env, this.transform);
        a.AddComponent<PolygonCollider2D>();
    }

}
