using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TacticSystem/Action/LaserAction")]
public class LaserAction : ActionType
{
    public GameObject laserPrefab;
    public float damage = 10f;
    public float laserDuration = 3f;

    public override void Execute(Character user, List<Character> targets)
    {
        if (targets == null || targets.Count == 0) return;
        Character istarget = targets[0];
        if (istarget == null || istarget.Hp <= 0) return;

        foreach (Character target in targets)
        {
            if (target != null && target.Hp > 0)
            {
                user.StartTrackedCoroutine(FireLaser(user, target));
            }
        }
    }

    private IEnumerator FireLaser(Character user, Character target)
    {
        user.anim.PlayAttack(1);
        GameObject laserInstance = Instantiate(laserPrefab);
        var laserScript = laserInstance.GetComponent<StartEndLaser>();
        float timer = 0f;

        // 레이저 발사 중
        while (timer < laserDuration && target != null && target.Hp > 0)
        {
            Vector3 start = user.rightHand.position;
            Vector3 end = target.torso.position;
            Vector3 dir = (end - start).normalized;

            laserInstance.transform.position = start;
            laserInstance.transform.forward = dir;

            laserScript.MaxLength = Vector3.Distance(start, end); 

            timer += Time.deltaTime; 
            yield return null;
        }

        if (laserScript) laserScript.DisablePrepare();

        target.ApplyDamage(damage);

        Destroy(laserInstance, 1f);
    }
}
