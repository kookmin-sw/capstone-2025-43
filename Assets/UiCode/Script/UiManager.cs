using UnityEngine;

public class UiManager : MonoBehaviour
{
    // todo manager ¿¡¼­ °ü¸®
    public static UiManager instance = null; // uimanager ½Ì±ÛÅæ
    

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    
}
