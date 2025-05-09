using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "TacticSystem/Action/Teleport")]
public class TeleportAction : ActionType
{
    public override void Execute(Character user, List<Character> targets)
    {
        user.anim.PlayAttack(2);
        //SingleTarget Setting
        foreach (Character target in targets)
        {
            Teleportation(user, target);
        }
    }

    private void Teleportation(Character user, Character target)
    {
        TeleportationVFX(user);

        float userRadius = user.agent.radius;
        float targetRadius = target.agent.radius;

        float safeDistance = Mathf.Max(userRadius, targetRadius) + 0.2f;
        Vector3 offset = -target.transform.forward * safeDistance;
        Vector3 teleportPosition = target.transform.position + offset;

        NavMeshHit hit;
        bool warped = false;

        if (NavMesh.SamplePosition(teleportPosition, out hit, 1.0f, NavMesh.AllAreas))
        {
            user.agent.Warp(hit.position);
            warped = true;
        }
        else
        {
            Vector3 sideOffset = target.transform.right * safeDistance;
            Vector3 sidePosition = target.transform.position + sideOffset;

            if (NavMesh.SamplePosition(sidePosition, out hit, 1.0f, NavMesh.AllAreas))
            {
                user.agent.Warp(hit.position);
                warped = true;
            }
            else
            {
                user.agent.Warp(target.transform.position);
                warped = true;
            }
        }

        if (warped)
        {
            user.agent.updateRotation = false;

            Vector3 directionToTarget = target.transform.position - user.transform.position;
            directionToTarget.y = 0;

            if (directionToTarget != Vector3.zero)
            {
                user.transform.rotation = Quaternion.LookRotation(directionToTarget);
            }

            user.agent.updateRotation = true;
        }

        TeleportationVFX(user);
    }
    private void TeleportationVFX(Character target)
    {
        Vector3 effectPosition = new Vector3(target.transform.position.x, target.transform.position.y + 0.1f, target.transform.position.z);
         EffectPoolManager.Instance.GetEffect("Teleport", effectPosition);
    }
}
