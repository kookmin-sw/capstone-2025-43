using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlotHandler : MonoBehaviour
{

    public Transform dropContent; // 드롭된 유닛들이 들어있는 ScrollView의 Content
    public Transform Gold;
    public void PurchaseItem()
    {
        List<ListIdx> itemsToBuy = new List<ListIdx>();

        foreach (Transform child in dropContent)
        {
            ListIdx unit = child.GetComponent<ListIdx>();
            if (unit != null && !unit.unitData.own)
            {
                itemsToBuy.Add(unit);
            }
        }

        foreach (ListIdx item in itemsToBuy)
        {
            item.unitData.own = true;
            Destroy(item.gameObject);
        }

        Managers.Pool.SetHeroList(); // 리스트 갱신
    }

    public void StartBattleButton()
    {
        for(int idx = 0; idx < dropContent.childCount; idx++)
        {
            Transform child = dropContent.GetChild(idx);
            if(child.childCount > 0)
            {
                TMP_Text name = child.GetChild(0).GetComponent<ListIdx>().unitName;
                if (name != null)
                    Managers.Data.handOverData.unitPositions[idx] = name.text;
            }
            else
                Managers.Data.handOverData.unitPositions[idx] = null;
        }
        Managers.Game.StartBattle();
    }

    public void CloseLocalButton()
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
}
