using UnityEngine;
using System.Collections.Generic;

public class ChainProjectile : MonoBehaviour
{
    public Transform target;
    public float speed = 100f;
    public float rotateSpeed = 500f;
    public float damage = 10f;
    public Character attacker;

    public int maxHits = 2;
    private int currentHits = 0;
    private List<Character> hitTargets = new List<Character>();

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
            Debug.LogWarning("[ChainProjectile] NaN direction detected.");
            Destroy(gameObject);
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (character == null) return;
        if (attacker == null || character == attacker) return;
        if (!attacker.IsEnemy(character)) return;
        if (hitTargets.Contains(character)) return;

        character.ApplyDamage(damage);
        hitTargets.Add(character);
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

            Collider col = GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = false;
                col.enabled = true;
            }

            transform.position += transform.forward * 0.3f;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private Transform FindNextEnemy(Character justHit)
    {
        float minDist = Mathf.Infinity;
        Transform nextTarget = null;

        Collider[] colliders = Physics.OverlapSphere(transform.position, 15f);
        foreach (var col in colliders)
        {
            Character c = col.GetComponent<Character>();
            if (c == null || c.Hp <= 0) continue;
            if (attacker == null || !attacker.IsEnemy(c)) continue;
            if (hitTargets.Contains(c)) continue;

            float dist = Vector3.Distance(transform.position, c.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nextTarget = c.torso != null ? c.torso : c.transform;
            }
        }

        return nextTarget;
    }
}
