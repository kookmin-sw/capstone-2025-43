using UnityEngine;
using System.Collections.Generic;

public class Map : MonoBehaviour
{
    const int nodeNum = 40;
    public DelaunayTriangulation DTri;
    public NodePosition nodePosition;
    public List<GameObject> nodes = new List<GameObject>();
    public Base baseObject;

    public void CreateNodes()
    {
        Debug.Log("Random position");
        while(nodes.Count < nodeNum)
        {
            Vector2 position = nodePosition.CreateRandomSpot();
            if (!baseObject.inmyBound(position))
            {
                continue;
            }
            GameObject tmpObject = Managers.instance.resourceManager.Instantiate("Node", nodePosition.transform);
            if (Managers.instance.gameManager.inBorderAlly(position))
                tmpObject.GetComponent<Node>().Init("Ally", position);
            else
                tmpObject.GetComponent<Node>().Init("Enemy", position);
            nodes.Add(tmpObject);
        }
    }
    
    public void CreateMap()
    {
        CreateNodes();
        DTri.Init(70, 70);
        foreach(GameObject node in nodes)
        {
            Debug.Log("»ý¼ºÁß");
            DTri.AddPoint(node);
        }
        DTri.RemoveSuperTriangle();
    }
}
