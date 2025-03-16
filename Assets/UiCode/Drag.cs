using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour , IDragHandler , IBeginDragHandler, IEndDragHandler
{
    // 원래 위치 , 부모 정보 , 켄버스
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
        // ui 충돌 방지로 켄버스의 자식으로 넣음
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
        if (this.transform.parent == rootCanvas.transform) // drop이 안되었을 경우
            returnToFrom();
    }

    public void returnToFrom()
    {
        transform.SetParent(parentRectTrans);
    }
}
