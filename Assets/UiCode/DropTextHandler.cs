using UnityEngine;
using UnityEngine.UI;
public class DropTextHandler : MonoBehaviour
{
    [SerializeField]
    Text text;
    int cur = 0, min = 0, max = 0;

    void UpdateText()
    {
        if (min == -1)
            text.text = $"{max}";
        else
        {
            text.text = $"{cur} / {max}";
            if (Able()) text.color = Color.blue;
            else text.color = Color.red;
        }
    }

    public void Init(int max, int min)
    {
        this.min = min;
        this.max = max;
        UpdateText();
    }
    public bool Able()
    {
        return cur <= max && cur >= min;
    }
    public void UpdateMax(int num)
    {
        max = num;
        UpdateText();
    }

    public void UpdateCur(int num)
    {
        cur = num;
        UpdateText();
    }
}
