using UnityEngine;
using UnityEngine.UI;

public class PositionGrid : MonoBehaviour
{
    [SerializeField]
    Image buttonimage;
    int heroCount;
    Transform[] slots;





    void HeroCounting()
    {
        heroCount = 0;
        for (int i = 0; i < 9; i++)
        {
            if (slots[i].childCount > 0)
                heroCount++;
        }
    }
    
    void SetButtonState()
    {
        HeroCounting();
        if (heroCount >= 1 && heroCount <= 4)
            buttonimage = Managers.Resource.Load<Image>("Images/button2_ready_on");
        else
            buttonimage = Managers.Resource.Load<Image>("Images/button2_ready_off");
    }

}