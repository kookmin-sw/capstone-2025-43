using UnityEngine;

public class ExplodeOnDeath : MonoBehaviour
{
    public float radius = 5f;
    public float explosionDamage = 50f;
    public Character owner; 
    public GameObject bombIconPrefab;
    private GameObject bombIconInstance;
    public GameObject explosionEffectPrefab;

    private bool exploded = false;

    public void SetMark(GameObject prefab)
    {
        if (prefab == null) return;

        Character self = GetComponent<Character>();
        Transform head = self != null && self.torso != null ? self.torso : transform;
        Vector3 offset = new Vector3(0, 2.5f, 0);

        bombIconInstance = Instantiate(prefab, head.position + offset, Quaternion.identity, head);
    }

    public void TriggerExplosion()
    {
        if (exploded) return;
        exploded = true;

        if (bombIconInstance != null)
            Destroy(bombIconInstance);

        if (explosionEffectPrefab != null)
        {
            GameObject vfx = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            Destroy(vfx, 2f);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var col in colliders)
        {
            Character c = col.GetComponent<Character>();
            if (c != null && owner != null && owner.IsEnemy(c) && c.Hp > 0)
            {
                c.ApplyDamage(explosionDamage);
            }
        }
    }
}
