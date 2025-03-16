using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TacticSystem/Condition/Nearest")]
public class NearestCondition : ConditionType
{
    public override List<Character> Filter(List<Character> targets, Character self)
    {
        if (targets.Count == 0) return new List<Character>();

        Character nearest = null;
        float minDistance = float.MaxValue;

        foreach (Character c in targets)
        {
            float distance = Vector3.Distance(self.transform.position, c.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = c;
            }
        }

        return nearest != null ? new List<Character> { nearest } : new List<Character>();
    }
}
