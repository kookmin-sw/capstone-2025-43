using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private float speed;
    private float rotateSpeed = 500f;
    private float damage;
    private Character attacker;

    private bool initialized = false;

    public void Initialize(Transform target, float speed, float damage, Character attacker)
    {
        this.target = target;
        this.speed = speed;
        this.damage = damage;
        this.attacker = attacker;
    }

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
        if (character != null && attacker != null && character != attacker && attacker.IsEnemy(character))
        {
            character.ApplyDamage(damage);
            Destroy(gameObject);
            
        }
    }
}
