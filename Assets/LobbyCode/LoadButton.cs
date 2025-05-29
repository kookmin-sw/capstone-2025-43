using UnityEngine;
using UnityEngine.UI;
public class LoadButton : MonoBehaviour
{
    [SerializeField]
    Text text;
    string path;
    public void Init(string path , string fileName)
    {
        this.name = fileName;
        this.path = path;
        text.text = fileName;
    }
    public void onClick()
    {
        Managers.Data.handOverData = Managers.Data.Load<HandOverData>(path);
        Managers.Data.SetDictionary();
        Managers.Game.isNew = false;
        Managers.Game.gold = Managers.Data.handOverData.Gold;
        Managers.Game.StartGame();
    }
}
