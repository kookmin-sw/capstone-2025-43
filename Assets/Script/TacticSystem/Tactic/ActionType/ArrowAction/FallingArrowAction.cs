using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MyProject.Utils;

[CreateAssetMenu(menuName = "TacticSystem/Action/FallingArrow")]
public class FallingArrowAction : ActionType
{
    public GameObject arrowPrefab;
    public float fallHeight = 50f;
    public float fallSpeed = 30f;

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
            user.StartTrackedCoroutine(MoveAndAction(user, target, user.agent, AttackWithFallingArrow));
        }
    }

    private void AttackWithFallingArrow(Character user, Character target)
    {
        if (user.TryGetComponent(out Animator animator))
        {
            animator.SetTrigger("Attack");
        }

        AudioManager.Instance.PlayEffect(SFXType.Attack);

        Vector3 spawnPosition = target.transform.position + Vector3.up * fallHeight;
        Quaternion rotation = Quaternion.LookRotation(Vector3.down);
        GameObject arrow = Instantiate(arrowPrefab, spawnPosition, rotation);

        FallingProjectile projectile = arrow.GetComponent<FallingProjectile>();
        if (projectile != null)
        {
            projectile.target = target.transform;
            projectile.fallSpeed = fallSpeed;
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

