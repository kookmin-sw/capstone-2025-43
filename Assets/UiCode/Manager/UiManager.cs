using UnityEngine;
using System.Collections;
public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public GameObject mapUi;
    public GameObject nodeUi;
    public GameObject settingUi;
    public GameObject shopUi;

    private Stack openUi;

    public void SetUiCondition(string name , bool condition)
    {
        GameObject gameObject = GetUi(name);
        if (gameObject != null) {
            gameObject.SetActive(condition);
            if (condition)
                openUi.Push(name);
            else
            {
                try
                {
                    openUi.Pop();
                }
                catch
                {
                    SetUiCondition("Settting", true);
                }
            }
        }

    }
    public GameObject GetUi( string name)
    {
        switch (name)
        {
            case "Map":
                return mapUi;
            case "Node":
                return nodeUi;
            case "Setting":
                return settingUi;
            case "Shop":
                return shopUi;
            case "Option":
                return null;
            case "Save":
                return null;
            case "Load":
                return null;
            case "Exit":
                return null;
            case "Quit":
                return null;
        }
        return null;
    }
    
}
