using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float rotateSpeed = 10f;
    public float moveSpeed = 30f;

    private Rigidbody rb;
    private Transform target;
    private bool hasHit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = FindClosestTarget();
        rb.linearVelocity = transform.forward * moveSpeed;
    }

    void FixedUpdate()
    {
        if (hasHit || target == null) return;

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // 부드럽게 목표 방향으로 회전시키기
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed));

        // 속도 방향을 현재 화살 방향으로 강제로 맞춤 (빙글빙글 도는 현상 방지)
        rb.linearVelocity = transform.forward * moveSpeed;
    }

    private Transform FindClosestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject obj in targets)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = obj.transform;
            }
        }
        return closest;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
        hasHit = true;
        Destroy(gameObject);
        }
    }
}