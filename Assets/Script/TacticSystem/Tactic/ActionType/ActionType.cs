using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public abstract class ActionType : ScriptableObject
{
    public string displayName = "DisplayName";
    public float actionCooldown = 0f;
    public override string ToString() => displayName;

    //Can Edit isSingleTarget in Editor but Not in Other class
    [SerializeField]
    private bool isSingleTarget = true;
    public bool IsSingleTarget { get => isSingleTarget; protected set => isSingleTarget = value; }

    public abstract void Execute(Character user, List<Character> targets);
    protected IEnumerator MoveAndAction(Character user, Character target, NavMeshAgent agent, Action<Character, Character> attackAction)
    {
        TacticSystem tacticSystem = user.GetComponent<TacticSystem>();
        while (true)
        {
            agent.isStopped = false;
            agent.SetDestination(target.transform.position);

            float distance = Vector3.Distance(user.transform.position, target.transform.position);
            if (distance <= user.stat.attackRange)
            {
                agent.isStopped = true;
                agent.ResetPath();
                agent.velocity = Vector3.zero;

                Vector3 directionToTarget = (target.transform.position - user.transform.position).normalized;
                float angle = Vector3.Angle(user.transform.forward, directionToTarget);
                LookAtTarget(user, target);
                if (angle < 10f)
                {
                    attackAction(user, target); //Use Function
                    break;
                }
            }
            yield return null;
        }

        if (tacticSystem)
        {
            agent.isStopped = false;
            agent.ResetPath();
            agent.velocity = Vector3.zero;

            tacticSystem.stopCooldown = false;
        }
    }
    protected void LookAtTarget(Character user, Character target)
    {
        if (user == null || target == null) return;

        Vector3 direction = target.transform.position - user.transform.position;
        direction.y = 0f; // Y축 회전 제외

        if (direction.sqrMagnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            float rotationSpeed = Mathf.Max(user.stat.rotationSpeed * Time.deltaTime, 1f);
            user.transform.rotation = Quaternion.RotateTowards(user.transform.rotation, targetRotation, rotationSpeed);
        }
    }
}