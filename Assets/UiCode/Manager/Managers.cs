using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class Managers : MonoBehaviour
{

    public static Managers instance;
    public UiManager uiManager;
    public GameManager gameManager;
    public ResourceManager resourceManager;
    public DataManager dataManager;
    public PoolManager poolManager;
    
    
    private void Start()
    {
        if (GameObject.Find("%Managers") != null)
        {
            Destroy(this.gameObject);
            return;
        }
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            this.name = '%' + this.name;
            instance = this;
        }
        Debug.Log("Start");
        //todo login
        gameManager.GameStart();
        uiManager.Init();
        poolManager.SetHeroList();
    }

    public void StartBattle()
    {
        gameManager.map.gameObject.SetActive(false);
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
            dataManager.handOverData.localInfos[dataManager.handOverData.openLocal].side = "Ally";
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
        uiManager.Init();
        poolManager.SetHeroList();
        gameManager.ReloadGame();

        // 씬 오브젝트 접근 가능
    }
}
