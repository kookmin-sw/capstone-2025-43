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
        yield return new WaitForSeconds(1f); // �� ������ ���
        Debug.Log("�Ƹ��� �� �ε� �Ϸ� ���Դϴ�!");
        uiManager.Init();
        poolManager.SetHeroList();
        gameManager.ReloadGame();

        // �� ������Ʈ ���� ����
    }
}
