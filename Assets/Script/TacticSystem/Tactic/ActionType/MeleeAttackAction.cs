using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MyProject.Utils;  // SFX

[CreateAssetMenu(menuName = "TacticSystem/Action/MeleeAttack")]
public class MeleeAttackAction : ActionType
{
    public override void Execute(Character user, List<Character> targets)
    {
        if (user == null || targets == null) return;
        TacticSystem tacticSystem = user.tacticSystem;
        if (tacticSystem != null)
        {
            tacticSystem.stopCooldown = true;
        }
        foreach (Character target in targets)
        {
            user.StartTrackedCoroutine(MoveAndAction(user, target, user.agent, Attack));
        }
    }

    private void Attack(Character user, Character target)
    {
        if (user.TryGetComponent(out Animator animator))
        {
            animator.SetTrigger("Attack");
        }
        if(AudioManager.Instance)
            AudioManager.Instance.PlayEffect(SFXType.Attack); // SFX
        target.ApplyDamage(user.stat.damage);
    }
}
