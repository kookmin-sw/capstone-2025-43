using UnityEngine;

public class UiEvent : MonoBehaviour
{
    public string targetName;
    public bool active;

    public void OnMouseDown()
    {
        Debug.Log("�������ϴ�");
        UiManager.instance.SetUiCondition(targetName, active);
    }
}
