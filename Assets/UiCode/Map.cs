using UnityEngine;
using System.Collections.Generic;

public class Map : MonoBehaviour
{
    const int nodeNum = 40;
    public DelaunayTriangulation DTri;
    public NodePosition nodePosition;
    public List<Vector2> positions = new List<Vector2>();
    public GameObject nodePrefab;
    public Base baseObject;

    public void CreateNodes()
    {
        Debug.Log("Random position");
        for (int i = 0; i < nodeNum; i++)
        {
            GameObject tmpObject = Instantiate(nodePrefab, nodePosition.CreateRandomSpot(), nodePrefab.transform.rotation);
            tmpObject.transform.parent = nodePosition.transform;
            Debug.Log(tmpObject);
            if (!baseObject.inmyBound(tmpObject))
            {
                Debug.Log("삭제");
                Destroy(tmpObject);
            }
        }
       
    }
    
    public void CreateMap()
    {
        DTri.Init(70, 70);
        for (int i = 0; i < nodePosition.transform.childCount; i++)
        {
            Debug.Log("생성중");
            DTri.AddPoint(nodePosition.positions[i]);
        }
        DTri.RemoveSuperTriangle();
    }
}
