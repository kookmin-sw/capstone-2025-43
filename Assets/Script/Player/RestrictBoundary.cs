using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RestrictBoundary : MonoBehaviour
{
    private BoxCollider boundsCollider;
    private Bounds bounds;
    [SerializeField] Transform player;

    private void Start()
    {
        boundsCollider = GetComponent<BoxCollider>();
        bounds = boundsCollider.bounds;
        player = FindByLayer("Player");
    }

    private void LateUpdate()
    {
        if (player == null) return;

        Vector3 clampedPosition = player.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, bounds.min.x, bounds.max.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, bounds.min.y, bounds.max.y);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, bounds.min.z, bounds.max.z);

        player.position = clampedPosition;
    }

    private void OnValidate()
    {
        if (boundsCollider == null)
            boundsCollider = GetComponent<BoxCollider>();

        bounds = boundsCollider.bounds;
    }

    private void OnDrawGizmos()
    {
        if (boundsCollider == null)
            boundsCollider = GetComponent<BoxCollider>();

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(boundsCollider.bounds.center, boundsCollider.bounds.size);
    }

    private Transform FindByLayer(string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);
        if (layer == -1)
        {
            Debug.LogWarning($"Layer \"{layerName}\" not found.");
            return null;
        }

        foreach (Transform t in FindObjectsByType<Transform>(FindObjectsSortMode.None))
        {
            if (t.gameObject.layer == layer)
                return t;
        }

        return null;
    }
}
