using System.Collections.Generic;
using UnityEngine;

public class SlotHandler : MonoBehaviour
{
    public Transform dropContent; // 드롭된 유닛들이 들어있는 ScrollView의 Content

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

        Managers.instance.poolManager.SetHeroList(); // 리스트 갱신
    }

    public void StartBattleButton()
    {
        for(int idx = 0; idx < dropContent.childCount; idx++)
        {
            Transform child = dropContent.GetChild(idx);
            if(child.childCount > 0)
            {
                UnitData unitdata = child.GetChild(0).GetComponent<ListIdx>().unitData;
                if (unitdata != null)
                    Managers.instance.dataManager.handOverData.unitPositions[idx] = unitdata.unitName;
                else
                    Managers.instance.dataManager.handOverData.unitPositions[idx] = null;
            }
        }
        Managers.instance.gameManager.StartBattle(null);
    }

}
