using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float dragSpeed = 1f; // 드래그 속도 조절
    private Vector3 dragOrigin;  // 드래그 시작점
    public float zoomSpeed = 5f;
    public float minZoom = 10f;
    public float maxZoom = 60f;
    public float panSpeed = 10f;

    public SpriteRenderer backgroundSprite; // 배경 이미지
    public float padding = 0f; // 약간의 여백 추가

    private float minX, maxX, minY, maxY;

    private bool isFocusing = false;


    void Start()
    {
        Bounds bounds = backgroundSprite.bounds;

        float vertExtent = Camera.main.orthographic
            ? Camera.main.orthographicSize
            : Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad) * Mathf.Abs(transform.position.z);

        float horzExtent = vertExtent * Camera.main.aspect;

        minX = bounds.min.x + horzExtent;
        maxX = bounds.max.x - horzExtent;
        minY = bounds.min.y + vertExtent;
        maxY = bounds.max.y - vertExtent;

        Debug.Log($"[CameraLimit] minX:{minX}, maxX:{maxX}, minY:{minY}, maxY:{maxY}");
    }



    void Update()
    {
        //Camera.main.fieldOfView = 60f;

        if (Drag.isDragging) return;

        if (!Managers.Ui.IsOnlyDefaultOpen())
            return;

        HandleZoom();
        HandleDrag();
        ClampCameraPosition();
    }

    void HandleDrag()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return; // UI 클릭 시 카메라 이동 방지

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
        if (isFocusing) return;

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

    void ClampCameraPosition()
    {
        // 카메라 절반 크기 계산
        float vertExtent = Camera.main.orthographic
            ? Camera.main.orthographicSize
            : Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad) * Mathf.Abs(transform.position.z);

        float horzExtent = vertExtent * Camera.main.aspect;

        // 배경 Sprite 기준
        Bounds bounds = backgroundSprite.bounds;

        float minX = bounds.min.x + horzExtent;
        float maxX = bounds.max.x - horzExtent;
        float minY = bounds.min.y + vertExtent;
        float maxY = bounds.max.y - vertExtent;

        // Clamp 적용
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }

    
    public void FocusOnNode(Transform targetNode, float targetZoom = 30f, float duration = 1.0f)
    {
        isFocusing = true;
        StartCoroutine(FocusCoroutine(targetNode.position, targetZoom, duration));
    }

    IEnumerator FocusCoroutine(Vector3 targetPos, float targetZoom, float duration)
    {
        Vector3 startPos = transform.position;
        float startZoom = Camera.main.fieldOfView;
        float time = 0f;

        targetPos.z = startPos.z;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, time / duration);

            transform.position = Vector3.Lerp(startPos, targetPos, t);
            Camera.main.fieldOfView = Mathf.Lerp(startZoom, targetZoom, t);

            yield return null;
        }

        transform.position = targetPos;
        Camera.main.fieldOfView = targetZoom;

        isFocusing = false;
    }
}
