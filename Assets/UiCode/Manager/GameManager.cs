using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UiManager uiManager;
    public Map map;

    public Day day;

    public float GameTime;
    public bool isPause = false;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        /*
        nodePosition = GetComponent<NodePosition>();
        DTri = GetComponent<DelaunayTriangulation>();
        uiManager = GetComponent<UiManager>();
        */
        map.CreateNodes();
    }

    public void GamePause()
    {
        isPause = true;
        Time.timeScale = 0.0f;
    }
    public void GameResume()
    {
        isPause = false;
        Time.timeScale = 1.0f;
    }
}
