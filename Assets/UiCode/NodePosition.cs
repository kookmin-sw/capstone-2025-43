using UnityEngine;

public class NodePosition : MonoBehaviour
{
    public struct Point
    {
        public int x, y;
    };
    public const int size = 30;
    public Point[] points = new Point[size];
    private bool[,] flag = new bool[size, size];
    public void CreateRandomSpot()
    {
        Debug.Log("·»´ýÁß");
        for (int i = 0; i < size;)
        {
            points[i].x = Random.Range(0, size);
            points[i].y = Random.Range(0, size);
            if (!flag[points[i].x, points[i].y])
            {
                flag[points[i].x, points[i].y] = true;
                i++;
            }
        }
        return;
    }
}
