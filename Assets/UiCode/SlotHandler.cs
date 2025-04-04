using System.Collections.Generic;
using UnityEngine;

public class SlotHandler : MonoBehaviour
{
    private List<ListIdx> storedItems = new List<ListIdx>(); // 여러 개의 ListIdx 저장

    public void StoreItem(ListIdx item)
    {
        if (!storedItems.Contains(item))
        {
            storedItems.Add(item); // 리스트에 추가
        }
    }

    public void PurchaseItem()
    {
        if (storedItems.Count > 0)
        {
            foreach (ListIdx item in storedItems)
            {
                item.data.own = true; // 데이터 변경
                Destroy(item.gameObject); // 구매한 아이템 삭제
            }

            storedItems.Clear(); // 슬롯 비우기
            Managers.instance.gameManager.UpdateHeroLists(); // 리스트 갱신
        }
    }
}
