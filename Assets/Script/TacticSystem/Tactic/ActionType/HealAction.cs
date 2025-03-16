using UnityEngine;

[CreateAssetMenu(menuName = "TacticSystem/Action/Heal")]
public class HealAction : TacticAction
{
    public override void Execute(Character user, Character target)
    {
        Debug.Log($"{user.name}�� {target.name}��(��) {user.damage}��ŭ ġ����!");
    }
}