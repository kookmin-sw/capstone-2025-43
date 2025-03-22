using UnityEngine;

public class MouseHoverEvent : MonoBehaviour
{
    public LayerMask Outlinemask;//Raycasted LayerMask
    private Camera cam;

    // RayCast Interval Settings
    private float raycastInterval = 0.05f;
    private float nextRaycastTime = 0f;

    private Outline lastOutline;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {

        if (lastOutline)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("PopUP!");
                PopUpTacticSystemUI();
            }
        }

        if (Time.time >= nextRaycastTime)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, Outlinemask))
            {
                Outline outline = hit.collider.GetComponent<Outline>();
                if (outline != null)
                {
                    if (lastOutline != outline)
                    {
                        ResetLastOutline();

                        lastOutline = outline;
                        lastOutline.enabled = true;
                    }
                }
            }
            else
            {
                ResetLastOutline();
            }

            nextRaycastTime = Time.time + raycastInterval;
        }
    }

    void ResetLastOutline()
    {
        if (lastOutline != null)
        {
            lastOutline.enabled = false;
            lastOutline = null;
        }
    }

    void PopUpTacticSystemUI()
    {
        if (lastOutline)
            TacticUIManager.Instance.OpenTacticUI(lastOutline.GetComponent<Character>());
    }
}
