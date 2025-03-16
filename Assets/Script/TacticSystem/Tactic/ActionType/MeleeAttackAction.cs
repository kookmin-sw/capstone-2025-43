using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "TacticSystem/Action/MeleeAttack")]
public class MeleeAttackAction : TacticAction
{
    public override void Execute(Character user, Character target)
    {
        if (user == null || target == null) return;

        // TacticSystem���� ��ٿ� ���߱�
        TacticSystem tacticSystem = user.GetComponent<TacticSystem>();
        if (tacticSystem != null)
        {
            tacticSystem.StopcoolDown = true;
        }

        // NavMeshAgent�� �ִ��� Ȯ��
        if (!user.TryGetComponent(out NavMeshAgent agent))
        {
            Debug.LogError($"{user.name}�� NavMeshAgent�� ����!");
            return;
        }

        // �̵� ����
        agent.speed = user.moveSpeed;
        agent.isStopped = false;

        // �ڷ�ƾ���� ���� �����ϰ� ����
        user.StartCoroutine(MoveAndAttack(user, target, agent));
    }

    private System.Collections.IEnumerator MoveAndAttack(Character user, Character target, NavMeshAgent agent)
    {
        while (true)
        {
            // ��ǥ���� �Ÿ� ���
            float distance = Vector3.Distance(user.transform.position, target.transform.position);

            // ��ǥ�� �ٶ󺸰� �ϱ�
            LookAtTarget(user, target);

            // ��ǥ�� �����ϸ� ����
            if (distance <= user.attackRange)
            {
                agent.isStopped = true; // �̵� ����
                Attack(user, target);
                break; // ���� �� �ڷ�ƾ ����
            }

            // ��ǥ�� ���� �̵�
            if (user.TryGetComponent(out Animator animator)){
                animator.SetBool("Walk", true);
            }
            agent.SetDestination(target.transform.position);
            
            // �� ������ ���
            yield return null;
        }

        // ���� �� ��ٿ� �ٽ� ����
        TacticSystem tacticSystem = user.GetComponent<TacticSystem>();
        if (tacticSystem != null)
        {
            tacticSystem.StopcoolDown = false;
        }
    }

    private void LookAtTarget(Character user, Character target)
    {
        // Ÿ���� �ٶ󺸰� �ϱ�
        Vector3 direction = target.transform.position - user.transform.position;
        direction.y = 0f;  // y �� ȸ�� ���� (�������θ� ȸ��)

        if (direction.sqrMagnitude > 0.1f) // ������ ���� ���� ȸ��
        {
            user.transform.rotation = Quaternion.Slerp(user.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * user.rotationSpeed);
        }
    }

    private void Attack(Character user, Character target)
    {
        Debug.Log($"{user.name}�� {target.name}���� {user.damage} �������� ��!");

        //Animation Trigger
        if (user.TryGetComponent(out Animator animator))
        {
            animator.SetTrigger("Attack");
        }

        // ������ ���� (Character�� ApplyDamage �Լ� Ȱ��)
        target.ApplyDamage(user.damage);
    }
}
