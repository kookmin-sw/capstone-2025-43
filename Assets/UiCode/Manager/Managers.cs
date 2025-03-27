using UnityEngine;

public class Managers : MonoBehaviour
{

    public static Managers instance;
    public UiManager uiManager;
    public GameManager gameManager;
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
