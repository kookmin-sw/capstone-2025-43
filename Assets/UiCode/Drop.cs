using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if(this.transform.childCount == 0)
        {
            eventData.pointerDrag.transform.SetParent(this.transform);
            return;
        }
        // �̹� object �� ������ �Ұ�
        Drag drag = eventData.pointerDrag.gameObject.GetComponent<Drag>();
        drag.returnToFrom();
        
    }
}
