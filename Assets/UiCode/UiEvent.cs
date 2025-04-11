using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiEvent : MonoBehaviour, IPointerClickHandler
{
    public string targetName;
    public bool active = false;

    
    private void OnMouseDown()
    {
        // 다른 UI가 열려 있으면 클릭 무시
        if (!UiManager.instance.IsOnlyDefaultOpen())
            return;
        onClick();
    }
    
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData == null)
            return;
        eventData.Use();
        onClick();
    }

    public void onClick()
    {
        Managers.instance.uiManager.SetUiCondition(targetName, active);
    }
}
