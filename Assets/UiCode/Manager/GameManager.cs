using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Map map;
    public Day day;

    public int xBorderAlly = 0;
    public int yBorderAlly = 5;

    public GameObject ownHeroList;
    public GameObject notOwnHeroList;


    public float GameTime;
    public bool isPause = false;
    public void GameStart()
    {
        map.CreateMap();
        ownHeroList.GetComponent<List>().SetList(true);
        notOwnHeroList.GetComponent<List>().SetList(false);
    }

    public void UpdateHeroLists()
    {
        ownHeroList.GetComponent<List>().SetList(true);  // 소유한 리스트 갱신
        notOwnHeroList.GetComponent<List>().SetList(false); // 미소유 리스트 갱신
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
