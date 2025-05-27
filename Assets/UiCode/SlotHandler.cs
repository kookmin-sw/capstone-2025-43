using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class SlotHandler : MonoBehaviour
{
    public Transform dropContent; // 드롭된 유닛들이 들어있는 ScrollView의 Content

    public void Buy()
    {
        if (!Managers.Game.canChange("Shop"))
            return;
        foreach (Transform child in dropContent)
        {
            ListIdx idx = child.GetComponent<ListIdx>();
            CharacterStat stat = idx.unitData.GetComponent<CharacterStat>();
            stat.own = true;
        }

    }
    public int cartPrice()
    {
        int price = 0;
        foreach (Transform child in dropContent)
        {
            ListIdx idx  = child.GetComponent<ListIdx>();
            CharacterStat stat = idx.unitData.GetComponent<CharacterStat>();
            price += stat.price;
        }
        return price;
    }

    public int selectHero()
    {
        int cnt = 0;
        foreach(Transform child in dropContent)
        {
            if(child.childCount > 0)
            {
                cnt++;
            }
        }
        return cnt;
    }

    public void StartBattle()
    {
        if (!Managers.Game.canChange("Local"))
            return;
        for (int idx = 0; idx < dropContent.childCount; idx++)
        {
            Transform child = dropContent.GetChild(idx);
            if(child.childCount > 0)
            {
                TMP_Text name = child.GetChild(0).GetComponent<ListIdx>().unitName;
                if (name != null)
                {
                    Managers.Data.handOverData.unitPositions[idx] = name.text;
                }
            }
            else
                Managers.Data.handOverData.unitPositions[idx] = null;
        }
    }

    public void ClearPositionGrid()
    {
        for (int idx = 0; idx < dropContent.childCount; idx++)
        {
            Transform child = dropContent.GetChild(idx);
            if (child.childCount > 0)
            {
                child.GetChild(0).GetComponent<Drag>().returnToFrom();
            }
        }
    }
    public void ClearCart()
    {
        foreach (Transform child in dropContent)
            Destroy(child.gameObject);
    }

}
