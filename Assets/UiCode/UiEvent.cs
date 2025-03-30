using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiEvent : MonoBehaviour, IPointerClickHandler
{
    public string targetName;
    public bool active = false;

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
        Debug.Log("�������ϴ�");
        Managers.instance.uiManager.SetUiCondition(targetName, active);
    }
}
