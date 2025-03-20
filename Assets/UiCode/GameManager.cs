using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UiManager uiManager;
    public NodePosition nodePosition;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        nodePosition.CreateRandomSpot();
    }

}
