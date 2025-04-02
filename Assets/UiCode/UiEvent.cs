using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiEvent : MonoBehaviour, IPointerClickHandler
{
    public string targetName;
    public bool active = false;
    public GameObject targetSlot;

    private void OnMouseDown() // object ��������
    {
        onClick();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData == null)
            return;
        onClick();
    }

    public void onClick() // ��ư ui ��������
    {
        if (targetName == "PurchaseButton") // 🎯 구매 버튼이면 실행
        {
            if (targetSlot != null && targetSlot.GetComponent<SlotHandler>() != null)
            {
                targetSlot.GetComponent<SlotHandler>().PurchaseItem();
            }
            return;
        }
        Debug.Log("�������ϴ�");
        Managers.instance.uiManager.SetUiCondition(targetName, active);
    }
}
