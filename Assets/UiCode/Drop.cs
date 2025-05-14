using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    [SerializeField]
    int maxChild = 1;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("drop Сп");
        if (eventData.pointerDrag.GetComponent<Drag>() == null)
        {
            eventData.pointerDrag.GetComponent<Drag>().returnToFrom();
            return;
        }

        Debug.Log($"{this.transform.childCount} , {maxChild}");
        if (this.transform.childCount < maxChild)
        {
            eventData.pointerDrag.transform.SetParent(this.transform);
            eventData.pointerDrag.transform.position = this.transform.position;
            CharacterStat cur = eventData.pointerDrag.GetComponent<ListIdx>().unitData;
            switch (cur.own)
            {
                case true:
                    Managers.Ui.updateText("Local", 1);
                    break;
                case false:
                    Managers.Ui.updateText("Shop", cur.price);
                    break;
            }
        }
    }
}
