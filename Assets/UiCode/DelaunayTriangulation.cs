using System.Collections.Generic;
using UnityEngine;

public class DelaunayTriangulation : MonoBehaviour
{
    public class Edge
    {
        public Vector2 v0;
        public Vector2 v1;

        public Edge(Vector2 v0, Vector2 v1)
        {
            this.v0 = v0;
            this.v1 = v1;
        }

        public override bool Equals(object other)
        {
            if (false == (other is Edge))
            {
                return false;
            }

            return Equals((Edge)other);
        }

        public bool Equals(Edge edge)
        {
            return ((this.v0.Equals(edge.v0) && this.v1.Equals(edge.v1)) || (this.v0.Equals(edge.v1) && this.v1.Equals(edge.v0)));
        }

        public override int GetHashCode()
        {
            return v0.GetHashCode() ^ (v1.GetHashCode() << 2);
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
        public Vector2 a;
        public Vector2 b;
        public Vector2 c;
        public Circle circumCircle;
        public List<Edge> edges;

        public LineRenderer lineRenderer = null;   // 삼각형을 그리기 위한 렌더러

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

        public override bool Equals(object other)
        {
            return (other is Triangle) ? Equals((Triangle)other) : false;
        }

        public override int GetHashCode()
        {
            return a.GetHashCode() ^ (b.GetHashCode() << 2) ^ (c.GetHashCode() >> 2);
        }

        public bool Equals(Triangle triangle)
        {
            return this.a == triangle.a && this.b == triangle.b && this.c == triangle.c;
        }

        private Circle calcCircumCircle(Vector2 a, Vector2 b, Vector2 c)
        {
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
    public void AddPoint(Vector2 point)
    {
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
            children.Remove(badTriangle.lineRenderer.gameObject);
            GameObject.Destroy(badTriangle.lineRenderer.gameObject);
        }

        // 공백 경계와 새로 추가된 점들을 연결해 새로운 삼각형 생성
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
    public void ActivateCircle(bool flag)
    {
        foreach (var triangle in triangles)
        {
            if (null == triangle.circumCircle)
            {
                continue;
            }

            triangle.circumCircle.lineRenderer.gameObject.SetActive(flag);
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
            children.Remove(triangle.lineRenderer.gameObject);
            GameObject.Destroy(triangle.lineRenderer.gameObject);
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
        Vector2 a = new Vector2(minX - dx, minY - dy);
        Vector2 b = new Vector2(minX - dx, maxY + dy * 3);
        Vector2 c = new Vector2(maxX + dx * 3, minY - dy);

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
    private Triangle CreateTriangle(Vector2 a, Vector2 b, Vector2 c)
    {
        if (a == b || b == c || c == a)
        {
            return null;
        }

        Triangle triangle = new Triangle(a, b, c);
        {
            LineRenderer lineRenderer = CreateLineRenderer($"Triangle_{triangleNo}", Color.blue);
            lineRenderer.positionCount = 4;
            lineRenderer.SetPosition(0, a);
            lineRenderer.SetPosition(1, b);
            lineRenderer.SetPosition(2, c);
            lineRenderer.SetPosition(3, a);
            lineRenderer.useWorldSpace = false;
            triangle.lineRenderer = lineRenderer;
        }
        triangleNo++;

        return triangle;
    }

    private LineRenderer CreateLineRenderer(string name, Color color)
    {
        const float lineWidth = 0.04f;
        var go = new GameObject();
        this.children.Add(go);
        go.name = name;
        go.transform.parent = transform;

        var lineRenderer = go.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.sortingOrder = 1;

        return lineRenderer;
    }

    /// <summary>
    /// 나중에 local icon으로 바꿔야함
    /// </summary>
    /// <param name="name"> local icon이 들고갈 이름</param>
    /// <param name="color">local icon의 색 <- 지워야할듯</param>
    /// <param name="position">local icon의 위치 </param>
    /// <returns></returns>
    public SpriteRenderer CreatePoint(string name, Color color, Vector2 position)
    {
        float size = 0.3f;
        float imageSize = 100.0f * size;    // 유니티 픽셀 유닛을 100으로 설정했다고 가정함

        var go = new GameObject();
        this.children.Add(go);
        go.name = name;
        go.transform.parent = transform;
        go.transform.position = new Vector2(position.x - size / 2, position.y - size / 2);

        var texture = new Texture2D((int)imageSize, (int)imageSize);
        var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width,
            texture.height), Vector2.zero, 100, 0, SpriteMeshType.FullRect, Vector4.zero, false);

        var spriteRenderer = go.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        spriteRenderer.color = color;
        spriteRenderer.sortingOrder = 2;

        return spriteRenderer;
    }

}