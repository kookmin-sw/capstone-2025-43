using UnityEngine;

public class NodePosition : MonoBehaviour
{
    public struct Point
    {
        public int x, y;
    };
    public const int size = 15;
    public Point[] points = new Point[size * 2];
    private bool[,] flag = new bool[size * 2 + 1, size * 2 + 1];
    public void CreateRandomSpot()
    {
        Debug.Log("·»´ýÁß");
        for (int i = 0; i < size*2;)
        {
            points[i].x = Random.Range(-size, size);
            points[i].y = Random.Range(-size, size);
            if (!flag[points[i].x + size, points[i].y +size])
            {
                flag[points[i].x + size, points[i].y + size] = true;
                i++;
            }
        }
        Debug.Log("·»´ý¿Ï·á");
        return;
    }
}
