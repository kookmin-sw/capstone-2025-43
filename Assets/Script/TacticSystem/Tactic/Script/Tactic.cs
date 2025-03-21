using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(menuName = "TacticSystem/Tactic")]
public class Tactic : ScriptableObject
{
    public bool Enable = true;
    public int Priority = 0;
    [SerializeField] private TargetType targetType;
    [SerializeField] private ConditionType conditionType; 
    [SerializeField] private ActionType actionType; 

    public bool Execute(Character self)
    {
        if (self == null || targetType == null || conditionType == null ||actionType == null)
            return false;

        List<Character> targets = CharacterManager.Instance.GetCharacters(self, targetType, conditionType, actionType);
        foreach (Character target in targets)
        {
            actionType.Execute(self, target);
        }
        return true;
    }
}
