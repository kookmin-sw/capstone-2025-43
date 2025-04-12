using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
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

    public void AllyToEnemy()
    {
        List<Line> roads = Managers.instance.dataManager.handOverData.Roads;
        int a = Random.Range(0, roads.Count);
        if (roads[a].node0.tag == "Ally")
            roads[a].node0.tag = "Enemy";
        else
            roads[a].node1.tag = "Enmey";
    }

    
    public void StartBattle(GameObject obj)
    {
        //todo start battle scene
        SceneManager.LoadScene("BattleScene");
    }

    // Called in BattleScene
    public void EndBattle(bool success)
    {
        if (success)
        {
            //day -> night
        }
        else
        {
            //day -> afternoon
        }
        AllyToEnemy();
    }

}
