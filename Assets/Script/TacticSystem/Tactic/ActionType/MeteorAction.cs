using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TacticSystem/Action/Meteor")]
public class MeteorAction : ActionType
{
    public float damage = 100f;
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
                MeteorRay(user, target);
            }
        }
    }

    private void MeteorRay(Character user ,Character target)
    {
        Vector3 effectPosition = new Vector3(target.transform.position.x, target.transform.position.y + 0.1f, target.transform.position.z);
        GameObject effect = EffectPoolManager.Instance.GetEffect("Meteor", effectPosition);
        VFXCollider col = effect.GetComponent<VFXCollider>();
        float ApplyDamage = user.stat.damage + damage;
        col.SetVFXOption(user,ApplyDamage,0.1f,1.0f);
    }
}
