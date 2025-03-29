using UnityEngine;

public class Managers : MonoBehaviour
{

    public static Managers instance;
    public UiManager uiManager;
    public GameManager gameManager;
    public ResourceManager resourceManager;
    public DataManager dataManager;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        //todo login
        gameManager.GameStart();
    }
}
