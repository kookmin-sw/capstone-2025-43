using UnityEngine;
using UnityEngine.EventSystems;

public class TacticSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped == null) return;

        draggableTactic draggableItem = dropped.GetComponent<draggableTactic>();
        if (draggableItem == null) return;

        Transform previousParent = draggableItem.parentAfterDrag; 

        if (transform.childCount == 0)
        {
            draggableItem.transform.SetParent(transform);
            draggableItem.transform.localPosition = Vector3.zero;
            draggableItem.parentAfterDrag = transform;
        }
        else
        {
            GameObject currentItem = transform.GetChild(0).gameObject;
            draggableTactic currentDraggable = currentItem.GetComponent<draggableTactic>();

            currentDraggable.transform.SetParent(previousParent);
            currentDraggable.transform.localPosition = Vector3.zero;
            currentDraggable.parentAfterDrag = previousParent;

            draggableItem.transform.SetParent(transform);
            draggableItem.transform.localPosition = Vector3.zero;
            draggableItem.parentAfterDrag = transform;

            if (draggableItem.tactic != null && currentDraggable.tactic != null)
            {
                int tempPriority = draggableItem.tactic.Priority;
                draggableItem.tactic.Priority = currentDraggable.tactic.Priority;
                currentDraggable.tactic.Priority = tempPriority;
            }
        }
    }

}
