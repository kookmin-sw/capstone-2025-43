using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public GameObject mapUi;
    public GameObject nodeUi;
    public GameObject settingUi;
    public GameObject shopUi;

    private Stack<string> openUi = new Stack<string>(); // add initialization

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
                    SetUiCondition("Setting", true);
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
    
    public void OpenShop()
    {
        SetUiCondition("Shop", true);
    }

    public void CloseShop()
    {
        Debug.Log("CloseShop() 호출됨!");
        SetUiCondition("Shop", false);
    }
}
