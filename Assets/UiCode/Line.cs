using UnityEngine;

public class Line : MonoBehaviour
{
    public GameObject node0;
    public GameObject node1;
    public float lineWidth;

    LineRenderer lineRenderer;

    public void Init(GameObject node0, GameObject node1)
    {
        this.node0 = node0;
        this.node1 = node1;
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 3;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth; 
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.SetPosition(0, node0.transform.position - node0.GetComponent<Node>().offset);
        lineRenderer.SetPosition(1, node1.transform.position - node1.GetComponent<Node>().offset);
        lineRenderer.SetPosition(2, node0.transform.position - node1.GetComponent<Node>().offset);
        SetColor();
    }

    public void SetColor()
    {
        Color color = Color.white;
        if (node1.tag != node0.tag)
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
