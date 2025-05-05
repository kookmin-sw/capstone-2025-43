using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TacticSystem/Action/LaserAction")]
public class LaserAction : ActionType
{
    public GameObject laserPrefab;
    public float damage = 100f;
    public float laserDuration = 3f;

    public override void Execute(Character user, List<Character> targets)
    {
        if (targets == null || targets.Count == 0) return;

        // ��Ÿ�� ����
        TacticSystem tacticSystem = user.tacticSystem;
        if (tacticSystem != null)
        {
            tacticSystem.stopCooldown = true;
        }

        // ȸ�� �� ���� �ڷ�ƾ ����
        user.StartCoroutine(RotateAndAction(user, targets, user.agent, FireLaser)); // FireLaser ��ü�� �ѱ�

    }

    private IEnumerator FireLaser(Character user, List<Character> targets)
    {
        user.anim.PlayAttack(1);

        List<IEnumerator> laserCoroutines = new List<IEnumerator>();
        List<Coroutine> running = new List<Coroutine>();
        int finishedCount = 0;

        foreach (var target in targets)
        {
            if (target == null || target.Hp <= 0) continue;

            IEnumerator coroutine = FireSingleLaserWithNotify(user, target, () => finishedCount++);
            Coroutine handle = user.StartCoroutine(coroutine);
            running.Add(handle);
        }

        // Wait until all FireSingleLaserWithNotify calls complete
        yield return new WaitUntil(() => finishedCount >= running.Count);
    }


    private IEnumerator FireSingleLaserWithNotify(Character user, Character target, System.Action onComplete)
    {
        if (user == null || target == null)
        {
            onComplete?.Invoke();
            yield break;
        }

        GameObject laserInstance = Instantiate(laserPrefab);
        StartEndLaser laserScript = laserInstance.GetComponent<StartEndLaser>();

        float tickInterval = laserDuration / 3f;

        for (int i = 0; i < 3; i++)
        {
            float timer = 0f;

            while (timer < tickInterval)
            {
                // ������ Ÿ���� �׾��� ��� ��� �������� �ı��ϰ� ����
                if (user == null || user.Hp <= 0 || target == null || target.Hp <= 0)
                {
                    Destroy(laserInstance); // �������� ��� �ı�
                    onComplete?.Invoke();
                    yield break;
                }

                Vector3 start = user.rightHand.position;
                Vector3 end = target.torso.position;
                Vector3 dir = (end - start).normalized;

                laserInstance.transform.position = start;
                laserInstance.transform.forward = dir;

                if (laserScript != null)
                    laserScript.MaxLength = Vector3.Distance(start, end);

                timer += Time.deltaTime;
                yield return null;
            }

            if (target != null && target.Hp > 0)
            {
                target.ApplyDamage(damage / 3f); // 3ƽ���� ������ ������ ����
            }

            // Ÿ���� �׾��ٸ� �ݺ����� ����
            if (target == null || target.Hp <= 0)
                break;
        }

        // �������� ������ ó��
        if (laserScript != null)
        {
            laserScript.DisablePrepare();
        }

        Destroy(laserInstance, 1f); // ������ �ν��Ͻ��� ������ 1�� �� �ı��� �� �ֵ��� ����
        onComplete?.Invoke();
    }

}
