using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiEvent : MonoBehaviour, IPointerClickHandler
{
    public string targetName;
    public bool active = false;

    private void OnMouseDown() // object ´­·¶À»¶§
    {
        onClick();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData == null)
            return;
        onClick();
    }

    public void onClick() // ¹öÆ° ui ´­·¶À»¶§
    {
        Debug.Log("´­·¶½À´Ï´Ù");
        Managers.instance.uiManager.SetUiCondition(targetName, active);
    }
}
