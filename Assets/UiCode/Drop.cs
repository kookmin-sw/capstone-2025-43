using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    private SlotHandler slotHandler;
    private void Start()
    {
        slotHandler = GetComponent<SlotHandler>(); // SlotHandler 찾기
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(this.transform.childCount == 0)
        {
            //eventData.pointerDrag.transform.SetParent(this.transform);
            GameObject droppedObject = eventData.pointerDrag;
            droppedObject.transform.SetParent(this.transform);

            ListIdx item = droppedObject.GetComponent<ListIdx>();
            if (item != null)
            {
                slotHandler.StoreItem(item); // SlotHandler에 저장
            }
            return;
        }
        // �̹� object �� ������ �Ұ�
        Drag drag = eventData.pointerDrag.gameObject.GetComponent<Drag>();
        drag.returnToFrom();
    }
}
