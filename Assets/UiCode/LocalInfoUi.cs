using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LocalInfoUi : MonoBehaviour
{
    public Image image;
    public Text desc;
    public Transform creepList;
    public Transform boss;
    public LocalInfo info;
    public void SetLocalUi()
    {
        Vector2 local = Managers.Data.handOverData.openLocal;
        info = Managers.Data.handOverData.localInfos[local];

        desc.text = info.localData.desc;
        image.sprite = info.localData.image;


    }
}
