using UnityEngine;

public class Arrow : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 3f); // 3초 후 화살 제거
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject); // 충돌 시 화살 제거
        }
    }
}
