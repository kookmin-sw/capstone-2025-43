using UnityEngine;

[CreateAssetMenu(menuName = "TacticSystem/Action/Heal")]
public class HealAction : TacticAction
{
    public override void Execute(Character user, Character target)
    {
        Debug.Log($"{user.name}이 {target.name}을(를) {user.damage}만큼 치유함!");
    }
}