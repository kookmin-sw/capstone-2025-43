using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MyProject.Utils;
using Unity.VisualScripting; // SFX
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
        CharacterAnimation anim = user.GetComponent<CharacterAnimation>(); // animation

        while (true)
        {
            if (target.Hp <= 0)
            {
                break;
            }
            agent.isStopped = false;
            agent.SetDestination(target.transform.position);
            anim.SetMoveState(true, user.MoveSpeed);    // animation
            if(AudioManager.Instance)
                AudioManager.Instance.PlayEffect(SFXType.FootStep); //SFX

            float distance = Vector3.Distance(user.transform.position, target.transform.position);
            if (distance <= user.stat.attackRange)
            {
                agent.isStopped = true;
                anim.SetMoveState(false, user.MoveSpeed);    // animation
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
    protected IEnumerator RotateAndAction(Character user,List<Character> targets,NavMeshAgent agent, Func<Character, List<Character>, IEnumerator> attackAction)
    {
        if (targets == null || targets.Count == 0 || targets[0] == null)
            yield break;

        Character target = targets[0];
        TacticSystem tacticSystem = user.tacticSystem;
        CharacterAnimation anim = user.anim;

        while (true)
        {
            if (target.Hp <= 0)
                break;

            agent.isStopped = true;
            agent.ResetPath();
            agent.velocity = Vector3.zero;

            Vector3 directionToTarget = (target.transform.position - user.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
            user.transform.rotation = Quaternion.RotateTowards(user.transform.rotation, lookRotation, Time.deltaTime * 360f);

            float angle = Vector3.Angle(user.transform.forward, directionToTarget);
            anim.SetMoveState(false, 0f);

            if (angle < 10f)
            {
                yield return user.StartTrackedCoroutine(attackAction(user, targets)); // ÇÙ½É º¯°æ
                break;
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
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            float rotationSpeed = Mathf.Max(user.stat.rotationSpeed * Time.deltaTime, 1f);
            user.transform.rotation = Quaternion.RotateTowards(user.transform.rotation, targetRotation, rotationSpeed);
        }
    }
}