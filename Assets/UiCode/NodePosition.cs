using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class NodePosition
{
    public const int size = 10;
    public bool[,] flag = new bool[size * 2 + 1, size * 2 + 1];
    public Vector2 CreateRandomSpot()
    {
        Debug.Log("·»´ýÁß");
        int x, z;
        do
        {
            x = Random.Range(-size, size);
            z = Random.Range(-size, size);

            if (!flag[x + size, z + size])
            {
                Debug.Log("·»´ý¿Ï·á");
                flag[x + size, z + size] = true;
                return new Vector2(x, z);
            }
        } while (true);
    }

    
}
