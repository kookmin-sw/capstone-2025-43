using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "TacticSystem/Action/DarkCircle")]
public class DarkCirlceAction : ActionType
{
    public float addGCDAmount = 2.5f;
    public override void Execute(Character user, List<Character> targets)
    {
        user.anim.PlayAttack(0);

        foreach (Character target in targets)
        {
            if (EffectPoolManager.Instance != null)
            {
                SummonDarkCircle(target);
            }

            float ApplyDamage = user.stat.damage + amount;
            target.ApplyDamage(ApplyDamage);
            target.tacticSystem.cooldownTimer += addGCDAmount;
        }
    }

    private void SummonDarkCircle(Character target)
    {
        Vector3 effectPosition = new Vector3(target.transform.position.x, target.transform.position.y + 0.1f, target.transform.position.z);
        GameObject effect = EffectPoolManager.Instance.GetEffect("DarkCircle", effectPosition);
        effect.GetComponent<PoolEffect>().SetStickGameObject(target.gameObject);
    }
}
