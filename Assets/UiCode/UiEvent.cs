using UnityEngine;

public class UiEvent : MonoBehaviour
{
    public string targetName;
    public bool active;

    public void OnMouseDown()
    {
        Debug.Log("´­·¶½À´Ï´Ù");
        UiManager.instance.SetUiCondition(targetName, active);
    }
}
