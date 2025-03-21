using UnityEngine;

[CreateAssetMenu(menuName = "TacticSystem/Action/Heal")]
public class HealAction : ActionType
{
    public override void Execute(Character user, Character target)
    {
        Debug.Log($"{user.stat.name} heals {target.stat.name} as {user.stat.damage}");
    }
}