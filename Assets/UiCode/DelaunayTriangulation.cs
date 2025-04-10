using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DelaunayTriangulation : MonoBehaviour
{
    public class Edge
    {
        public GameObject obj0;
        public GameObject obj1;

        public Edge(GameObject obj0, GameObject obj1)
        {
            this.obj0 = obj0;
            this.obj1 = obj1;
        }

        public bool Equals(Edge edge)
        {
            return ((this.obj0 == edge.obj0 && this.obj1 == edge.obj1) || (this.obj1 == edge.obj0 && this.obj0 == edge.obj1));
        }
    }

    public class Circle
    {
        public Vector2 center;
        public float radius;
        public LineRenderer lineRenderer = null;   // 원을 그리기 위한 렌더러

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

    public class Triangle
    {
        public GameObject a;
        public GameObject b;
        public GameObject c;
        public Circle circumCircle;
        public List<Edge> edges;
        public Triangle(GameObject a, GameObject b, GameObject c)
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

        private Circle calcCircumCircle(GameObject objA, GameObject objB, GameObject objC)
        {
            Vector2 a = objA.GetComponent<Node>().pin;
            Vector2 b = objB.GetComponent<Node>().pin;
            Vector2 c = objC.GetComponent<Node>().pin; 
            // 출처: 삼각형 외접원 구하기 - https://kukuta.tistory.com/444

            if (a == b || b == c || c == a) // 같은 점이 있음. 삼각형 아님. 외접원 구할 수 없음.
            {
                return null;
            }

            float mab = (b.x - a.x) / (b.y - a.y) * -1.0f;  // 직선 ab에 수직이등분선의 기울기
            float a1 = (b.x + a.x) / 2.0f;                  // 직선 ab의 x축 중심 좌표
            float b1 = (b.y + a.y) / 2.0f;                  // 직선 ab의 y축 중심 좌표

            // 직선 bc
            float mbc = (b.x - c.x) / (b.y - c.y) * -1.0f;  // 직선 bc에 수직이등분선의 기울기
            float a2 = (b.x + c.x) / 2.0f;                  // 직선 bc의 x축 중심 좌표
            float b2 = (b.y + c.y) / 2.0f;                  // 직선 bc의 y축 중심 좌표

            if (mab == mbc)     // 두 수직이등분선의 기울기가 같음. 평행함. 
            {
                return null;    // 교점 구할 수 없음
            }

            float x = (mab * a1 - mbc * a2 + b2 - b1) / (mab - mbc);
            float y = mab * (x - a1) + b1;

            if (b.x == a.x)     // 수직이등분선의 기울기가 0인 경우(수평선)
            {
                x = a2 + (b1 - b2) / mbc;
                y = b1;
            }

            if (b.y == a.y)     // 수직이등분선의 기울기가 무한인 경우(수직선)
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

            if (b.x == c.x)     // 수직이등분선의 기울기가 0인 경우(수평선)
            {
                x = a1 + (b2 - b1) / mab;
                y = b2;
            }

            if (b.y == c.y)     // 수직이등분선의 기울기가 무한인 경우(수직선)
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

    private int triangleNo = 0;
    public Triangle superTriangle = null;
    public List<Triangle> triangles = new List<Triangle>();
    private List<GameObject> children = new List<GameObject>();

    public void Init(int width, int height)
    {
        foreach (GameObject child in children)
        {
            GameObject.Destroy(child);
        }

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
    public void AddPoint(GameObject obj)
    {
        Vector2 point = obj.transform.GetComponent<Node>().pin;
        // 추가 되는 외접원에 점이 포함되어 삭제될 삼각형 목록
        List<Triangle> badTriangles = new List<Triangle>();
        foreach (var triangle in triangles)
        {
            if (triangle.circumCircle.Contains(point)) // 삼각형의 외접원에 점이 포함 되는가
            {
                badTriangles.Add(triangle);
            }
        }

        // 삼각형 삭제로 인한 공백 영역의 경계 찾기
        List<Edge> polygon = new List<Edge>();
        foreach (var triangle in badTriangles)
        {
            List<Edge> edges = triangle.edges;

            foreach (Edge edge in edges)
            {
                // find unique edge
                // 삼각형과 공유 되지 않는 변만이 공백 영역의 경계가 된다.
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

        // 공백 경계와 새로 추가된 점들을 연결해 새로운 삼각형 생성
        foreach (Edge edge in polygon)
        {
            Triangle triangle = CreateTriangle(edge.obj0, edge.obj1, obj);
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
            Destroy(triangle.a);
            Destroy(triangle.b);
            Destroy(triangle.c);
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

        // super triangle을 포인트 리스트 보다 크게 잡는 이유는
        // super triangle의 변과 포인트가 겹치게 되면 삼각형이 아닌 직선이 되므로 델로네 삼각분할을 적용할 수 없기 때문이다.
        GameObject a = Managers.instance.resourceManager.Instantiate("Node", this.transform);
        GameObject b = Managers.instance.resourceManager.Instantiate("Node", this.transform); ;
        GameObject c = Managers.instance.resourceManager.Instantiate("Node", this.transform); ;
        a.transform.position = new Vector2(minX - dx, minY - dy);
        b.transform.position = new Vector2(minX - dx, maxY + dy * 3);
        c.transform.position = new Vector2(maxX + dx * 3, minY - dy);

        // super triangle이 직선인 경우 리턴
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
    private Triangle CreateTriangle(GameObject a, GameObject b, GameObject c)
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