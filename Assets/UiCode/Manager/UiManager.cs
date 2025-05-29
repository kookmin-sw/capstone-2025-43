using UnityEngine;
using System.Collections.Generic;
public class UiManager
{
    public GameObject defaultUi;
    public GameObject localUi;
    public GameObject settingUi;
    public GameObject shopUi;
    public GameObject statusUi;
    public GameObject ruleUi;

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
        statusUi = canvas.transform.GetChild(4).gameObject;
        ruleUi = canvas.transform.GetChild(5).gameObject;

        SetUiCondition("Default", true);
        localUi.SetActive(false);
        settingUi.SetActive(false);
        shopUi.SetActive(false);
        statusUi.SetActive(false);
        ruleUi.SetActive(false);

        shopUi.GetComponent<DropTextHandler>().Init(Managers.Game.gold, 0);
        localUi.GetComponent<DropTextHandler>().Init(Managers.Game.maxHero, 1);
    }

    public bool IsOnlyDefaultOpen()
    {   
        return openUi.Count == 1 && openUi.Peek() == "Default";
    }

    public void SetUiCondition(string name , bool condition)
    {
        Debug.Log($"{name}UI {condition}");
        if (condition) // UI ?ó¥Í∏?
        {
            if (openUi.Count > 0)
            {
                string top = openUi.Peek();
                if (top != "Default")
                {
                    SetUiActive(top, false); // Default ?†ú?ô∏?ïòÍ≥†Îßå ÎπÑÌôú?Ñ±?ôî
                }
                else
                {
                    SetDefaultUiRaycast(false); // Default?äî ÎπÑÌôú?Ñ±?ôî ????ã† RaycastÎß? ÎßâÏùå
                }
            }

            openUi.Push(name);
            SetUiActive(name, true);
        }
        else // UI ?ã´Í∏?
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
                    SetDefaultUiRaycast(true); // DefaultÍ∞? ?ã§?ãú ÏµúÏÉÅ?ã®?ù¥Î©? Raycast ?ôú?Ñ±?ôî
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
                    localUi.GetComponent<DropTextHandler>().UpdateCur(0);
                    localUi.GetComponent<UnitList>().SetList();
                    localUi.GetComponent<LocalInfoUi>().SetLocalUi();
                }
                break;
            case "Setting":
                settingUi.SetActive(active);
                break;
            case "Shop":
                shopUi.SetActive(active);
                if (active)
                {
                    shopUi.GetComponent<DropTextHandler>().UpdateCur(0);
                    shopUi.GetComponent<UnitList>().SetList();
                }
                break;
            case "Status":
                statusUi.SetActive(active);
                break;
            case "Rule":
                ruleUi.SetActive(active);
                break;
            case "Option":
                break;
        }
    }
    private void SetDefaultUiRaycast(bool value)
    {
        var cg = defaultUi.GetComponent<CanvasGroup>();
        if (cg != null)
        {
            cg.blocksRaycasts = value;
            Debug.Log($"Default UI Raycast ?Üí {value}");
        }
    }

    public void CloseAllUi()
    {
        while (openUi.Count > 0)
        {
            SetUiActive(openUi.Pop(), false);
        }

        // Í∏∞Î≥∏ UIÎß? ?ã§?ãú ?ôú?Ñ±?ôî
        defaultUi.SetActive(true);
        openUi.Push("Default");
    }

    public string CurrentUi()
    {
        return openUi.Count > 0 ? openUi.Peek() : "";
    }

    public void updateText(string name)
    {
        DropTextHandler textHandler = null;
        int num = 0;
        switch (name)
        {
            case "Local":
                textHandler = localUi.GetComponent<DropTextHandler>();
                num = localUi.GetComponent<SlotHandler>().selectHero();
                break;
            case "Shop":
                textHandler = shopUi.GetComponent<DropTextHandler>();
                num = shopUi.GetComponent<SlotHandler>().cartPrice();
                textHandler.UpdateMax(Managers.Game.gold);
                break;
        }
        textHandler.UpdateCur(num);
    }
}
