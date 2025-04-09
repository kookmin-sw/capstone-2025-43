using UnityEngine;

public class Line : MonoBehaviour
{
    public GameObject node1;
    public GameObject node2;
    LineRenderer lineRenderer;
    public void SetColor()
    {
        Color color = Color.white;
        if (node1.tag != node2.tag)
        {
            color = Color.red;
        }
        else if (node1.tag == "Ally")
            color = Color.green;
        else if (node1.tag == "Enemy")
            color = Color.blue;
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }
}
