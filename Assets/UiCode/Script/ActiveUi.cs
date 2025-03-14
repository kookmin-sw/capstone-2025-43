using UnityEngine;

public class ActiveUi : MonoBehaviour
{
    public string uiName;
    public bool condition;
    private void OnMouseDown()
    {
        UiManager.instance.SetUiActive(uiName, condition);
    }
}
