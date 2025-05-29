using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class GameManager
{
    public int time; // 0 : morning, 1 : afternoon, 2 : night
    public int gold = 500;
    public int maxHero = 4;
    public Map map;
    public Day day = new Day();
    public int xBorderAlly = 0;
    public int yBorderAlly = 5;
    
    public float GameTime;
    public bool isPause = false;
    public bool isNew;

    public void Init()
    {
        map = GameObject.Find("Map").GetComponent<Map>();
    }
    public void StartGame()
    {
        Managers.Instance.LoadScene("MapScene");
    }
    public bool canChange(string name)
    {
        DropTextHandler handler = null;
        switch (name)
        {
            case "Local":
                handler = Managers.Ui.localUi.GetComponent<DropTextHandler>();
                break;
            case "Shop":
                handler = Managers.Ui.shopUi.GetComponent<DropTextHandler>();
                break;
        }
        return handler.Able();
    }
    public void NewGame()
    {
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
        if (BattleManager.Instance)
            BattleManager.Instance.EnablePlayerHeroAgent(false);
        if (success)
        {
            //day -> night
            Managers.Data.localInfos[Managers.Data.handOverData.openLocal].side = "Ally";
            day.setDay(0, 9);
            day.passDay();
        }
        else
        {
            //day -> afternoon
            day.setDay(1, 1);
            day.passDay();
        }
        Managers.Instance.LoadScene("MapScene");
        GameState();
    }

    public void loadGame()
    {
        //update gold
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

    /// <summary>
    /// game state
    /// </summary>
    /// <returns>
    /// 0 : over 1: win  2: continue
    /// </returns>
    public void GameState()
    {
        int enemy = 0 , ally = 0;
        foreach (var local in Managers.Data.localInfos.Values)
        {
            switch (local.side)
            {
                case "Enemy":
                    enemy++;
                    break;
                case "Ally":
                    ally++; 
                    break;
            }
        }
        if (ally == 0)
            GameOver();
        if (ally == enemy)
        {
            // win
        }
        return ;
    }

    public void GameOver()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
