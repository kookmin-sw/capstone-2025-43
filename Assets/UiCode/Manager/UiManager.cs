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

    private Stack<string> openUi = new Stack<string>();

    public void Init()
    {
        openUi.Clear();
        openUi.Push("Default");
        defaultUi.SetActive(true);
        localUi.SetActive(false);
        settingUi.SetActive(false);
        shopUi.SetActive(false);
    }

    public void SetUiCondition(string name , bool condition)
    {
        Debug.Log($"{name}UI {condition}");
        if (condition) // UI 열기
        {
            if (openUi.Count > 0)
                SetUiActive(openUi.Peek(), false); // 현재 열려 있는 UI 비활성화

            openUi.Push(name);
            SetUiActive(name, true);
        }
        else // UI 닫기
        {
            if (openUi.Count > 0 && openUi.Peek() == name)
            {
                SetUiActive(name, false);
                openUi.Pop();

                if (openUi.Count > 0)
                    SetUiActive(openUi.Peek(), true); // 이전 UI 다시 활성화
            }
        }
        /*
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
                if (!condition && openUi.Peek() == "Setting")
                    settingUi.SetActive(condition);
                break;
            case "Shop":
                if (condition && openUi.Peek() == "Default")
                {
                    shopUi.SetActive(condition);
                    shopUi.GetComponent<UnitList>().SetList();
                }
                if (!condition && openUi.Peek() == "Shop")
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
        */
    }
    private void SetUiActive(string name, bool active)
    {
        switch (name)
        {
            case "Default":
                defaultUi.SetActive(active);
                break;
            case "Local":
                localUi.SetActive(active);
                if (active) localUi.GetComponent<UnitList>().SetList();
                break;
            case "Setting":
                settingUi.SetActive(active);
                break;
            case "Shop":
                shopUi.SetActive(active);
                if (active) shopUi.GetComponent<UnitList>().SetList();
                break;
        }
    }
    public void CloseAllUi()
    {
        while (openUi.Count > 0)
        {
            SetUiActive(openUi.Pop(), false);
        }

        // 기본 UI만 다시 활성화
        defaultUi.SetActive(true);
        openUi.Push("Default");
    }

    public string CurrentUi()
    {
        return openUi.Count > 0 ? openUi.Peek() : "";
    }
}
