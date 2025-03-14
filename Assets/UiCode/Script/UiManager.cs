using UnityEngine;

public class UiManager : MonoBehaviour
{
    // todo manager ���� ����
    public static UiManager instance = null; // uimanager �̱���
    public GameObject mapUi;
    public GameObject shopUi;
    public GameObject nodeUi;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void SetUiActive(string name , bool condition)
    {
        switch (name)
        {
            case "Map":
                mapUi.SetActive(condition);
                break;
            case "Shop":
                break;
            case "Node":
                nodeUi.SetActive(condition);
                break;
        }
    }
    
}
