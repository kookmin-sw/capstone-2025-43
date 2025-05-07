using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class UiManager
{
    public GameObject defaultUi;
    public GameObject localUi;
    public GameObject settingUi;
    public GameObject shopUi;

    public GameObject localList;

    private Stack<string> openUi = new Stack<string>();

    public void Init()
    {
        openUi.Clear();
        GameObject canvas = GameObject.Find("Canvas");
        Debug.Log($"{canvas} is open");
        defaultUi = canvas.transform.GetChild(0).gameObject;
        localUi = canvas.transform.GetChild(1).gameObject;
        settingUi= canvas.transform.GetChild(2).gameObject;
        shopUi = canvas.transform.GetChild(3).gameObject;

        openUi.Push("Default");
        defaultUi.SetActive(true);
        localUi.SetActive(false);
        settingUi.SetActive(false);
        shopUi.SetActive(false);
    }

    public bool IsOnlyDefaultOpen()
    {   
        return openUi.Count == 1 && openUi.Peek() == "Default";
    }
    public void SetUiCondition(string name , bool condition)
    {
        Debug.Log($"{name}UI {condition}");
        if (condition) // UI 열기
        {
            if (openUi.Count > 0)
        {
            string top = openUi.Peek();
            if (top != "Default")
            {
                SetUiActive(top, false); // Default 제외하고만 비활성화
            }
            else
            {
                SetDefaultUiRaycast(false); // Default는 비활성화 대신 Raycast만 막음
            }
        }

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
            {
                string previous = openUi.Peek();
                SetUiActive(previous, true);

                if (previous == "Default")
                    SetDefaultUiRaycast(true); // Default가 다시 최상단이면 Raycast 활성화
            }
        }
        }
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
                if (active)
                {
                    //localUi.GetComponent<UnitList>().SetList();
                    localUi.GetComponent<LocalInfoUi>().SetLocalUi();
                }
                break;
            case "Setting":
                settingUi.SetActive(active);
                break;
            case "Shop":
                shopUi.SetActive(active);
                // if (active) shopUi.GetComponent<UnitList>().SetList();
                break;
        }
    }
    private void SetDefaultUiRaycast(bool value)
    {
        var cg = defaultUi.GetComponent<CanvasGroup>();
        if (cg != null)
        {
            cg.blocksRaycasts = value;
            Debug.Log($"Default UI Raycast → {value}");
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
