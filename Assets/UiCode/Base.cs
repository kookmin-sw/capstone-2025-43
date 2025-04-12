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

    public void MapCreate()
    {
        Managers.instance.resourceManager.Instantiate("ground", this.transform);
        Managers.instance.resourceManager.Instantiate("snow", this.transform);
        Managers.instance.resourceManager.Instantiate("forest", this.transform);
        Managers.instance.resourceManager.Instantiate("mount", this.transform);



    }

}
