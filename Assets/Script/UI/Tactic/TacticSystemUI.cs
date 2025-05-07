using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using static UnityEngine.Rendering.DebugUI.Table;
using Unity.VisualScripting;
using UnityEditor;

public class TacticSystemUI : MonoBehaviour
{
    public GameObject tacticUI; // tacticSystem UI Panel
    [SerializeField] private GameObject[] TacticSlots; // TacticSlot Count is Always 6
    public Button closeButton;
    public Button saveButton;
    public TMP_Text nameText;

    private Character currentCharacter;

    private List<Tactic> tempTactics;//TempTacticList

    private void Awake()
    {
        closeButton.onClick.AddListener(CloseTacticUI);
        saveButton.onClick.AddListener(SaveTactics);
    }

    public void OpenTacticUI(Character character)
    {
        Time.timeScale = 0f; // game pause

        tacticUI.SetActive(true);
        currentCharacter = character;
        TacticSystem tacticSystem = currentCharacter.GetComponent<TacticSystem>();
        nameText.SetText(currentCharacter.DisplayName);
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
            if (i < tacticCapacity)
                InitializeRowUI(TacticSlots[i], i);
        }
    }

    private void InitializeRowUI(GameObject TacticSlot, int index)
    {
        Transform rowUI = TacticSlot.transform.Find("RowUI");
        Tactic currentTactic = tempTactics[index];
        draggableTactic item = rowUI.GetComponent<draggableTactic>();
        if (item != null)
            item.tactic = currentTactic;

        TMP_Dropdown targetDropdown = rowUI.Find("Dropdown_Target").GetComponent<TMP_Dropdown>();
        TMP_Dropdown conditionDropdown = rowUI.Find("Dropdown_Condition").GetComponent<TMP_Dropdown>();
        TMP_Dropdown actionDropdown = rowUI.Find("Dropdown_Action").GetComponent<TMP_Dropdown>();
        Toggle enableToggle = rowUI.Find("EnableToggle").GetComponent<Toggle>();

        targetDropdown.onValueChanged.RemoveAllListeners();
        conditionDropdown.onValueChanged.RemoveAllListeners();
        actionDropdown.onValueChanged.RemoveAllListeners();
        enableToggle.onValueChanged.RemoveAllListeners();

        SetDropdownOptions(targetDropdown, currentCharacter.Targets);
        SetDropdownOptions(conditionDropdown, currentCharacter.Conditions);
        SetDropdownOptions(actionDropdown, currentCharacter.Actions);

        targetDropdown.value = (currentTactic.targetType != null) ?
                               currentCharacter.Targets.IndexOf(currentTactic.targetType) + 1 : 0;
        conditionDropdown.value = (currentTactic.conditionType != null) ?
                                  currentCharacter.Conditions.IndexOf(currentTactic.conditionType) + 1 : 0;
        actionDropdown.value = (currentTactic.actionType != null) ?
                               currentCharacter.Actions.IndexOf(currentTactic.actionType) + 1 : 0;
        enableToggle.isOn = currentTactic.enable;

        SetRowUIInteractable(currentTactic.editable, targetDropdown, conditionDropdown, actionDropdown, enableToggle);

        targetDropdown.onValueChanged.AddListener(delegate { UpdateTargetDropdown(currentTactic, targetDropdown); });
        conditionDropdown.onValueChanged.AddListener(delegate { UpdateConditionDropdown(currentTactic, conditionDropdown); });
        actionDropdown.onValueChanged.AddListener(delegate { UpdateActionDropdown(currentTactic, actionDropdown); });
        enableToggle.onValueChanged.AddListener(delegate { UpdateToggle(currentTactic, enableToggle); });
    }

    private void SetDropdownOptions<T>(TMP_Dropdown dropdown, List<T> options)
    {
        dropdown.ClearOptions();
        List<string> optionNames = new List<string> { "Empty" };

        if (options != null && options.Count > 0)
        {
            optionNames.AddRange(options.ConvertAll(option => option.ToString()));
        }
        dropdown.AddOptions(optionNames);
    }
    private void UpdateTargetDropdown(Tactic currentTactic, TMP_Dropdown targetDropdown)
    {
        TargetType newTarget = targetDropdown.value == 0 ? null : currentCharacter.Targets[targetDropdown.value - 1];
        currentTactic.targetType = newTarget;
    }
    private void UpdateConditionDropdown(Tactic currentTactic, TMP_Dropdown conditionDropdown)
    {
        ConditionType newCondition = conditionDropdown.value == 0 ? null : currentCharacter.Conditions[conditionDropdown.value - 1];
        currentTactic.conditionType = newCondition;
    }
    private void UpdateActionDropdown(Tactic currentTactic, TMP_Dropdown actionDropdown)
    {
        ActionType newAction = actionDropdown.value == 0 ? null : currentCharacter.Actions[actionDropdown.value - 1];
        currentTactic.actionType = newAction;
        currentTactic.ApplyCoolDown();
    }
    private void UpdateToggle(Tactic currentTactic, Toggle enableToggle)
    {
        bool newEnalbeState = enableToggle.isOn;
        currentTactic.enable = newEnalbeState;
    }

    public void SaveTactics()
    {
        TacticSystem tacticSystem = currentCharacter.GetComponent<TacticSystem>();
        tacticSystem.tactics = new List<Tactic>(tempTactics);
        tacticSystem.SortTactics();
        //Debug.Log("Tactics saved!");
        CloseTacticUI();
    }

    public void CloseTacticUI()
    {
        Time.timeScale = 1f;
        tacticUI.SetActive(false);
    }
    public void SetRowUIInteractable(bool value, TMP_Dropdown targetDropdown, TMP_Dropdown conditionDropdown, TMP_Dropdown actionDropdown, Toggle enableToggle)
    {
        targetDropdown.interactable = value;
        conditionDropdown.interactable = value;
        actionDropdown.interactable = value;
        enableToggle.interactable = value;
    }
    public Character GetCurrentCharacter()
    {
        return currentCharacter;
    }

}
