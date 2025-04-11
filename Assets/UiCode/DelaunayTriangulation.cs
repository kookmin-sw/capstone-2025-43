using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DelaunayTriangulation : MonoBehaviour
{
    private int triangleNo = 0;
    public Triangle superTriangle = null;
    public List<Triangle> triangles = new List<Triangle>();
    
    public void Init(int width, int height)
    {
        triangles.Clear();
        triangleNo = 0;

        List<Vector2> points = new List<Vector2>();

        points.Add(new Vector2(0.0f, 0.0f));
        points.Add(new Vector2(0.0f, height));
        points.Add(new Vector2(width, height));
        points.Add(new Vector2(width, 0.0f));

        superTriangle = CreateSuperTriangle(points);
        if (superTriangle != null)
            triangles.Add(superTriangle);

        return;
    }
    public void AddPoint(Vector2 point)
    {
        // �߰� �Ǵ� �������� ���� ���ԵǾ� ������ �ﰢ�� ���
        List<Triangle> badTriangles = new List<Triangle>();
        foreach (var triangle in triangles)
        {
            if (triangle.circumCircle.Contains(point)) // �ﰢ���� �������� ���� ���� �Ǵ°�
            {
                badTriangles.Add(triangle);
            }
        }

        // �ﰢ�� ������ ���� ���� ������ ��� ã��
        List<Edge> polygon = new List<Edge>();
        foreach (var triangle in badTriangles)
        {
            List<Edge> edges = triangle.edges;

            foreach (Edge edge in edges)
            {
                // find unique edge
                // �ﰢ���� ���� ���� �ʴ� ������ ���� ������ ��谡 �ȴ�.
                bool unique = true;
                foreach (var other in badTriangles)
                {
                    if (true == triangle.Equals(other))
                    {
                        continue;
                    }

                    foreach (var otherEdge in other.edges)
                    {
                        if (true == edge.Equals(otherEdge))
                        {
                            unique = false;
                            break;
                        }
                    }

                    if (false == unique)
                    {
                        break;
                    }
                }

                if (true == unique)
                {
                    polygon.Add(edge);
                }
            }
        }

        foreach (var badTriangle in badTriangles)
        {
            triangles.Remove(badTriangle);
        }

        // ���� ���� ���� �߰��� ������ ������ ���ο� �ﰢ�� ����
        foreach (Edge edge in polygon)
        {
            Triangle triangle = CreateTriangle(edge.v0, edge.v1, point);
            if (null == triangle)
            {
                continue;
            }
            triangles.Add(triangle);
        }
    }
    public void RemoveSuperTriangle()
    {
        if (null == superTriangle)
        {
            return;
        }

        List<Triangle> remove = new List<Triangle>();
        foreach (var triangle in triangles)
        {
            if (true == (triangle.a == superTriangle.a || triangle.a == superTriangle.b || triangle.a == superTriangle.c ||
                         triangle.b == superTriangle.a || triangle.b == superTriangle.b || triangle.b == superTriangle.c ||
                         triangle.c == superTriangle.a || triangle.c == superTriangle.b || triangle.c == superTriangle.c
               )
            )
            {
                remove.Add(triangle);
            }
        }

        foreach (var triangle in remove)
        {
            triangles.Remove(triangle);
        }
    }

    private Triangle CreateSuperTriangle(List<Vector2> points)
    {
        float minX = float.MaxValue;
        float maxX = float.MinValue;
        float minY = float.MaxValue;
        float maxY = float.MinValue;

        foreach (Vector2 point in points)
        {
            minX = Mathf.Min(minX, point.x);
            maxX = Mathf.Max(maxX, point.x);
            minY = Mathf.Min(minY, point.y);
            maxY = Mathf.Max(maxY, point.y);
        }

        float dx = maxX - minX;
        float dy = maxY - minY;

        // super triangle�� ����Ʈ ����Ʈ ���� ũ�� ��� ������
        // super triangle�� ���� ����Ʈ�� ��ġ�� �Ǹ� �ﰢ���� �ƴ� ������ �ǹǷ� ���γ� �ﰢ������ ������ �� ���� �����̴�.
        Vector2 a = new Vector2(minX - dx, minY - dy);
        Vector2 b = new Vector2(minX - dx, maxY + dy * 3);
        Vector2 c = new Vector2(maxX + dx * 3, minY - dy);

        // super triangle�� ������ ��� ����
        if (a == b || b == c || c == a)
        {
            return null;
        }
        /*
        CreatePoint("Point", Color.red, a);
        CreatePoint("Point", Color.red, b);
        CreatePoint("Point", Color.red, c);
        */
        return CreateTriangle(a, b, c);
    }
    private Triangle CreateTriangle(Vector2 a, Vector2 b, Vector2 c)
    {
        if (a == b || b == c || c == a)
        {
            return null;
        }

        Triangle triangle = new Triangle(a, b, c);
        triangleNo++;

        return triangle;
    }
}
