using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Map map;
    public Day day;

    public int xBorderAlly = 0;
    public int yBorderAlly = 5;

    public GameObject ownHeroList;
    public GameObject NotOwnHeroList;


    public float GameTime;
    public bool isPause = false;
    public void GameStart()
    {
        map.CreateMap();
        ownHeroList.GetComponent<List>().SetList(true);
        NotOwnHeroList.GetComponent<List>().SetList(false);
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
