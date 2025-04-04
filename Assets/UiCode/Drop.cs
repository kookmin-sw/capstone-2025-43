using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (this.transform.childCount == 0)
        {
            eventData.pointerDrag.transform.SetParent(this.transform);
            eventData.pointerDrag.transform.position = this.transform.position; // 위치 잡기
            /*
            GameObject droppedObject = eventData.pointerDrag;
            droppedObject.transform.SetParent(this.transform);

            ListIdx item = droppedObject.GetComponent<ListIdx>();
            if (item != null)
            {
                slotHandler.StoreItem(item); // SlotHandler에 저장
            }
            return;
            */
        }
    }
}
