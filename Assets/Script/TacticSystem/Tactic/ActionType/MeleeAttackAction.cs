using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "TacticSystem/Action/MeleeAttack")]
public class MeleeAttackAction : ActionType
{
    TacticSystem tacticSystem;
    Animator animator;

    public override void Execute(Character user, Character target)
    {
        if (user == null || target == null) return;

        tacticSystem = user.GetComponent<TacticSystem>();
        if (tacticSystem != null)
        {
            tacticSystem.StopcoolDown = true;
        }

        if (!user.TryGetComponent(out NavMeshAgent agent))
        {
            return;
        }
        animator = user.GetComponent<Animator>();

        agent.speed = user.stat.moveSpeed;
        agent.isStopped = false;
        user.StartCoroutine(MoveAndAttack(user, target, agent));
    }

    private System.Collections.IEnumerator MoveAndAttack(Character user, Character target, NavMeshAgent agent)
    {
        agent.stoppingDistance = user.AttackRange;
        agent.isStopped = false;
        agent.SetDestination(target.transform.position);
        while (true)
        {
            float distance = Vector3.Distance(user.transform.position, target.transform.position);

            if (distance <= user.stat.attackRange)
            {
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                agent.updateRotation = false;
                Attack(user, target);
                break;
            }
            yield return null;
        }

        if (tacticSystem)
        {
            animator.SetBool("IsWalk", false);
            tacticSystem.StopcoolDown = false;
            agent.updateRotation = true;
        }
    }

    private void LookAtTarget(Character user, Character target)
    {
        if (user == null || target == null) return;

        Vector3 direction = target.transform.position - user.transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            user.transform.rotation = Quaternion.RotateTowards(user.transform.rotation, targetRotation, user.stat.rotationSpeed * Time.deltaTime);
        }
    }


    private void Attack(Character user, Character target)
    {
        Debug.Log($"{user.name} attack {target.name} as {user.stat.damage} damage!");

        //Animation Trigger
        if (user.TryGetComponent(out Animator animator))
        {
            animator.SetTrigger("Attack");
        }

        target.ApplyDamage(user.stat.damage);
    }
}
