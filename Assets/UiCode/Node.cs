using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 0.5f, -0.5f);
    public void SetPosition(Vector3 position)
    {
        transform.position = position + offset;
    }
}
