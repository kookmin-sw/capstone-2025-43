using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 0.5f, -0.5f);

    public void Init(Vector3 position)
    {
        //Set Position
        transform.position = position + offset;
     
        //Set Tag
        if (Managers.instance.gameManager.inBorderAlly(transform.position))
            transform.tag = "Ally";
        else
            transform.tag = "Enemy";
    }
}
