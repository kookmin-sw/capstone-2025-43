using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
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

    public void EnvCreate(string env)
    {
        GameObject a = Managers.instance.resourceManager.Instantiate(env, this.transform);
        a.AddComponent<PolygonCollider2D>();
    }

}
