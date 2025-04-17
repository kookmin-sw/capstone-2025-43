using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Map map;
    public Day day;
    public LocalData data;
    

    public int xBorderAlly = 0;
    public int yBorderAlly = 5;

    public float GameTime;
    public bool isPause = false;


    public void GameStart()
    {
        map.Init();
        map.CreateMap();
    }

    public void ReloadGame()
    {
        map = GameObject.Find("Map").GetComponent<Map>();
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
}
