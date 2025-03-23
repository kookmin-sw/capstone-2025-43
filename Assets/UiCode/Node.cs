using UnityEngine;

public class Node : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Ãæµ¹ÇÔ");
        GameObject collisonObject = collision.collider.gameObject;
        Debug.Log(collisonObject.name);
    }
}
