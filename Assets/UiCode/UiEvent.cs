using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiEvent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    string targetName;
    [SerializeField]
    bool active = false;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("´­¸²");    
        if (eventData == null)
            return;

        if (active && !Managers.Ui.IsOnlyDefaultOpen())
            return;
        
        eventData.Use();
        onClick();
    }

    public void onClick()
    {
        Managers.Ui.SetUiCondition(targetName, active);
    }
}
