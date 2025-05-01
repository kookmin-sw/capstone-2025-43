using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "TacticSystem/Action/Lightning")]
public class LightningAttackAction : ActionType
{
    public override void Execute(Character user, List<Character> targets)
    {
        // Apply Animation TODO :: Change Animation Trigger
        if (user.TryGetComponent(out Animator animator))
        {
            animator.SetTrigger("Attack"); 
            //animtor.SetTrigger("Spell");
        }
        foreach (Character target in targets)
        {
            if (EffectPoolManager.Instance != null)
            {
                LightningRay(target);
                LightningPlane(target);
            }

            float ApplyDamage = user.stat.damage + 10;
            target.ApplyDamage(ApplyDamage);
        }
    }

    private void LightningPlane(Character target)
    {
        Vector3 effectPosition = new Vector3(target.transform.position.x, target.transform.position.y + 0.1f, target.transform.position.z);
        GameObject effect = EffectPoolManager.Instance.GetEffect("LightningPlane", effectPosition);
        effect.GetComponent<PoolEffect>().SetStickGameObject(target.gameObject);
    }
    private void LightningRay(Character target)
    {
        Vector3 effectPosition = new Vector3(target.transform.position.x, target.transform.position.y + 3f, target.transform.position.z);
        GameObject effect = EffectPoolManager.Instance.GetEffect("LightningRay", effectPosition);
        effect.GetComponent<PoolEffect>().SetStickGameObject(target.gameObject);
    }
}
