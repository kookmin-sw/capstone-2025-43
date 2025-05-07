using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiEvent : MonoBehaviour, IPointerClickHandler
{
    public string targetName;
    public bool active = false;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData == null)
            return;

        if (active && !Managers.Ui.IsOnlyDefaultOpen())
            return;
            
        eventData.Use();
        onClick();
    }

    public virtual void onClick()
    {
        Managers.Ui.SetUiCondition(targetName, active);
    }
}
