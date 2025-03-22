using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public GameObject mapUi;
    public GameObject NodeUi;
    public void SetUiCondition(string name , bool condition)
    {
        switch (name)
        {
            case "Map":
                mapUi.SetActive(condition);
                break;
            case "Node":
                NodeUi.SetActive(condition);
                break;
        }

    }
    
}
