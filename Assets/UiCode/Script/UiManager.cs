using UnityEngine;

public class UiManager : MonoBehaviour
{
    // todo manager ���� ����
    public static UiManager instance = null; // uimanager �̱���
    

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    
}
