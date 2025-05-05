using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    public float speed = 20f;
    public float rotateSpeed = 500f;
    public float damage = 10f;
    public Character attacker;

    private bool initialized = false;


    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        if (!initialized)
        {
            transform.rotation = Quaternion.LookRotation((target.position - transform.position).normalized);
            initialized = true;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (character != null && character.transform == target && attacker.IsEnemy(character))
        {
            character.ApplyDamage(damage);
            Destroy(gameObject);
        }
    }
}