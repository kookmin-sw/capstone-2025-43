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
        if (draggableItem.draggable == false) return;

        Transform previousParent = draggableItem.parentAfterDrag; 

        if (transform.childCount == 0) //Slot Empty Drop 
        {
            draggableItem.transform.SetParent(transform);
            draggableItem.transform.localPosition = Vector3.zero;
            draggableItem.parentAfterDrag = transform;
        }
        else //Slot Full Drop -> Change
        {
            GameObject currentItem = transform.GetChild(0).gameObject;
            draggableTactic currentDraggable = currentItem.GetComponent<draggableTactic>();
            if ((currentDraggable.draggable == false))
            {
                return; 
            }

            currentDraggable.transform.SetParent(previousParent);
            currentDraggable.transform.localPosition = Vector3.zero;
            currentDraggable.parentAfterDrag = previousParent;

            draggableItem.transform.SetParent(transform);
            draggableItem.transform.localPosition = Vector3.zero;
            draggableItem.parentAfterDrag = transform;

            ChangePriority(draggableItem, currentDraggable);
        }
    }

    private void ChangePriority(draggableTactic draggableItem, draggableTactic currentDraggable)
    {
        //Change Priority of Tactic
        if (draggableItem.tactic != null && currentDraggable.tactic != null)
        {
            int tempPriority = draggableItem.tactic.priority;
            draggableItem.tactic.priority = currentDraggable.tactic.priority;
            currentDraggable.tactic.priority = tempPriority;
        }
    }
}
