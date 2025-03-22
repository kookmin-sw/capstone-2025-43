using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class draggableTactic : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject test; 
    [HideInInspector] public Transform parentAfterDrag;
    private CanvasGroup canvasGroup;
    public Tactic tactic;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        SetRaycastTarget(test, false);

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        transform.position = parentAfterDrag.position;

        SetRaycastTarget(test, true);

        canvasGroup.blocksRaycasts = true;
    }

    private void SetRaycastTarget(GameObject target, bool state)
    {
        if (target == null) return;

        foreach (Image img in target.GetComponentsInChildren<Image>(true))
        {
            img.raycastTarget = state;
        }

        foreach (RawImage rawImg in target.GetComponentsInChildren<RawImage>(true))
        {
            rawImg.raycastTarget = state;
        }
    }
}
