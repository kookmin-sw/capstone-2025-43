using UnityEngine;

public class Archer : MonoBehaviour
{
    public GameObject arrowPrefab;  // 화살 프리팹
    public Transform firePoint;     // 화살이 생성될 위치
    public float arrowSpeed = 10f;  // 화살 속도

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 왼쪽 마우스 클릭 시 발사
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();

    }
}
