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

    public bool stopcoolDown = false;
    [HideInInspector] public float cooldownTimer = 0f;

    public bool Execute(Character self)
    {
        if (self == null || targetType == null || conditionType == null ||actionType == null)
            return false;

        List<Character> targets = CharacterManager.Instance.GetCharacters(self, targetType, conditionType, actionType);
        if (targets.Count == 0)
            return false;
 
        actionType.Execute(self, targets);
       
        return true;
    }

    public Tactic Clone()
    {
        Tactic clone = Instantiate(this); //Clone By Instantiate
        clone.name = clone.name.Replace("(Clone)", "").Trim();
        clone.stopcoolDown = this.stopcoolDown;
        clone.cooldownTimer = this.cooldownTimer;
        return clone;
    }

    public void ApplyCoolDown()
    {
        stopcoolDown = false;
        if(actionType)
            cooldownTimer = actionType.actionCooldown;
    }
}
