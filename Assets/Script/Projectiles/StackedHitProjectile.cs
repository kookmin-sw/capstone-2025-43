using UnityEngine;
using System.Collections.Generic;

public class StackedHitProjectile : MonoBehaviour
{
    public Transform target;
    public float speed = 80f;
    public float damage = 10f;
    public Character attacker;

    private static Dictionary<Character, int> hitCounts = new();

    private void Update()
    {
        if (target == null || !target.gameObject.activeSelf)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 toTarget = target.position - transform.position;
        if (toTarget == Vector3.zero) return;

        Vector3 direction = toTarget.normalized;
        if (float.IsNaN(direction.x) || float.IsNaN(direction.y) || float.IsNaN(direction.z))
        {
            Debug.LogWarning("[StackedHitProjectile] NaN detected.");
            Destroy(gameObject);
            return;
        }

        transform.position += direction * speed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (character == null || attacker == null || character == attacker) return;
        if (!attacker.IsEnemy(character)) return;

        if (!hitCounts.ContainsKey(character))
            hitCounts[character] = 0;

        hitCounts[character]++;

        float totalDamage = damage;
        if (hitCounts[character] >= 4)
        {
            totalDamage += 100f;           
            hitCounts[character] = 0;     
        }

        character.ApplyDamage(totalDamage);
        Destroy(gameObject);
    }
}
