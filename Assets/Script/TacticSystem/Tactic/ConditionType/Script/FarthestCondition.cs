using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TacticSystem/Condition/Farthest")]
public class FarthestCondition : ConditionType
{
    public override List<Character> Filter(List<Character> targets, Character self)
    {
        if (targets.Count == 0) return new List<Character>();

        Character farthest = null;
        float maxDistance = float.MinValue;

        foreach (Character c in targets)
        {
            float distance = Vector3.Distance(self.transform.position, c.transform.position);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                farthest = c;
            }
        }

        return farthest != null ? new List<Character> { farthest } : new List<Character>();
    }
}
