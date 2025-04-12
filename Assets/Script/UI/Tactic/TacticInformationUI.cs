using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TacticInformationUI : MonoBehaviour
{
    public GameObject tacticUI; // tacticSystem UI Panel
    public Button closeButton;
    private Character currentCharacter;
    [SerializeField] private TMP_Text GCDtext;
    [SerializeField] private TMP_Text[] tacticInformTexts; // TacticSlot Count is Always 6
    private TacticSystem tacticSystem;

    private void Awake()
    {
        closeButton.onClick.AddListener(CloseTacticUI);
    }
    void Update()
    {
        if(currentCharacter && tacticSystem)
        {
            GCDtext.SetText(string.Format("Global CoolDown : {0:F1}", tacticSystem.cooldownTimer));
            for(int i = 0; i < tacticSystem.TacticCapacity; ++i)
            {
                Tactic currentTactic = tacticSystem.tactics[i];
                if (currentTactic.targetType && currentTactic.conditionType && currentTactic.actionType)
                    tacticInformTexts[i].SetText(string.Format("{0} {1} {2}: {3:F1}", currentTactic.targetType.displayName, currentTactic.conditionType.displayName, currentTactic.actionType.displayName, currentTactic.cooldownTimer));
                else
                    tacticInformTexts[i].SetText(string.Format("Empty"));
            }
        }
    }

    public void InitializeUI(Character character)
    {
        currentCharacter = character;

        tacticSystem = currentCharacter.GetComponent<TacticSystem>();
        int tacticCapacity = tacticSystem.TacticCapacity;

        for (int i = 0; i < tacticInformTexts.Length; i++)
        {
            tacticInformTexts[i].gameObject.SetActive(i < tacticCapacity);
        }
    }


    public void CloseTacticUI()
    {
        tacticUI.SetActive(false);
    }
}
