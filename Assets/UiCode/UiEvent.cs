using UnityEngine;

public class UiEvent : MonoBehaviour
{
    public string targetName;
    public bool active;

    private void OnMouseDown() // object ´­·¶À»¶§
    {
        onClick();
    }

    public void onClick() // ¹öÆ° ui ´­·¶À»¶§
    {
        Debug.Log("´­·¶½À´Ï´Ù");
        GameManager.instance.uiManager.SetUiCondition(targetName, active);
    }
}
