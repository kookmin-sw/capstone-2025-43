using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(menuName = "TacticSystem/Tactic")]
public class Tactic : ScriptableObject
{
    public bool enable = true;
    public int priority = 0;
    public TargetType targetType;
    public ConditionType conditionType; 
    public ActionType actionType;
    public bool editable = true;
    public bool draggable = true;

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

    public Tactic Clone()
    {
        Tactic tactic = Instantiate(this); //Clone By Instantiate
        tactic.name = tactic.name.Replace("(Clone)", "").Trim();
        return tactic;
    }
}
