using UnityEngine;
using System.Collections.Generic;

public class Map : MonoBehaviour
{
    const int nodeNum = 40;
    public DelaunayTriangulation DTri = new DelaunayTriangulation();
    public NodePosition nodePosition = new NodePosition();
    public GameObject roads;
    public GameObject locals;
    public Border baseObject;
    public List<Border> Envs = new List<Border>();
    private void CreateNode()
    {
        int idx = 0;
        foreach (var info  in Managers.Data.handOverData.localInfos)
        {
            idx++;
            GameObject tmpObject = Managers.Resource.Instantiate("Node", locals.transform);
            tmpObject.GetComponent<Node>().Init(info.Value);
            tmpObject.name = $"node {idx}";
        }
    }
    private void CreateRoad()
    {
        int idx = 0;
        foreach (Edge edge in Managers.Data.handOverData.roads)
        {
            GameObject road = Managers.Resource.Instantiate("Road", roads.transform);
            road.GetComponent<Line>().Init(edge.v0, edge.v1);
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
                Managers.Data.handOverData.localInfos.Add(position, new LocalInfo(position, "Ally"));
            else
                Managers.Data.handOverData.localInfos.Add(position, new LocalInfo(position, "Enemy"));
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
        roads = new GameObject() { name = "Roads" };
        locals = new GameObject() { name = "Locals"};
        EnvCreate("Desert");
        EnvCreate("NorthLand");
        EnvCreate("DarkForest");
        EnvCreate("Mount");
        SetEnv();
        CreateNode();
        CreateRoad();
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
        CreateMap();
    }

    public List<Line> GetLines()
    {
        List<Line> attack = new List<Line>();

        for (int i = 0; i < roads.transform.childCount; i++)
        {
            Line a = roads.transform.GetChild(i).GetComponent<Line>();
            string tag0 = Managers.Data.handOverData.localInfos[a.p0].side;
            string tag1 = Managers.Data.handOverData.localInfos[a.p1].side;

            if (tag0 != tag1)
                attack.Add(a);
        }
        return attack;
    }

    public void ReSetRoads()
    {
        GameObject[] objects = roads.GetComponentsInChildren<GameObject>();
        foreach (GameObject obj in objects)
        {
            obj.GetComponent<Line>().SetColor();
        }
    }

    public void EnvCreate(string env)
    {
        GameObject a = Managers.Resource.Instantiate("mapBase", this.transform);
        a.GetComponent<Border>().Init(Managers.Data.GetLocalData(env));
        Envs.Add(a.GetComponent<Border>());
    }

    public void SetEnv()
    {
        foreach(var info in Managers.Data.handOverData.localInfos)
        {
            foreach(var env in Envs)
            {
                if (env.inmyBound(info.Key))
                {
                    info.Value.localData = env.localData;
                }
            }
        }
    }

}
