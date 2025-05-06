using System.Collections.Generic;
using UnityEngine;

public class StackedHitProjectile : MonoBehaviour
{
    public Transform target;
    public float speed = 80f;
    public float damage = 10f;
    public Character attacker;
    public static Dictionary<Character, int> hitCounts = new();

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (character != null && character.transform == target && attacker.IsEnemy(character))
        {
            if (!hitCounts.ContainsKey(character))
                hitCounts[character] = 0;

            hitCounts[character]++;
            float totalDamage = damage;

            if (hitCounts[character] >= 4)
            {
                totalDamage += 20f; 
                hitCounts[character] = 0; 
            }

            character.ApplyDamage(totalDamage);
            Destroy(gameObject);
        }
    }
}
