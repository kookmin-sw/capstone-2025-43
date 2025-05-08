using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager
{
    public int time; // 0 : morning, 1 : afternoon, 2 : night
    public Map map;   

    public int xBorderAlly = 0;
    public int yBorderAlly = 5;
    
    public float GameTime;
    public bool isPause = false;
    public bool loadingScene = true;

    public void Init()
    {
        map = GameObject.Find("Map").GetComponent<Map>();
        map.Init();
    }

    public void StartBattle()
    {
        //todo start battle scene
        SceneManager.LoadScene("BattleScene");
    }

    // From BattleScene
    public void EndBattle(bool success)
    {
        Managers.Instance.LoadScene("MapScene");
        if (success)
        {
            //day -> night
            time = 2;
            Managers.Data.handOverData.localInfos[Managers.Data.handOverData.openLocal].side = "Ally";
        }
        else
        {
            time = 1;
            //day -> afternoon
        }
        //Load Game
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

    public void Heal()
    {
        //heal : poolmanager -> own hero
    }
    public void TakenAlly()
    {
        List<Line> attack = map.GetLines();
        int t = Random.Range(0, attack.Count);
        Line cur = attack[t];
        LocalInfo a = Managers.Data.handOverData.localInfos[cur.p0];
        LocalInfo b = Managers.Data.handOverData.localInfos[cur.p1];
        if(a.side == "Ally")
            a.side = "Enemy";
        else
            b.side = "Enemy";
    }

    public void SetSpecial()
    {

    }


}
