using System.Collections.Generic;
using UnityEngine;

public class Edge
{
    public Vector2 v0;
    public Vector2 v1;

    public Edge(Vector2 v0, Vector2 v1)
    {
        this.v0 = v0;
        this.v1 = v1;
    }
    public override bool Equals(object obj)
    {
        if (obj is Edge edge)
        {
            return (v0 == edge.v0 && v1 == edge.v1) || (v0 == edge.v1 && v1 == edge.v0);
        }
        return false;
    }

    public override int GetHashCode()
    {
        // ������ ������� ���� �ؽø� ��ȯ�ϵ��� �����ؼ� ó��
        int hash1 = v0.GetHashCode() ^ v1.GetHashCode();
        int hash2 = v1.GetHashCode() ^ v0.GetHashCode();
        return hash1 ^ hash2;
    }
}

public class Triangle
{
    public Vector2 a;
    public Vector2 b;
    public Vector2 c;
    public Circle circumCircle;
    public List<Edge> edges;
    public Triangle(Vector2 a, Vector2 b, Vector2 c)
    {
        this.a = a;
        this.b = b;
        this.c = c;

        this.circumCircle = calcCircumCircle(a, b, c);
        this.edges = new List<Edge>();
        this.edges.Add(new Edge(this.a, this.b));
        this.edges.Add(new Edge(this.b, this.c));
        this.edges.Add(new Edge(this.c, this.a));
    }

    public bool Equals(Triangle triangle)
    {
        return this.a == triangle.a && this.b == triangle.b && this.c == triangle.c;
    }

    private Circle calcCircumCircle(Vector2 a, Vector2 b, Vector2 c)
    {
        // ��ó: �ﰢ�� ������ ���ϱ� - https://kukuta.tistory.com/444

        if (a == b || b == c || c == a) // ���� ���� ����. �ﰢ�� �ƴ�. ������ ���� �� ����.
        {
            return null;
        }

        float mab = (b.x - a.x) / (b.y - a.y) * -1.0f;  // ���� ab�� �����̵�м��� ����
        float a1 = (b.x + a.x) / 2.0f;                  // ���� ab�� x�� �߽� ��ǥ
        float b1 = (b.y + a.y) / 2.0f;                  // ���� ab�� y�� �߽� ��ǥ

        // ���� bc
        float mbc = (b.x - c.x) / (b.y - c.y) * -1.0f;  // ���� bc�� �����̵�м��� ����
        float a2 = (b.x + c.x) / 2.0f;                  // ���� bc�� x�� �߽� ��ǥ
        float b2 = (b.y + c.y) / 2.0f;                  // ���� bc�� y�� �߽� ��ǥ

        if (mab == mbc)     // �� �����̵�м��� ���Ⱑ ����. ������. 
        {
            return null;    // ���� ���� �� ����
        }

        float x = (mab * a1 - mbc * a2 + b2 - b1) / (mab - mbc);
        float y = mab * (x - a1) + b1;

        if (b.x == a.x)     // �����̵�м��� ���Ⱑ 0�� ���(����)
        {
            x = a2 + (b1 - b2) / mbc;
            y = b1;
        }

        if (b.y == a.y)     // �����̵�м��� ���Ⱑ ������ ���(������)
        {
            x = a1;
            if (0.0f == mbc)
            {
                y = b2;
            }
            else
            {
                y = mbc * (a1 - a2) + b2;
            }
        }

        if (b.x == c.x)     // �����̵�м��� ���Ⱑ 0�� ���(����)
        {
            x = a1 + (b2 - b1) / mab;
            y = b2;
        }

        if (b.y == c.y)     // �����̵�м��� ���Ⱑ ������ ���(������)
        {
            x = a2;
            if (0.0f == mab)
            {
                y = b1;
            }
            else
            {
                y = mab * (a2 - a1) + b1;
            }
        }

        Vector2 center = new Vector2(x, y);
        float radius = Vector2.Distance(center, a);

        return new Circle(center, radius);
    }
}

public class Circle
{
    public Vector2 center;
    public float radius;
    public LineRenderer lineRenderer = null;   // ���� �׸��� ���� ������

    public Circle(Vector2 center, float radius)
    {
        this.center = center;
        this.radius = radius;
        this.lineRenderer = null;
    }

    public bool Contains(Vector2 point)
    {
        return radius >= Vector2.Distance(center, point);
    }
}

