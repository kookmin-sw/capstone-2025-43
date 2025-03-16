using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TacticSystem/Condition/Mixed")]
public class MixedConditions : ConditionType
{
    [SerializeField] private List<ConditionType> conditions;

    public override List<Character> Filter(List<Character> targets, Character self)
    {
        List<Character> filtered = new List<Character>(targets);

        foreach (ConditionType condition in conditions)
        {
            filtered = condition.Filter(filtered, self);
        }

        return filtered;
    }
}
