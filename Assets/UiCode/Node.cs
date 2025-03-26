using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Node : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(this.name + " and " + collision.transform.name);
        if (!collision.bounds.Contains(this.transform.position))
            Destroy(this.transform);
    }
}
