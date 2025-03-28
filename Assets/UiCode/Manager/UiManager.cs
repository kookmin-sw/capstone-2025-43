using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public GameObject defaultUi;
    public GameObject localUi;
    public GameObject settingUi;
    public GameObject shopUi;

    public GameObject localList;

    private Stack<string> openUi = new Stack<string>(); // add initialization

    public void SetUiCondition(string name , bool condition)
    {
        switch (name)
        {
            case "Default":
                defaultUi.SetActive(condition);
                break;
            case "Local":
                localUi.SetActive(condition);
                break;
            case "Setting":
                settingUi.SetActive(condition);
                break;
            case "Shop":
                shopUi.SetActive(condition);
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
                SetUiCondition("Setting", true);
            }
        }
    }

    public void SetLocalHeroListUi()
    {
        GameObject go = Instantiate(Managers.instance.resourceManager.Instantiate("ListIdx", localList.transform));
        go.GetComponent<ListIdx>().data = (UnitData)Managers.instance.dataManager.GetSOData("Unit/Hero", "adf");
    }
}
