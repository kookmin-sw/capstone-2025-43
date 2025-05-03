using UnityEngine;
public class Border : MonoBehaviour
{
    public LocalData localData;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D baseCollider;
    private void Awake()
    {
        baseCollider = GetComponent<PolygonCollider2D>();
    }
    public void Init(LocalData data)
    {
        baseCollider = GetComponent<PolygonCollider2D>();
        localData = data;
        spriteRenderer.sprite = data.image;
    }
    public bool inmyBound(Vector2 position)
    {
        return baseCollider.OverlapPoint(position);
    }
    public string getEnv()
    {
        return localData.env;
    }

}
