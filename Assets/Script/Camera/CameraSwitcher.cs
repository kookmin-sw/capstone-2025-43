using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Transform[] characters; // 1~4번 캐릭터들의 Transform 배열
    public Vector3 offset = new Vector3(0, 10, -5); // 카메라 오프셋 (탑뷰)
    public float smoothSpeed = 5f; // 부드러운 전환 속도

    private Transform target; // 현재 카메라가 따라갈 대상

    void Start()
    {
        if (characters.Length > 0)
        {
            target = characters[0]; // 기본적으로 첫 번째 캐릭터 시점을 사용
            MoveCameraInstantly();
        }
    }

    void Update()
    {
        // 키 입력 확인 및 캐릭터 시점 변경
        if (Input.GetKeyDown(KeyCode.Alpha1)) SetTarget(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetTarget(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetTarget(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SetTarget(3);
    }

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.LookAt(target.position);
        }
    }

    void SetTarget(int index)
    {
        if (index < characters.Length)
        {
            target = characters[index];
        }
    }

    void MoveCameraInstantly()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}
