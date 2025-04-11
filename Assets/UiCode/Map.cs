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
        }
    }
    
    public void CreateMap()
    {
        CreateNodes();
        DTri.Init(70, 70);
        foreach(Vector2 position in positions)
        {
            Debug.Log("»ý¼ºÁß");
            DTri.AddPoint(position);
        }
        DTri.RemoveSuperTriangle();
    }
}
