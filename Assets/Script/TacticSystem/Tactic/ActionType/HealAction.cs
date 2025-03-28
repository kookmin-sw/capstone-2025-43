using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "TacticSystem/Action/Heal")]
public class HealAction : ActionType
{
    public override void Execute(Character user, List<Character> targets )
    {
        foreach ( Character target in targets )
        {
            Debug.Log($"{user.stat.name} heals {target.stat.name} as {user.stat.damage}");
        }
    }
}
