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
        if (targetName == "PurchaseButton") // 구매 버튼이면 실행
        {
            SlotHandler[] slots = FindObjectsByType<SlotHandler>(FindObjectsInactive.Include, FindObjectsSortMode.None); // 모든 슬롯 찾기

            foreach (SlotHandler slot in slots)
            {
                slot.PurchaseItem(); // 슬롯 내 모든 아이템 구매 처리
            }
            return;
        }

        Managers.instance.uiManager.SetUiCondition(targetName, active);
    }
}
