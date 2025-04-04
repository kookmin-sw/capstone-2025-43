using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using static UnityEngine.Rendering.DebugUI.Table;
using Unity.VisualScripting;
using UnityEditor;

public class TacticUIManager : MonoBehaviour
{
    public static TacticUIManager Instance;

    public GameObject tacticUI; // tacticSystem UI Panel
    public GameObject tacticinformationUI; // tacticSystem UI Panel
    [SerializeField] private GameObject[] TacticSlots; // TacticSlot Count is Always 6

    private Character currentCharacter;
    private void Awake()
    {
        Instance = this;
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


}
