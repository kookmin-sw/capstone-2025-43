using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "TacticSystem/Action/Heal")]
public class HealAction : ActionType
{
    public override void Execute(Character user, List<Character> targets)
    {
        // Apply Animation TODO :: Change Animation Trigger
        if (user.TryGetComponent(out Animator animator))
        {
            animator.SetTrigger("Attack"); //animtor.SetTrigger("Spell");
        }
        foreach (Character target in targets)
        {
            if (EffectPoolManager.Instance != null)
            {
                Vector3 vfxPosition = new Vector3(target.transform.position.x, target.transform.position.y + 0.1f, target.transform.position.z);
                GameObject effect = EffectPoolManager.Instance.GetEffect("HealEffect", vfxPosition);
                effect.GetComponent<PoolEffect>().SetStickGameObject(target.gameObject);
            }

            Debug.Log($"{user.stat.name} heals {target.stat.name} as {user.stat.damage}");
            target.AddHP(user.stat.damage);
        }
    }
}
