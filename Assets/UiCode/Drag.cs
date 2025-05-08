using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour , IDragHandler , IBeginDragHandler, IEndDragHandler
{
    // ���� ��ġ , �θ� ���� , �˹���
    private RectTransform tmpRectTrans;
    private RectTransform parentRectTrans;
    private Canvas rootCanvas;

    public static bool isDragging = false;


    private void Start()
    {
        tmpRectTrans = GetComponent<RectTransform>();
        parentRectTrans = this.tmpRectTrans.parent as RectTransform;
        rootCanvas = transform.GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // ui �浹 ������ �˹����� �ڽ����� ����
        isDragging = true;
        transform.SetParent(rootCanvas.transform);
        transform.GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        transform.GetComponent<Image>().raycastTarget = true;
        if (this.transform.parent == rootCanvas.transform) // drop�� �ȵǾ��� ���
            returnToFrom();
    }

    public void returnToFrom()
    {
        transform.SetParent(parentRectTrans);
    }
}
