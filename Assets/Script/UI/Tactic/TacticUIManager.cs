using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using static UnityEngine.Rendering.DebugUI.Table;
using Unity.VisualScripting;
using UnityEditor;
using System.Collections;

public class TacticUIManager : MonoBehaviour
{
    public static TacticUIManager Instance;

    public GameObject tacticUI; // tacticSystem UI Panel
    public GameObject tacticinformationUI; // tacticSystem UI Panel
    public GameObject resultUI; //Result UI Panel
    public GameObject characterInfoUI; //CharacterInfo UI Panel
    private Character currentCharacter;
    private void Awake()
    {
        Instance = this;
    }
    public void OpenCharacterInfoUI()
    {
        characterInfoUI.GetComponent<CharacterInfoUI>().OpenUI();
    }

    public void OpenTacticSystemUI(Character character)
    {
        tacticUI.GetComponent<TacticSystemUI>().OpenTacticUI(character);
        currentCharacter = character;
    }
    public void InitializeTacticInformationUI()
    {
        tacticinformationUI.GetComponent<TacticInformationUI>().InitializeUI(currentCharacter);
    }

    public void OpenResultUI(bool isWin)
    {
        StartCoroutine(DelayedResultUIOpen(isWin));
    }

    //TODO (Optional) :: Refactoring TacticUI's Structure to Function Extensibility 
    private IEnumerator DelayedResultUIOpen(bool isWin)
    {
        yield return new WaitForSeconds(3f);
        resultUI.GetComponent<ResultUI>().OpenUI(isWin);
    }

}
