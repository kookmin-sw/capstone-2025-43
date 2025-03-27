using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Map map;
    public Day day;

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
}
