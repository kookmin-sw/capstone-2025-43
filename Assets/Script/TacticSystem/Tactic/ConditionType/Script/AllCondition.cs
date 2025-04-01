using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TacticSystem/Condition/AllCondition")]
public class AllCondition : ConditionType
{
    public override List<Character> Filter(List<Character> targets, Character self)
    {
        return targets;
    }
}
