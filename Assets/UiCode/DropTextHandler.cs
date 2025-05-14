using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
public class DropTextHandler : MonoBehaviour
{
    [SerializeField]
    Text text;
    int cur = 0, min = 0, max = 0;

    public void Init(int max, int min)
    {
        this.min = min;
        this.max = max;
        UpdateText();
    }

    public void UpdateText()
    {
        text.text = $"{cur} / {max}";
        if (cur < min || cur > max) text.color = Color.red;
        else text.color = Color.blue;
    }

    public void SetMax(int num)
    {
        max = num;
    }

    public void UpdateCur(int num)
    {
        cur += num;
        if (cur < 0)
            cur = 0;
    }
}
