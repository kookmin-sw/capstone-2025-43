using UnityEngine;

public class PositionGrid : MonoBehaviour
{
    public string[] positionData = new string[9];
    public string[] GetPosition()
    {
        for (int i = 0; i < 9; i++)
        {
            positionData[i] = transform.GetChild(i).name;
        }
        return positionData;
    }
}