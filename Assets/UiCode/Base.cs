using UnityEngine;
using System.Collections.Generic;
public class Base : MonoBehaviour
{
    private PolygonCollider2D baseCollider;
    private void Awake()
    {
        baseCollider = GetComponent<PolygonCollider2D>();
    }
    public bool inmyBound(Vector2 position)
    {
        return baseCollider.OverlapPoint(position);
    }

}
