using UnityEngine;

public class Managers : MonoBehaviour
{

    public static Managers instance;
    public UiManager uiManager;
    public GameManager gameManager;
    public ResourceManager resourceManager;
    public DataManager dataManager;
    public PoolManager poolManager;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        //todo login
        gameManager.GameStart();
        poolManager.SetHeroList();
        uiManager.Init();
    }
}
