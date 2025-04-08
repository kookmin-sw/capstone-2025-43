using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public GameObject defaultUi;
    public GameObject localUi;
    public GameObject settingUi;
    public GameObject shopUi;

    public GameObject localList;

    private Stack<string> openUi = new Stack<string>(); // add initialization

    public void Init()
    {
        openUi.Push("Default");
    }

    public void SetUiCondition(string name , bool condition)
    {
        Debug.Log($"{name}UI {condition}");
        switch (name)
        {
            case "Default":
                defaultUi.SetActive(condition);
                break;
            case "Local":
                if (condition && openUi.Peek() == "Default")
                {
                    localUi.SetActive(condition);
                    localUi.GetComponent<UnitList>().SetList();
                }
                if(!condition && openUi.Peek() == "Local")
                    localUi.SetActive(condition);
                break;
            case "Setting":
                if (condition && openUi.Peek() == "Default")
                    settingUi.SetActive(condition);
                break;
            case "Shop":
                if (condition && openUi.Peek() == "Default")
                {
                    shopUi.SetActive(condition);
                    shopUi.GetComponent<UnitList>().SetList();
                }
                if (!condition && openUi.Peek() == "Shop")
                    localUi.SetActive(condition);
                break;
            case "Option":
                break;
            case "Save":
                break;
            case "Load":
                break;
            case "Exit":
                break;
            case "Quit":
                break;
        }
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
                Debug.Log("Stack Empty");
            }
        }
    }
}
