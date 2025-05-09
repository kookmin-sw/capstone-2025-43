using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "TacticSystem/Action/DarkCircle")]
public class DarkCirlceAction : ActionType
{
    public override void Execute(Character user, List<Character> targets)
    {
        user.anim.PlayAttack(0);

        foreach (Character target in targets)
        {
            if (EffectPoolManager.Instance != null)
            {
                SummonDarkCircle(target);
            }

            float ApplyDamage = user.stat.damage + 10;
            target.ApplyDamage(ApplyDamage);
            target.tacticSystem.cooldownTimer += 2.5f;
        }
    }

    private void SummonDarkCircle(Character target)
    {
        Vector3 effectPosition = new Vector3(target.transform.position.x, target.transform.position.y + 0.1f, target.transform.position.z);
        GameObject effect = EffectPoolManager.Instance.GetEffect("DarkCircle", effectPosition);
        effect.GetComponent<PoolEffect>().SetStickGameObject(target.gameObject);
    }
}
