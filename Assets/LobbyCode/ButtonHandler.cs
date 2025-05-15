using Unity.VisualScripting;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public void newGame()
    {
        Managers.Game.StartGame();
        Managers.Game.isNew = true;
    }
    public void loadGame()
    {
        Managers.Game.StartGame();
        Managers.Game.isNew = false;
    }
    public void quitGame()
    {
        // endgame
    }
}
