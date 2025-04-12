using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Map map;
    public Day day;
    public List<Line> Roads = new List<Line>();
    public LocalData data;
    public List<GameObject> nodes = new List<GameObject>();
    public GameObject openLocal;

    public int xBorderAlly = 0;
    public int yBorderAlly = 5;

    public float GameTime;
    public bool isPause = false;
    public void GameStart()
    {
        map.CreateMap();
        Roads = map.GetLines();
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

    public void AllyToEnemy()
    {
        int a = Random.Range(0, Roads.Count);
        if (Roads[a].node0.tag == "Ally")
            Roads[a].node0.tag = "Enemy";
        else
            Roads[a].node1.tag = "Enmey";
    }

    
    public void StartBattle(GameObject obj)
    {
        //todo start battle scene
    }

    // From BattleScene
    public void EndBattle(GameObject obj , bool success)
    {
        if (success)
        {
            //day -> night
            obj.tag = "Ally";
        }
        else
        {
            //day -> afternoon
        }
        AllyToEnemy();
    }
}
