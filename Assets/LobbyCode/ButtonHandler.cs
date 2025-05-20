using System;
using System.IO;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public GameObject load;
    public GameObject option;
    private string savePath => Application.persistentDataPath;

    public void newGame()
    {
        Managers.Game.isNew = true;
        Managers.Game.StartGame();
    }

    public void loadGame()
    {

        string[] files = Directory.GetFiles(savePath, "*.json");
        foreach (string file in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(file);
            GameObject slot = Managers.Resource.Instantiate("LoadButton", load.transform);
            slot.name = fileName;
            slot.GetComponent<LoadButton>().Init(fileName);
        }

        load.SetActive(true);
    }
    public void saveGame()
    {
        // 게임 상태 설정
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd-HHmm");
        string filename = $"{timestamp}.json";
        string path = Path.Combine(Application.persistentDataPath, filename);
        Managers.Data.Save<HandOverData>(path, Managers.Data.handOverData);
    }
    public void closeUi(string name)
    {
        switch (name)
        {
            case "Load":
                load.SetActive (false);
                this.gameObject.SetActive (true);
                break;
            case "Option":
                option.SetActive (false);
                this.gameObject.SetActive(true);
                break;
        }
    }

    public void quitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
