using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "TacticSystem/Action/MeleeAttack")]
public class MeleeAttackAction : ActionType
{
    public override void Execute(Character user, List<Character> targets)
    {
        if (user == null || targets == null) return;

        TacticSystem tacticSystem = user.GetComponent<TacticSystem>();
        if (tacticSystem != null)
        {
            tacticSystem.StopcoolDown = true;
        }

        if (!user.TryGetComponent(out NavMeshAgent agent))
        {
            return;
        }
        agent.speed = user.stat.moveSpeed;
        agent.isStopped = false;

        foreach (Character target in targets)
        {
            user.StartCoroutine(MoveAndAction(user, target, agent, Attack));
        }
    }

    private void Attack(Character user, Character target)
    {
        //Debug.Log($"{user.name} attack {target.name} as {user.stat.damage} damage!");

        if (user.TryGetComponent(out Animator animator))
        {
            animator.SetTrigger("Attack");
        }

        target.ApplyDamage(user.stat.damage);
    }
}
