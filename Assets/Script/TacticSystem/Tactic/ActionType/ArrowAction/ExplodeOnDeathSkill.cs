using System.Collections.Generic;
using UnityEngine;
using MyProject.Utils;

[CreateAssetMenu(menuName = "TacticSystem/Action/ExplodeOnDeath")]
public class ExplodeOnDeathSkill : ActionType
{
    public float radius = 10f;
    public float damage = 50f;
    public GameObject bombIconPrefab;
    public GameObject explosionEffectPrefab;

    public override void Execute(Character user, List<Character> targets)
    {
        if (user == null || targets == null) return;

        if (user.TryGetComponent(out Animator animator))
            animator.SetTrigger("Attack");


        foreach (Character target in targets)
        {
            if (!user.IsEnemy(target)) continue;

            if (!target.TryGetComponent(out ExplodeOnDeath existing))
            {
                ExplodeOnDeath eod = target.gameObject.AddComponent<ExplodeOnDeath>();
                eod.radius = radius;
                eod.explosionDamage = damage;
                eod.owner = user;
                eod.explosionEffectPrefab = this.explosionEffectPrefab;
                eod.SetMark(bombIconPrefab);
            }
        }
    }
}
