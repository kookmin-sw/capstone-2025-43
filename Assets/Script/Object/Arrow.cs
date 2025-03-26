using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Transform target;       
    public float speed = 10f;      
    public float rotateSpeed = 500f; 

    void Update()
    {
        if (target == null) return;

        // direction
        Vector3 direction = target.position - transform.position;
        direction.Normalize();

        // rotate
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        // move forward
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TestEnemy"))
        {
            Destroy(gameObject);
        }
    }
}