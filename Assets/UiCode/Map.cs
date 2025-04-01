using UnityEngine;
using System.Collections.Generic;

public class Map : MonoBehaviour
{
    const int nodeNum = 40;
    public DelaunayTriangulation DTri;
    public NodePosition nodePosition;
    public List<Vector2> positions = new List<Vector2>();
    public Base baseObject;

    public void CreateNodes()
    {
        Debug.Log("Random position");
        while(positions.Count < nodeNum)
        {
            Vector2 position = nodePosition.CreateRandomSpot();
            if (!baseObject.inmyBound(position))
            {
                continue;
            }
            positions.Add(position);
            GameObject tmpObject = Managers.instance.resourceManager.Instantiate("Node", nodePosition.transform);
            if (Managers.instance.gameManager.inBorderAlly(position))
                tmpObject.GetComponent<Node>().Init("Ally", position);
            else
                tmpObject.GetComponent<Node>().Init("Enemy", position);
        }
    }
    
    public void CreateMap()
    {
        CreateNodes();

        DTri.Init(70, 70);
        foreach(Vector2 position in positions)
        {
            Debug.Log("������");
            DTri.AddPoint(position);
        }
        DTri.RemoveSuperTriangle();
    }
}
