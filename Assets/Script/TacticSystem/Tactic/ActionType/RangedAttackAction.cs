using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MyProject.Utils;

[CreateAssetMenu(menuName = "TacticSystem/Action/RangedAttack")]
public class RangedAttackAction : ActionType
{
    public GameObject arrowPrefab;
    public float arrowSpeed = 20f;

    public override void Execute(Character user, List<Character> targets)
    {
        if (user == null || targets == null || arrowPrefab == null) return;

        TacticSystem tacticSystem = user.tacticSystem;
        if (tacticSystem != null)
        {
            tacticSystem.stopCooldown = true;
        }

        foreach (Character target in targets)
        {
            user.StartTrackedCoroutine(MoveAndAction(user, target, user.agent, AttackWithArrow));
        }
    }

    private void AttackWithArrow(Character user, Character target)
    {
        if (user.TryGetComponent(out Animator animator))
        {
            animator.SetTrigger("Attack");
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayEffect(SFXType.Attack);
        }

        Transform firePoint = user.firePoint;
        if (firePoint == null) return;

        Vector3 direction = (target.transform.position - firePoint.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, rotation);

        Projectile projectile = arrow.GetComponent<Projectile>();
        if (projectile != null)
            {
            projectile.Initialize(target.torso, arrowSpeed, user.stat.damage, user);
            }
            
        Collider arrowCol = arrow.GetComponent<Collider>();
        Collider[] allColliders = Physics.OverlapSphere(arrow.transform.position, 5f);
        foreach (var col in allColliders)
        {
            Character character = col.GetComponent<Character>();
            if (character != null && !user.IsEnemy(character) && arrowCol != null)
            {
                Physics.IgnoreCollision(arrowCol, col);
            }
        }
    }
}

