using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "TacticSystem/Action/MeleeAttack")]
public class MeleeAttackAction : ActionType
{
    TacticSystem tacticSystem;
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

        agent.speed = user.stat.moveSpeed;
        agent.isStopped = false;
        user.StartCoroutine(MoveAndAttack(user, target, agent));
    }

    private System.Collections.IEnumerator MoveAndAttack(Character user, Character target, NavMeshAgent agent)
    {
        while (true)
        {
            float distance = Vector3.Distance(user.transform.position, target.transform.position);

            LookAtTarget(user, target);

            if (distance <= user.stat.attackRange)
            {
                agent.isStopped = true;
                Attack(user, target);
                break;
            }

            if (user.TryGetComponent(out Animator animator)){
                animator.SetBool("IsWalk", true);
            }
            agent.SetDestination(target.transform.position);
            
            yield return null;
        }

        TacticSystem tacticSystem = user.GetComponent<TacticSystem>();
        if (tacticSystem != null)
        {
            tacticSystem.StopcoolDown = false;
        }
    }

    private void LookAtTarget(Character user, Character target)
    {
        Vector3 direction = target.transform.position - user.transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.1f)
        {
            user.transform.rotation = Quaternion.Slerp(user.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * user.stat.rotationSpeed);
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
