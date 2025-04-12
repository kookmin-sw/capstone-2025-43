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
        List<Line> roads = map.GetLines();
        foreach (Line line in roads)
        {
            if(line.node0 != line.node1)
                roads.Add(line);
        }
        int a = Random.Range(0, roads.Count);
        if (roads[a].node0.tag == "Ally")
        {
            Managers.instance.dataManager.handOverData.allyNodes.Remove(roads[a].node0.GetComponent<Node>().pin);
            Managers.instance.dataManager.handOverData.enemyNodes.Add(roads[a].node0.GetComponent<Node>().pin);
        }
        else
        {
            Managers.instance.dataManager.handOverData.enemyNodes.Remove(roads[a].node0.GetComponent<Node>().pin);
            Managers.instance.dataManager.handOverData.allyNodes.Add(roads[a].node0.GetComponent<Node>().pin);
        }
    }

    
    public void StartBattle()
    {
        map.gameObject.SetActive(false);
        //todo start battle scene
        SceneManager.LoadScene("BattleScene");
    }

    // From BattleScene
    public void EndBattle(bool success)
    {
        map.CreateMap();
        map.gameObject.SetActive(true);
        if (success)
        {
            //day -> night
            Managers.instance.dataManager.handOverData.allyNodes.Add(Managers.instance.dataManager.handOverData.openLocal);
        }
        else
        {
            //day -> afternoon
        }
        //AllyToEnemy();
    }

}
