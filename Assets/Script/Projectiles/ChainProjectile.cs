using UnityEngine;
public class ChainProjectile : MonoBehaviour
{
    public Transform target;
    public float speed = 100f;
    public float rotateSpeed = 500f;
    public float damage = 10f;
    public Character attacker;

    public int maxHits = 2;
    private int currentHits = 0;

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
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
            currentHits++;

            if (currentHits >= maxHits)
            {
                Destroy(gameObject);
                return;
            }

            Transform next = FindNextEnemy(character);
            if (next != null)
            {
                target = next;
                return;
            }

            Destroy(gameObject);
        }
    }

    Transform FindNextEnemy(Character justHit)
    {
        float minDist = Mathf.Infinity;
        Transform nextTarget = null;

        Collider[] colliders = Physics.OverlapSphere(transform.position, 15f);

        foreach (var col in colliders)
        {
            Character c = col.GetComponent<Character>();
            if (c == null || !attacker.IsEnemy(c) || c == justHit || c.Hp <= 0) continue;

            float dist = Vector3.Distance(transform.position, c.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nextTarget = c.transform;
            }
        }

        return nextTarget;
    }
}
