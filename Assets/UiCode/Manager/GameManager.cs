using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int time; // 0 : morning, 1 : afternoon, 2 : night
    public Map map;
    public LocalData data;
    

    public int xBorderAlly = 0;
    public int yBorderAlly = 5;
    
    public float GameTime;
    public bool isPause = false;


    public void GameStart()
    {
        map.Init();
        map.CreateMap();
        Managers.Ui.Init();
        Managers.Pool.SetHeroList();
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
        SceneManager.LoadScene("MapScene");
        if (success)
        {
            //day -> night
            Managers.Data.handOverData.localInfos[Managers.Data.handOverData.openLocal].side = "Ally";
        }
        else
        {
            //day -> afternoon
        }
        StartCoroutine("WaitForSceneLoad");
        //AllyToEnemy();
    }
    IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(1f); // 한 프레임 대기
        Debug.Log("아마도 씬 로드 완료 후입니다!");
        Managers.Ui.Init();
        Managers.Pool.SetHeroList();
        ReloadGame();
        // 씬 오브젝트 접근 가능
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
        // todo heal heros
    }
    public void TakenAlly()
    {
        //handover data 수정
    }

}
