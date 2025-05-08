using UnityEngine;
using UnityEngine.UI;

public class PositionGrid : MonoBehaviour
{
    public string[] positionData = new string[9];
    [SerializeField]
    Image startButtonimage;
    int heroCount;

    public string[] GetPosition()
    {
        for (int i = 0; i < 9; i++)
        {
            positionData[i] = transform.GetChild(i).name;
        }
        return positionData;
    }

    void HeroCounting()
    {
        heroCount = 0;
        for (int i = 0; i < 9; i++)
        {
            if (positionData[i] != null)
                heroCount++;
        }
    }

    void SetButtonState()
    {
        HeroCounting();
        if (heroCount >= 1 && heroCount <= 4)
            startButtonimage = Managers.Resource.Load<Image>("Images/button2_ready_on");
        else
            startButtonimage = Managers.Resource.Load<Image>("Images/button2_ready_off");
    }

}