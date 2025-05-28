using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "TacticSystem/Action/MagicShield")]
public class MagicShieldAction : ActionType
{
    public override void Execute(Character user, List<Character> targets)
    {
        user.anim.PlayAttack(2);
        //SingleTarget Setting
        foreach (Character target in targets)
        {
            target.EnableOneTimeShield();
        }
    }
}
