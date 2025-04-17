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

    public void Init(LocalInfo info)
    {
        // image <- localinfo.image
        desc.text = info.localData.desc;
        //battle wave -> gameobject(prefab) . tag -> parent ∞·¡§ 
    }

    public void CreateList()
    {

    }
}
