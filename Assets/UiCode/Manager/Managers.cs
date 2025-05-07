using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class Managers : MonoBehaviour
{

    static Managers _instance;
    public static Managers Instance { get { Init(); return _instance; } }

    public static Managers GetInstance()
    {
        return _instance;
    }

    UiManager _ui = new UiManager();
    GameManager _game = new GameManager();
    ResourceManager _resource = new ResourceManager();
    DataManager _data = new DataManager();
    PoolManager _pool = new PoolManager();

    public static UiManager Ui { get { return Instance._ui; } }
    public static GameManager Game { get { return Instance._game; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static DataManager Data { get { return Instance._data; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    
    private void Start()
    {
        Init();
    }
    static void Init()
    {
        if (_instance == null)
        {
            GameObject mO = GameObject.Find("@Managers");
            if(mO == null)
            {
                mO = new GameObject { name = "@Managers" };
                mO.AddComponent<Managers>();
            }
            DontDestroyOnLoad(mO);
            _instance = mO.GetComponent<Managers>();
            Ui.Init();
            Data.Init();
            Game.Init();
            Pool.Init();
        }
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
        Invoke("ReLoad", 3f);
        return;
    }
    void ReLoad()
    {
        Debug.Log("Reload");
        Ui.Init();
       // Pool.Init();
        Game.ReloadGame();
    }
    
}
