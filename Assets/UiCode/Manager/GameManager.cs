using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UiManager uiManager;
    public NodePosition nodePosition;
    public DelaunayTriangulation DTri;
    public Day day;

    public float GameTime;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        /*
        nodePosition = GetComponent<NodePosition>();
        DTri = GetComponent<DelaunayTriangulation>();
        uiManager = GetComponent<UiManager>();
        */
        CreateMap();
    }
    public void CreateMap()
    {
        DTri.Init(70, 70);
        for (int i = 0; i < 20; i++)
        {
            Debug.Log("생성중");
            Vector2 p = nodePosition.CreateRandomSpot();
            DTri.AddPoint(p);
            DTri.CreatePoint($"Point", Color.red, p);
        }
        DTri.RemoveSuperTriangle();
    }

    private void Dominate()
    {
       
        //link - move가능 -> ally배열중 random 
    }
}
