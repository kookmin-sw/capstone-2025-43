using UnityEngine;
using System.Collections.Generic;
public class Base : MonoBehaviour
{
    private PolygonCollider2D baseCollider;
    private void Awake()
    {
        baseCollider = GetComponent<PolygonCollider2D>();
    }
    public bool inmyBound(GameObject node)
    {
        return baseCollider.bounds.Contains(node.transform.position);        
    }
}
