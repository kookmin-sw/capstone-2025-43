using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public float dragSpeed = 1f; // 드래그 속도 조절
    private Vector3 dragOrigin;  // 드래그 시작점
    public float zoomSpeed = 5f;
    public float minZoom = 10f;
    public float maxZoom = 60f;

    public float panSpeed = 10f;


    void Update()
    {
        //Camera.main.fieldOfView = 60f;

        if (Drag.isDragging) return;

        // 카메라 이동 (화살표 키 / WASD)
        float moveX = Input.GetAxis("Horizontal") * panSpeed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * panSpeed * Time.deltaTime;
        transform.position += new Vector3(moveX, moveY, 0);

        HandleZoom();
        HandleDrag();
    }

    void HandleDrag()
    {
        if (EventSystem.current.IsPointerOverGameObject())  return; // UI 클릭 시 카메라 이동 방지

        if (Input.GetMouseButtonDown(0)) // 마우스 클릭 시
        {
            dragOrigin = GetMouseWorldPosition();
        }
        if (Input.GetMouseButton(0))    // 마우스를 누르고 있으면 이동
        {
            Vector3 currentMousePosition = GetMouseWorldPosition();  // 현재 마우스 위치

            Vector3 difference = currentMousePosition - dragOrigin;  // 드래그 시작점과의 차이 계산

            transform.position += new Vector3(difference.x, difference.y, 0f) * (Time.deltaTime * dragSpeed);  // 카메라 이동
        }
    }


    void HandleZoom()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // 마우스 휠 줌 기능
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.fieldOfView -= scroll * zoomSpeed;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, minZoom, maxZoom);
    }

    Vector3 GetMouseWorldPosition()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Camera.main.transform.forward, Vector3.zero); // 카메라의 forward 방향을 기준으로 평면 설정

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);  // 광선과 평면이 만나는 지점
            return hitPoint;  
        }

        return Vector3.zero;
    }
}
