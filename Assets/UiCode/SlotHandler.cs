using UnityEngine;

public class SlotHandler : MonoBehaviour
{
    private ListIdx storedItem; // 슬롯에 저장된 ListIdx

    public void StoreItem(ListIdx item)
    {
        storedItem = item;
    }

    public void PurchaseItem()
    {
        if (storedItem != null)
        {
            storedItem.data.own = true; // 데이터 변경
            Managers.instance.gameManager.UpdateHeroLists(); // 리스트 갱신
            Destroy(storedItem.gameObject); // 구매한 아이템 삭제
            storedItem = null; // 슬롯 비우기
        }
    }
}
