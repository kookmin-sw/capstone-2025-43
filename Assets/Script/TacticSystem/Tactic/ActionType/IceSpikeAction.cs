using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TacticSystem/Action/IceSpike")]
public class IceSpikeAction : ActionType
{
    public override void Execute(Character user, List<Character> targets)
    {
        // Apply Animation TODO :: Change Animation Trigger
        if (user.TryGetComponent(out Animator animator))
        {
            user.anim.PlayAttack(2);
        }
        foreach (Character target in targets)
        {
            if (EffectPoolManager.Instance != null)
            {
                IceSpike(user, target);
            }

            if(target != user)
            {
                target.ApplyDamage(user.stat.damage);
            }
        }
    }

    private void IceSpike(Character user, Character target)
    {
        Vector3 effectPosition = new Vector3(target.transform.position.x, target.transform.position.y + 0.1f, target.transform.position.z);
        GameObject effect = EffectPoolManager.Instance.GetEffect("IceSpike", effectPosition);
        VFXCollider col = effect.GetComponent<VFXCollider>();
        float ApplyDamage = user.stat.damage + amount;
        col.SetVFXOption(user, ApplyDamage, 0.1f, 1.0f);
    }
}
