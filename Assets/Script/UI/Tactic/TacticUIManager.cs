using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using static UnityEngine.Rendering.DebugUI.Table;
using Unity.VisualScripting;

public class TacticUIManager : MonoBehaviour
{
    public static TacticUIManager Instance;

    public GameObject tacticUI; // UI Panel
    [SerializeField] private GameObject[] TacticSlots; // Add TacticSlots
    public Button closeButton;
    public Button saveButton;

    private Character currentCharacter;

    private List<Tactic> tempTactics;//TempTacticList

    private void Awake()
    {
        Instance = this;
        closeButton.onClick.AddListener(CloseTacticUI);
        saveButton.onClick.AddListener(SaveTactics);
    }

    public void OpenTacticUI(Character character)
    {
        Debug.Log("Open TacticUI");
        tacticUI.SetActive(true);
        currentCharacter = character;
        TacticSystem tacticSystem = currentCharacter.GetComponent<TacticSystem>();
        tacticSystem.SortTactics();
        int tacticCapacity = tacticSystem.TacticCapacity;

        tempTactics = new List<Tactic>();
        foreach (Tactic tactic in tacticSystem.tactics)
        {
            tempTactics.Add(tactic.Clone());
        }

        for (int i = 0; i < TacticSlots.Length; i++)
        {
            TacticSlots[i].SetActive(i < tacticCapacity);
            if (i < tacticCapacity && currentCharacter != null)
            {
                InitializeUIs(TacticSlots[i], i);
            }
        }
    }

    private void InitializeUIs(GameObject TacticSlot, int index)
    {
        Transform rowUI = TacticSlot.transform.Find("RowUI");
        TMP_Dropdown targetDropdown = rowUI.Find("Dropdown_Target").GetComponent<TMP_Dropdown>();
        TMP_Dropdown conditionDropdown = rowUI.Find("Dropdown_Condition").GetComponent<TMP_Dropdown>();
        TMP_Dropdown actionDropdown = rowUI.Find("Dropdown_Action").GetComponent<TMP_Dropdown>();
        Toggle enableToggle = rowUI.Find("EnableToggle").GetComponent<Toggle>();

        targetDropdown.onValueChanged.RemoveAllListeners();
        conditionDropdown.onValueChanged.RemoveAllListeners();
        actionDropdown.onValueChanged.RemoveAllListeners();
        enableToggle.onValueChanged.RemoveAllListeners();

        Tactic currentTactic = tempTactics[index];
        draggableTactic item = rowUI.GetComponent<draggableTactic>();
        if (item != null)
            item.tactic = currentTactic;
        SetDropdownOptions(targetDropdown, currentCharacter.Targets);
        SetDropdownOptions(conditionDropdown, currentCharacter.Conditions);
        SetDropdownOptions(actionDropdown, currentCharacter.Actions);

        targetDropdown.value = (currentTactic.targetType != null) ?
                               currentCharacter.Targets.IndexOf(currentTactic.targetType) + 1 : 0;

        conditionDropdown.value = (currentTactic.conditionType != null) ?
                                  currentCharacter.Conditions.IndexOf(currentTactic.conditionType) + 1 : 0;

        actionDropdown.value = (currentTactic.actionType != null) ?
                               currentCharacter.Actions.IndexOf(currentTactic.actionType) + 1 : 0;
        enableToggle.isOn = currentTactic.Enable;

        targetDropdown.onValueChanged.AddListener(delegate { UpdateTempTactic(index); });
        conditionDropdown.onValueChanged.AddListener(delegate { UpdateTempTactic(index); });
        actionDropdown.onValueChanged.AddListener(delegate { UpdateTempTactic(index); });
        enableToggle.onValueChanged.AddListener(delegate { UpdateTempTactic(index); });
    }

    private void SetDropdownOptions<T>(TMP_Dropdown dropdown, List<T> options)
    {
        dropdown.ClearOptions();
        List<string> optionNames = new List<string> { "Null" }; 

        if (options != null && options.Count > 0)
        {
            optionNames.AddRange(options.ConvertAll(option => option.ToString())); 
        }

        dropdown.AddOptions(optionNames);
    }

    private void UpdateTempTactic(int index)
    {
        Transform rowUI = TacticSlots[index].transform.Find("RowUI");
        TMP_Dropdown targetDropdown = rowUI.Find("Dropdown_Target").GetComponent<TMP_Dropdown>();
        TMP_Dropdown conditionDropdown = rowUI.Find("Dropdown_Condition").GetComponent<TMP_Dropdown>();
        TMP_Dropdown actionDropdown = rowUI.Find("Dropdown_Action").GetComponent<TMP_Dropdown>();
        Toggle enableToggle = rowUI.Find("EnableToggle").GetComponent<Toggle>();


        TargetType newTarget = targetDropdown.value == 0 ? null : currentCharacter.Targets[targetDropdown.value - 1];
        ConditionType newCondition = conditionDropdown.value == 0 ? null : currentCharacter.Conditions[conditionDropdown.value - 1];
        ActionType newAction = actionDropdown.value == 0 ? null : currentCharacter.Actions[actionDropdown.value - 1];
        bool newEnalbeState = enableToggle.isOn;

        Tactic tempTactic = tempTactics[index];
        tempTactic.targetType = newTarget;
        tempTactic.conditionType = newCondition;
        tempTactic.actionType = newAction;
        tempTactic.Enable = newEnalbeState;

    }

    public void SaveTactics()
    {
        TacticSystem tacticSystem = currentCharacter.GetComponent<TacticSystem>();
        tacticSystem.tactics = new List<Tactic>(tempTactics);
        tacticSystem.SortTactics();
        Debug.Log("Tactics saved!");
        CloseTacticUI();
    }

    public void CloseTacticUI()
    {
        tacticUI.SetActive(false);
    }

    public Character GetCurrentCharacter()
    {
        return currentCharacter;
    }

}
