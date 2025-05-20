using UnityEngine;
using UnityEngine.UI;
public class LoadButton : MonoBehaviour
{
    [SerializeField]
    Text text;
    string path;
    public void Init(string path)
    {
        this.name = path;
        this.path = path;
        text.text = path;
    }
    public void onClick()
    {
        Managers.Data.handOverData = Managers.Data.Load<HandOverData>(path);
        Managers.Game.isNew = false;
        Managers.Game.StartGame();
    }
}
