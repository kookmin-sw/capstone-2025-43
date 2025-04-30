using UnityEngine;

public class Line : MonoBehaviour
{
    public Vector2 p0;
    public Vector2 p1;
    public float lineWidth = 0.3f;

    LineRenderer lineRenderer;

    public void Init(Vector2 p0, Vector2 p1)
    {
        this.p0 = p0;
        this.p1 = p1;
        SetLine();
        SetColor();
    }

    public void SetLine()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 3;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.SetPosition(0, p0);
        lineRenderer.SetPosition(1, p1);
        lineRenderer.SetPosition(2, p0);
    }

    public void SetColor()
    {
        string tag0 = Managers.Data.handOverData.localInfos[p0].side;
        string tag1 = Managers.Data.handOverData.localInfos[p1].side;
        Color color = Color.white;
        if (tag0 != tag1)
        {
            color = Color.red;
        }
        else if (tag1 == "Ally")
            color = Color.green;
        else if (tag1 == "Enemy")
            color = Color.blue;
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }
}
