using UnityEngine;

public class UiEvent : MonoBehaviour
{
    public string targetName;
    public bool active;

    private void OnMouseDown() // object ��������
    {
        onClick();
    }

    public void onClick() // ��ư ui ��������
    {
        Debug.Log("�������ϴ�");
        GameManager.instance.uiManager.SetUiCondition(targetName, active);
    }
}
