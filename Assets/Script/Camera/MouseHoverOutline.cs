using UnityEngine;

public class MouseHoverOutline : MonoBehaviour
{
    public LayerMask Outlinemask;//감지할 LayerMask
    private Camera cam;

    // 레이캐스트 주기 설정
    private float raycastInterval = 0.05f;
    private float nextRaycastTime = 0f;

    private Outline lastOutline; // 가장 최근의 아웃라인

    void Start()
    {
        cam = Camera.main; // 카메라 참조
    }

    void Update()
    {
        if (Time.time >= nextRaycastTime)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); 
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, Outlinemask))
            {
                Outline outline = hit.collider.GetComponent<Outline>();
                if (outline != null)
                {
                    if (lastOutline != outline) // 새로운 오브젝트에 호버링할 때
                    {
                        ResetLastOutline(); // 이전 오브젝트의 아웃라인 비활성화

                        lastOutline = outline; 
                        lastOutline.enabled = true;
                    }
                }
            }
            else
            {
                ResetLastOutline(); // 마우스가 오브젝트 밖으로 나가면 아웃라인 비활성화
            }

            nextRaycastTime = Time.time + raycastInterval; // 레이캐스트 주기 설정
        }
    }

    void ResetLastOutline()
    {
        if (lastOutline != null)
        {
            lastOutline.enabled = false; // 이전 오브젝트의 아웃라인 비활성화
            lastOutline = null; // 아웃라인 초기화
        }
    }
}
