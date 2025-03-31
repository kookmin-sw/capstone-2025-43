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
        for (int i = 0; i < nodeNum; i++)
        {
            GameObject tmpObject = Managers.instance.resourceManager.Instantiate("Node", nodePosition.transform);
            tmpObject.GetComponent<Node>().Init(nodePosition.GetComponent<NodePosition>().CreateRandomSpot());
            if (!baseObject.inmyBound(tmpObject))
            {
                Debug.Log("삭제");
                Destroy(tmpObject);
            }
            else
            {
                positions.Add(tmpObject.transform.position);
            }
        }
    }
    
    public void CreateMap()
    {
        CreateNodes();

        DTri.Init(70, 70);
        foreach(Vector2 position in positions)
        {
            Debug.Log("생성중");
            DTri.AddPoint(position);
        }
        DTri.RemoveSuperTriangle();
    }
}
