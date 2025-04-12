using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiEvent : MonoBehaviour, IPointerClickHandler
{
    public string targetName;
    public bool active = false;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!UiManager.instance.IsOnlyDefaultOpen())
            return;
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
