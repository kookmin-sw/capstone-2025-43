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
}
