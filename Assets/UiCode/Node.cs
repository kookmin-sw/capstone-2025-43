using UnityEngine;

public class Node : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("�浹��");
        GameObject collisonObject = collision.collider.gameObject;
        Debug.Log(this.name + " and " + collisonObject.name);
    }
}
