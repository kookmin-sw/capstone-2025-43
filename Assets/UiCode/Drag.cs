using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour , IDragHandler , IBeginDragHandler, IEndDragHandler
{
    private RectTransform tmpRectTrans;
    private RectTransform parentRectTrans;
    private Canvas rootCanvas;


    private void Awake()
    {
        tmpRectTrans = GetComponent<RectTransform>();
        parentRectTrans = this.tmpRectTrans.parent as RectTransform;
        rootCanvas = transform.GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(rootCanvas.transform);
        transform.GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.GetComponent<Image>().raycastTarget = true;
        if (this.transform.parent == rootCanvas.transform)
            returnToFrom();
    }

    public void returnToFrom()
    {
        transform.SetParent(parentRectTrans);
    }
}
