using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Map map;
    public Day day;
    public GameObject Roads;

    public int xBorderAlly = 0;
    public int yBorderAlly = 5;

    public float GameTime;
    public bool isPause = false;
    public void GameStart()
    {
        map.CreateMap();
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

    public bool inBorderAlly(Vector2 position)
    {
        return position.x < xBorderAlly && position.y < yBorderAlly;
    }



    public void SetColor(LineRenderer lineRenderer , Color color)
    {
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }
}
