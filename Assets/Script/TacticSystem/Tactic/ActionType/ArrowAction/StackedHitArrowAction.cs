using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MyProject.Utils;

[CreateAssetMenu(menuName = "TacticSystem/Action/StackedHitArrow")]
public class StackedHitArrowAction : ActionType
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
            user.StartTrackedCoroutine(MoveAndAction(user, target, user.agent, AttackWithStackedArrow));
        }
    }

    private void AttackWithStackedArrow(Character user, Character target)
    {
        if (user.TryGetComponent(out Animator animator))
        {
            animator.SetTrigger("Attack");
        }

        AudioManager.Instance.PlayEffect(SFXType.Attack);

        Transform firePoint = user.firePoint;
        if (firePoint == null) return;

        Vector3 direction = (target.transform.position - firePoint.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, rotation);

        StackedHitProjectile projectile = arrow.GetComponent<StackedHitProjectile>();
        if (projectile != null)
        {
            projectile.target = target.transform;
            projectile.speed = arrowSpeed;
            projectile.damage = user.stat.damage;
            projectile.attacker = user;
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
