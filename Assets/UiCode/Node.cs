using UnityEngine;

public class Node : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("�浹��");
        GameObject collisonObject = collision.collider.gameObject;
        Debug.Log(collisonObject.name);
    }
}
