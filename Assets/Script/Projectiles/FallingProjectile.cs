using UnityEngine;

public class FallingProjectile : MonoBehaviour
{
    public Transform target;
    public float fallSpeed = 30f;
    public float damage = 10f;
    public Character attacker;

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
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
