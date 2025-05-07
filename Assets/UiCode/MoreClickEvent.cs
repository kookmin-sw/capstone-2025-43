using UnityEngine;
using UnityEngine.EventSystems;

public class MoreClickEvent : MonoBehaviour, IPointerClickHandler
{
    public ListIdx listIdx;

    public void OnPointerClick(PointerEventData eventData)
    {
        Managers.Ui.statusUi.GetComponent<Status>().Init(listIdx.unitData);
        Managers.Ui.SetUiCondition("Status", true);
    }
}

